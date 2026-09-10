namespace ProductListCheckpoint2 {
    internal class ProductManager() {
        public static List<Product> ProductList { get; set; } = [];

        public static void AddProduct() {
            while (true) {
                try {
                    string category = ProductManager.EnterCategory();

                    if (category.ToLower().Equals("q")) {
                        break;
                    }

                    string name = ProductManager.EnterName();
                    int price = ProductManager.EnterPrice();
                    ProductList.Add(new Product(category, name, price));
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

        public static void SearchProduct() {
            string query = GetInput("Search Product: ");
            List<Product> products = ProductList.FindAll(s => s.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase));
            products.AddRange(ProductList.FindAll(s => s.Category.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
            Console.WriteLine();
            Console.WriteLine("FOUND PRODUCTS:");
            Console.WriteLine();
            if (products.Count > 0) {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (Product product in products.OrderBy(p => p.Price)) {
                    Console.WriteLine($"{product.Category,-12}| {product.Name,-12}| {product.Price} kr");
                }
            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No Products Found.");
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Press <Enter> to continue.");
            Console.ReadLine();
        }

        public static int CalculateTotal() {
            int total = ProductList.Sum(p => p.Price);
            return total;
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
                Console.WriteLine($"{product.Category,-12}| {product.Name,-12}| {product.Price} kr");
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
    }
}
