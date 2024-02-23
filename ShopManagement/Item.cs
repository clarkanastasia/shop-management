namespace Warehouse.ShopManagement;

public class Item
{
    public string Name { get; init; }   
    
    public int Quantity {get; set; }

    public DateTime ? DateSold {get; init;}

    public Item(string itemName)
    {
        Name = itemName;
        Quantity = 0;
    }
    public Item(string itemName, int itemQuantity)
    {
        Name = itemName;
        Quantity = itemQuantity;
    }

    public Item(string itemName, int itemQuantity, DateTime dateSold)
    {
        Name = itemName;
        Quantity = itemQuantity;
        DateSold = dateSold;
    }

    public void IncreaseQuantity(int increase)
    {
        Quantity += increase;
    }

    public void DecreaseQuantity(int decrease)
    {
        Quantity -= decrease;
    }

    public bool CheckAvailability(int quantityNeeded)
    {
        bool isAvailable = false;
        if (Quantity > 0)
        {
            if(quantityNeeded == 0)
            {
            Console.WriteLine("Item quantity cannot be equal to zero");
            } 
            else if(Quantity >= quantityNeeded)
            {
            isAvailable = true;
            } 
            else if (Quantity < quantityNeeded)
            {
            Console.WriteLine($"Please enter a number smaller than {Quantity}");
            } 
        } 
        else
        {
            Console.WriteLine("Sorry, this item is out of stock");
        }
        return isAvailable;
    }    
}