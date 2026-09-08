namespace ProductListCheckpoint2 {
    internal class ProductManager() {
        public List<Product> ProductList { get; set; } = [];

        public bool AddProduct(Product product) {
            ProductList.Add(product);
            return true;
        }

        public void ShowProducts() {
            foreach (Product product in ProductList.OrderBy(p => p.Price)) {
                Console.WriteLine($"{product.Category,-12}| {product.Name,-12}| {product.Price} kr");
            }
        }

        public int CalculateTotal() {
            int total = ProductList.Sum(p => p.Price);
            return total;
        }
    }
}
