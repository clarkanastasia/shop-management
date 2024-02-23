namespace Warehouse.ShopManagement;

public class UserInterface 
{
    public required Shop Shop { private get; init; }

public bool ValidateCustomer(Shop instanceOfShop, out int customerIndex){
        bool isRegistered = false;
        customerIndex = 0;
        Console.WriteLine("Are you an existing customer? \n 1. Yes \n 2. No");
        var input = Console.ReadLine() ?? "";
        if(input == "1"){
            Console.WriteLine("Please enter your name");
            var name = Console.ReadLine() ?? "";
            Shop.CheckExistingCustomer(name, out bool exists);
            if (!exists)
            {
                Console.WriteLine("Sorry we couldn't find a customer with this name");
            } 
            else 
            {
                customerIndex = Shop.FindCustomerIndex(name);
                isRegistered = true;
            }
        } 
        else if (input == "2")
        {
            Console.WriteLine("Let's register then! Please enter you name:");
            var name = Console.ReadLine() ?? "";
            Shop.CheckExistingCustomer(name, out bool exists);
            if (exists)
            {
                Console.WriteLine("A customer with this name is already registered");
            } 
            else 
            {
                Shop.AddCustomer(name, instanceOfShop);
                customerIndex = Shop.FindCustomerIndex(name);
                isRegistered = true;
            }
        }
        return isRegistered;
    }
    public void LaunchMenu()
    {
    PrintWelcomeMessage();
    bool sessionComplete;
        do
        {
            Console.WriteLine("Who is logging in? \n 1. Manager\n 2. Customer");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ManagerMenu();
                    break;
                case "2":
                    CustomerMenu(Shop);
                    break;
                default:
                Console.WriteLine("The value you entered does not correspond to any of the options. Please try again");
                break;
            }
            Console.WriteLine("Do you want to log in again? \n 1. No \n 2. Yes");
            input = Console.ReadLine();
            sessionComplete = input == "1";
        } 
        while(sessionComplete == false);    
    }
    public void ManagerMenu()
    {
        Console.WriteLine("You've selected manager.\nWhat would you like to do today?");
        bool finished;
        do 
        {
            Console.WriteLine(" \n 1. Add a new item \n 2. Increase stock of existing item \n 3. See all items currently in stock \n 4. See recently sold items");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1" :
                    Console.WriteLine("Enter the item you would like to add");
                    var itemName = Console.ReadLine() ?? "";
                    Shop.AddItem(itemName);
                    break;
                case "2" :   
                    Console.WriteLine("Which item would you like to order more of?");
                    itemName = Console.ReadLine() ?? "";
                    Console.WriteLine("How much do you want to increase the quantitiy by?");
                    var quantity = int.Parse(Console.ReadLine() ?? "0");
                    Shop.OrderStock(itemName, quantity);
                    break;
                case "3" :
                    Shop.GetInventory();
                    break;
                case "4" :
                    Shop.GetItemsSold();
                    break;
                default:
                    Console.WriteLine("The value you entered does not correspond to any of the options. Please try again");
                break;
            }
            finished = ExitMenu();
        }
        while(finished == false);
    } 
    public void CustomerMenu(Shop instanceOfShop)
    {
        Console.WriteLine("You've selected customer");
        int customerIndex;
        bool isRegistered;
        do 
        {
            isRegistered = ValidateCustomer(instanceOfShop, out customerIndex);
        } 
        while (isRegistered == false);
        Console.WriteLine($"What would you like to do today, {Shop._customers[customerIndex].CustomerName}?");
        bool finished;
        do 
        {
            Console.WriteLine(" \n 1. See items in stock \n 2. Add an item to your basket \n 3. See what's inside your basket \n 4. Checkout and purchase items in basket");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Shop.GetInventory();
                    break;
                case "2":
                    Console.WriteLine("Which item would you like to buy?");
                    string itemName = Console.ReadLine() ?? "";
                    Shop._customers[customerIndex].AddToBasket(itemName);
                    break;
                case "3":
                    Shop._customers[customerIndex].SeeBasket();
                    break;
                case "4":
                    Shop._customers[customerIndex].PurchaseItems();
                    break;
                default:
                    Console.WriteLine("The value you entered does not correspond to any of the options. Please try again");
                    break;
            }
            finished = ExitMenu();
        } 
        while (finished == false);
        Console.WriteLine($"Thank you for shopping at {Shop.ShopName}");
    }
    public void PrintWelcomeMessage()
    {
    Console.WriteLine($"Welcome to {Shop.ShopName}");
    Console.WriteLine("=====================");
    Console.WriteLine("Navigate the menu by entering the relevant numbers");
    }
    public static bool ExitMenu()
    {
        Console.WriteLine("Would you like to exit programme or see the menu again? \n 1. Exit \n 2. Menu");
        var input = Console.ReadLine();
        var finished = input == "1";    
        return finished;
    }    
}