using Microsoft.VisualBasic;

namespace Warehouse.ShopManagement;

public class Customer
{
    public required string CustomerName
    {get; init;}
    public required Shop InstanceOfShop {get; set;}

    private readonly List<Item> _basket = [];

    public void SeeBasket()
    {
        if(_basket.Count > 0){
        Console.WriteLine($"Here's what's in {CustomerName}'s basket");
        _basket.ForEach(item => Console.WriteLine($"{item.Name} : {item.Quantity}"));
        } else{
        Console.WriteLine($"{CustomerName}'s basket is empty");
        }
    }
    public void AddToBasket(string itemName)
    {
        InstanceOfShop.CheckInStock(itemName, out bool inStock);
        if (!inStock){
            Console.WriteLine("Couldn't add item to basket");
        } else {
            int index = InstanceOfShop.FindItemIndex(itemName);
            Console.WriteLine($"There are {InstanceOfShop.Inventory[index].Quantity} units of {InstanceOfShop.Inventory[index].Name} available");
            Console.WriteLine("How many units would you like to buy?");
            int userInput = int.Parse(Console.ReadLine() ?? "0");
            bool isAvailable = InstanceOfShop.Inventory[index].CheckAvailability(userInput);
            if(isAvailable){
                _basket.Add(new Item(itemName, userInput));
                InstanceOfShop.Inventory[index].DecreaseQuantity(userInput);
                Console.WriteLine("Item added to basket");
            }
        }
    } 
    public void PurchaseItems()
    {
        if (_basket.Count > 0){
        DateTime date = DateTime.Now;
        _basket.ForEach(item => InstanceOfShop.AddSoldItem(item.Name, item.Quantity, date));
        _basket.Clear();
        Console.WriteLine("Your purchase is complete");
        } else{
        Console.WriteLine("Unable to complete purchase as basket is empty");
        }
    }
}