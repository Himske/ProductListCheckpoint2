using ProductListCheckpoint2;

while (true) {
    ProductManager.ShowHeader();
    ProductManager.ShowMenu();
    
    string option = ProductManager.GetInput("Select Option: ");
    switch (option) {
        case "1":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.AddProduct();
            break;
        case "2":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.ShowProducts();
            break;
        case "3":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.SearchProduct();
            break;
        case "4":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.EditProduct();
            break;
        case "5":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.DeleteProduct();
            break;
        case "6":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.GenerateStatistics();
            break;
        case "7":
            Console.Clear();
            ProductManager.ShowHeader();
             ProductManager.SaveProducts();
            break;
        case "8":
            Console.Clear();
            ProductManager.ShowHeader();
            ProductManager.LoadProducts();
            break;
        case "9":
            Environment.Exit(0);
            break;
        default:
            break;
    }
    Console.Clear();
}
