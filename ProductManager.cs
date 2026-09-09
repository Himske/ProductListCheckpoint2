namespace ProductListCheckpoint2 {
    internal class ProductManager() {
        public List<Product> ProductList { get; set; } = [];

        public void AddProduct(string category, string name, int price) {
            ProductList.Add(new Product(category, name, price));
        }

        public void ShowProducts() {
            Console.WriteLine();
            Console.WriteLine("***** PRODUCT LIST *****");
            Console.WriteLine();
            foreach (Product product in ProductList.OrderBy(p => p.Price)) {
                Console.WriteLine($"{product.Category,-12}| {product.Name,-12}| {product.Price} kr");
            }
            Console.WriteLine();
            Console.WriteLine("------------------------");
            Console.WriteLine($"TOTAL PRICE: {CalculateTotal()} kr");
            Console.WriteLine("------------------------");
        }

        public int CalculateTotal() {
            int total = ProductList.Sum(p => p.Price);
            return total;
        }

        public static string GetInput(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            return input;
        }

        public static string EnterCategory() {
            string category = GetInput("Enter Category: ");
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
            Console.WriteLine("***** PRODUCT LIST APPLICATION *****");
            Console.WriteLine();
        }
    }
}
