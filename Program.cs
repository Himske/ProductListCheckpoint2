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
            break;
        case "5":
            break;
        case "6":
            break;
        case "7":
            break;
        case "8":
            break;
        case "9":
            Environment.Exit(0);
            break;
        default:
            break;
    }
    Console.Clear();
}
