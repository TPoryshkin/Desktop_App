using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<MenuItem> items = new List<MenuItem>();
    static decimal tipAmount = 0;

    static void AddItem()
    {
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();

        Console.Write("Enter price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            items.Add(new MenuItem(desc, price));
            Console.WriteLine("Item added successfully");
        }
        else
        {
            Console.WriteLine("Invalid price");
        }
    }

}

class MenuItem
{
    public string Description { get; set; }
    public decimal Price { get; set; }

    public MenuItem(string description, decimal price)
    {
        Description = description;
        Price = price;
    }
}
