using ProductListCheckpoint2;

ProductManager products = new();

ProductManager.ShowHeader();

while (true) {
    while (true) {
        try {
            string category = ProductManager.EnterCategory();

            if (category.ToLower().Equals("q")) {
                break;
            }

            string name = ProductManager.EnterName();
            int price = ProductManager.EnterPrice();
            products.AddProduct(category, name, price);
            ProductManager.ShowSuccess("Product added successfully!");
        }
        catch (Exception ex) {
            ProductManager.ShowError(ex.Message);
        }
        finally {
            Console.ResetColor();
        }
    }

    products.ShowProducts();

    while (true) {
        Console.WriteLine();
        string more = ProductManager.GetInput("Do you want to add more products? (Y/N): ");
        Console.WriteLine();
        if (more.ToUpper().Equals("N")) {
            Environment.Exit(0);
        }
        else if (!more.ToUpper().Equals("Y")) {
            ProductManager.ShowError("That is not a valid option.");
        }
        else {
            break;
        }
    }
}

