using Warehouse.ShopManagement;

var myShop = new Shop
{
    ShopName = "My Shop",
};

var myUserInterface = new UserInterface 
{
    Shop = myShop
};
myUserInterface.LaunchMenu();
