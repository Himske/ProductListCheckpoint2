namespace ProductListCheckpoint2 {
    internal class Product(int id, string category, string name, int price) {
        public int Id { get; set; } = id;
        public string Category { get; set; } = category;
        public string Name { get; set; } = name;
        public int Price { get; set; } = price;

        public override string ToString() {
            return $"{Id,-10}| {Category,-12}| {Name,-12}| {Price} kr";
        }
    }
}
