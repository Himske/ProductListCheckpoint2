namespace ProductListCheckpoint2 {
    internal class Product(string category, string name, int price) {
        public string Category { get; set; } = category;
        public string Name { get; set; } = name;
        public int Price { get; set; } = price;
    }
}
