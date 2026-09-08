using ProductListCheckpoint2;

ProductManager products = new();

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
    bool addedProduct = products.AddProduct(product);

    if (addedProduct) {
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine("Product added successfully!");
        Console.ResetColor();
        Console.WriteLine();
    }
}

Console.WriteLine("***** PRODUCT LIST *****");
Console.WriteLine();
products.ShowProducts();
Console.WriteLine();
Console.WriteLine("------------------------");
Console.WriteLine($"TOTAL PRICE: {products.CalculateTotal()} kr");
Console.WriteLine("------------------------");
