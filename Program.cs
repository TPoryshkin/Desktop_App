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

    static void RemoveItem()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items to remove");
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i].Description} - ${items[i].Price}");
        }

        Console.Write("Enter item number to remove or 0 to cancel: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= items.Count)
        {
            items.RemoveAt(index - 1);
            Console.WriteLine("Item removed");
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
