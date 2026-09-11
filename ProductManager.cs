using System.Net.NetworkInformation;
using System.Text.Json;

namespace ProductListCheckpoint2 {
    internal class ProductManager() {
        public static List<Product> ProductList { get; set; } = [];
        private static readonly string s_fileName = "products.json";
        private static readonly JsonSerializerOptions s_writeOptions = new() {
            WriteIndented = true
        };

        public static void AddProduct() {
            while (true) {
                try {
                    string category = ProductManager.EnterCategory();

                    if (category.ToLower().Equals("q")) {
                        break;
                    }

                    int id = 0;
                    if (ProductList.Count == 0) {
                        id = 1001;
                    }
                    else {
                        id = ProductList.Max(p => p.Id);
                        id++;
                    }
                    string name = ProductManager.EnterName();
                    int price = ProductManager.EnterPrice();
                    ProductList.Add(new Product(id, category, name, price));
                    ProductManager.ShowSuccess("Product added successfully!");
                }
                catch (Exception ex) {
                    ProductManager.ShowError(ex.Message);
                }
                finally {
                    Console.ResetColor();
                }
            }
        }

        internal static void DeleteProduct() {
            string idStr = GetInput("Delete Product(Id): ");
            if (!string.IsNullOrWhiteSpace(idStr)) {
                try {
                    int id = int.Parse(idStr);
                    Product? product = ProductList.FirstOrDefault(p => p?.Id == id, defaultValue: null);
                    if (product != null) {
                        ProductList.Remove(product);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Product: {product}");
                        Console.WriteLine();
                        Console.WriteLine("Removed Successfully.");
                    }
                    else {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("That product doesn't exist.");
                    }
                }
                catch {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("That's not a valid Id.");
                }
            }
            else {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No product deleted.");
            }
            ResetAndPause();
        }

        public static void SearchProduct() {
            string query = GetInput("Search Product: ");
            List<Product> products = ProductList.FindAll(s => s.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase));
            products.AddRange(ProductList.FindAll(s => s.Category.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
            Console.WriteLine();
            Console.WriteLine("FOUND PRODUCTS:");
            Console.WriteLine();
            foreach (Product product in ProductList.OrderBy(p => p.Price)) {
                if (product.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                    product.Category.Contains(query, StringComparison.CurrentCultureIgnoreCase)) {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else {
                    Console.ResetColor();
                }
                Console.WriteLine(product.ToString());
            }
            ResetAndPause();
        }

        public static void SaveProducts() {
            string jsonString = JsonSerializer.Serialize(ProductList, s_writeOptions);
            File.WriteAllText(s_fileName, jsonString);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Products Save Successfully.");
            ResetAndPause();
        }

        public static void LoadProducts() {
            if (!File.Exists(s_fileName)) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("JSON file not found.");
            }
            else {
                string jsonString = File.ReadAllText(s_fileName);
                if (string.IsNullOrWhiteSpace(jsonString)) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("JSON file is empty.");
                }
                else {
                    try {
                        ProductList = JsonSerializer.Deserialize<List<Product>>(jsonString) ?? [];
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Products Loaded Successfully.");

                    }
                    catch (JsonException ex) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error parsing JSON: {ex.Message}");
                    }
                }
            }
            ResetAndPause();
        }

        public static void EditProduct() {
            string idStr = GetInput("Edit Product (Id): ");
            try {
                int id = int.Parse(idStr);
                int index = ProductList.FindIndex(p => p.Id == id);
                Console.WriteLine();
                Console.WriteLine(ProductList[index].ToString());
                Console.WriteLine();
                string newCategory = GetInput("Enter new Category (leave blank to keep current): ");
                if (!string.IsNullOrWhiteSpace(newCategory)) {
                    ProductList[index].Category = newCategory.Trim();
                }
                string newName = GetInput("Enter new Name (leave blank to keep current): ");
                if (!string.IsNullOrWhiteSpace(newName)) {
                    ProductList[index].Name = newName.Trim();
                }
                string priceInput = GetInput("Enter new Price (leave blank to keep current): ");
                if (!string.IsNullOrWhiteSpace(priceInput)) {
                    if (int.TryParse(priceInput, out int newPrice) && newPrice >= 0) {
                        ProductList[index].Price = newPrice;
                    }
                    else {
                        priceInput = string.Empty;
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Invalid price. Keeping old value.");
                        Console.ResetColor();
                    }
                }
                if (string.IsNullOrWhiteSpace(newCategory) && string.IsNullOrWhiteSpace(newName) && string.IsNullOrWhiteSpace(priceInput)) {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No changes where made.");
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Product Updated Successfully.");
                    Console.WriteLine();
                    Console.WriteLine(ProductList[index].ToString());
                }
            }
            catch {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("That product doesn't exist.");
            }
            ResetAndPause();
        }

        public static int CalculateTotal() {
            int total = ProductList.Sum(p => p.Price);
            return total;
        }

        public static void GenerateStatistics() {
            Console.WriteLine("******* STATISTICS *******");
            Console.WriteLine();

            try {
                int highestPrice = ProductList.Max(p => p.Price);
                List<Product> expensiveProducts = ProductList.FindAll(p => p.Price == highestPrice);
                Console.WriteLine("Most Expensive Product(s):");
                foreach (Product product in expensiveProducts) {
                    Console.WriteLine($"{product.Name} - {product.Price} kr");
                }

                Console.WriteLine();

                int lowestPrice = ProductList.Min(p => p.Price);
                List<Product> cheapProducts = ProductList.FindAll(p => p.Price == lowestPrice);
                Console.WriteLine("Cheapest Product(s):");
                foreach (Product product in cheapProducts) {
                    Console.WriteLine($"{product.Name} - {product.Price} kr");
                }

                Console.WriteLine();

                double avgPrice = CalculateTotal() / ProductList.Count;
                Console.WriteLine("Average Price:");
                Console.WriteLine($"{avgPrice} kr");

                Console.WriteLine();

                var categoryCounts = ProductList
                    .GroupBy(product => product.Category)
                    .Select(group => new { Category = group.Key, Count = group.Count() })
                    .OrderByDescending(r => r.Count);
                Console.WriteLine("Products per Category:");
                foreach (var category in categoryCounts) {
                    Console.WriteLine($"{category.Category}: {category.Count}");
                }
            }
            catch (InvalidOperationException) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Currently there are no Products in the system.");
            }
            ResetAndPause();
        }

        public static string GetInput(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            return input;
        }

        public static string EnterCategory() {
            string category = GetInput("Enter Category ('q' to quit): ");
            if (category.Equals(string.Empty)) {
                throw new ArgumentException("Category can't be empty.");
            }
            return category.Trim();
        }

        public static string EnterName() {
            string name = string.Empty;
            while (true) {
                try {
                    name = GetInput("Enter Product Name: ");
                    if (name.Equals(string.Empty)) {
                        throw new ArgumentException("Name can't be empty.");
                    }
                    break;
                }
                catch (Exception ex) {
                    ShowError(ex.Message);
                }
                finally {
                    Console.ResetColor();
                }
            }
            return name.Trim();
        }

        public static int EnterPrice() {
            int price;
            while (true) {
                try {
                    string priceString = GetInput("Enter Price: ");
                    try {
                        price = int.Parse(priceString);
                    }
                    catch {
                        throw new ArgumentException("Invalid price. Please enter a numeric value.");
                    }

                    if (price < 0) {
                        throw new ArgumentException("Price can't be negative.");
                    }
                    break;
                }
                catch (Exception ex) {
                    ShowError(ex.Message);
                }
                finally {
                    Console.ResetColor();
                }
            }
            return price;
        }

        public static void ShowProducts() {
            Console.WriteLine("***** PRODUCT LIST *****");
            Console.WriteLine();
            foreach (Product product in ProductList.OrderBy(p => p.Price)) {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("------------------------");
            Console.WriteLine($"TOTAL PRICE: {ProductManager.CalculateTotal()} kr");
            Console.WriteLine("------------------------");
            Console.WriteLine();
            Console.Write("Press <Enter> to continue.");
            Console.ReadLine();
        }

        public static void ShowError(string message) {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR:");
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowSuccess(string message) {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void ShowHeader() {
            Console.WriteLine("*".PadRight(35, '*'));
            Console.WriteLine(" PRODUCT MANAGEMENT SYSTEM");
            Console.WriteLine("*".PadRight(35, '*'));
            Console.WriteLine();
        }

        public static void ShowMenu() {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Show Products");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. Edit Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Statistics");
            Console.WriteLine("7. Save Products");
            Console.WriteLine("8. Load Products");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
        }

        private static void ResetAndPause() {
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Press <Enter> to continue.");
            Console.ReadLine();
        }
    }
}
