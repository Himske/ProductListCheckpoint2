using ProductListCheckpoint2;

List<Product> products = [];

Console.WriteLine("***** PRODUCT LIST APPLICATION *****");
Console.WriteLine();

while (true) {
    Console.Write("Enter Category: ");
    string category = Console.ReadLine() ?? string.Empty;

    if (category.Trim().ToLower().Equals("q")) {
        Console.WriteLine();
        break;
    }

    Console.Write("Enter Product Name: ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.Write("Enter Price: ");
    string priceString = Console.ReadLine() ?? string.Empty;

    int price = int.Parse(priceString);

    Product product = new(category, name, price);
    products.Add(product);

    Console.WriteLine();
    Console.WriteLine("Product added successfully!");
    Console.WriteLine();
}

Console.WriteLine("***** PRODUCT LIST *****");
Console.WriteLine();
foreach (Product product in products) {
    Console.WriteLine($"{product.Category,-10}| {product.Name,-10}| {product.Price} kr");
}
