namespace Warehouse.ShopManagement;

public class Shop
{
    public required string ShopName {get; init;}

    public List<Item> Inventory {get; set;} = [];

    public readonly List<Customer> _customers = [];

    private readonly List<Item> _itemsSold = [];

    public void AddCustomer(string customerName, Shop instanceOfShop)
    {
        _customers.Add(new Customer
        {
            CustomerName = customerName,
            InstanceOfShop = instanceOfShop,
        });
        Console.WriteLine($"New customer {customerName} added successfully");
    }
    public void AddItem(string itemName)
    {
        int index = FindItemIndex(itemName);
        if (index != -1)
        {
            Console.WriteLine("This item already exists. You can only increase its quantity");
        } else
        {
            Inventory.Add(new Item(itemName));
            Console.WriteLine($"New item {itemName} added successfully");
        }    
    }
    public void AddSoldItem(string itemName, int itemQuanitiy, DateTime dateSold)
    {
        _itemsSold.Add(new Item(itemName, itemQuanitiy, dateSold));
    }
    public void GetInventory()
    {
        if(Inventory.Count > 0)
        {
            Console.WriteLine($"Here's what we have on the shelves at {ShopName}:");
            Inventory.ForEach(item => Console.WriteLine($"{item.Name} : {item.Quantity}"));
        }
        else
        {
        Console.WriteLine("There are currently no items in your shop");   
        }
    }
    public void GetItemsSold()
    {
        Console.WriteLine("Do you want to see\n 1. All items sold \n 2. Items sold over the past week");
        var input = Console.ReadLine();
        if(_itemsSold.Count > 0)
        {
            switch (input)
            {
                case "1" :
                    Console.WriteLine($"Here is a list of all the items sold from {ShopName}");
                    foreach (Item item in _itemsSold){
                    if(item.DateSold != null){
                        string date = item.DateSold.Value.ToString("dd/MM/yyy");
                        Console.WriteLine($"{item.Quantity} units of {item.Name} were bought on {date}" );
                        }
                    }
                    break;
                case "2" :
                    DateTime dateToday = DateTime.Now;
                    IEnumerable<Item> query = _itemsSold.Where(item => item.DateSold >= dateToday.AddDays(-7));
                    Console.WriteLine($"Here is a list of all the items sold from {ShopName} over the past week");
                    foreach (Item item in query){
                    Console.WriteLine($"{item.Quantity} units of {item.Name} were bought on {item.DateSold}");
                    }
                    break;
                default:
                    Console.WriteLine("The value you entered does not correspond to any of the options. Please try again");
                    break;
            }
        } 
        else
        {
            Console.WriteLine("There are no sold items yet");
        }
    }
    public void OrderStock(string itemName, int quantity)
    {
        int index = FindItemIndex(itemName);
        if (index == -1)
        {
            Console.WriteLine("Cannot increase quantity as item does not exist in shop. Add new item first");
        }
        else
        {
            Inventory[index].IncreaseQuantity(quantity);
            Console.WriteLine($"You've ordered more {Inventory[index].Name}. The quantity is now {Inventory[index].Quantity}");
        }
    }
    public int FindItemIndex(string itemName)
    {
        int index = Inventory.FindIndex(item => item.Name == itemName);
        return index;
    }
    public int FindCustomerIndex(string customerName)
    {
        int index = _customers.FindIndex(item => item.CustomerName == customerName);
        return index;
    }
    public void CheckInStock(string itemName, out bool inStock)
    {
        int index = FindItemIndex(itemName);
        inStock = index != -1;
    }
    public void CheckExistingCustomer(string customerName, out bool exists)
    {
        int index = FindCustomerIndex(customerName);
        exists = index != -1;
    }
}