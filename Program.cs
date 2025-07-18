﻿using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<MenuItem> items = new List<MenuItem>();
    static decimal tipAmount = 0;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Monk's Cafe");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Add Tip");
            Console.WriteLine("4. Display Bill");
            Console.WriteLine("5. Clear All");
            Console.WriteLine("6. Save to file");
            Console.WriteLine("7. Load from file");
            Console.WriteLine("8. Exit");
            Console.Write("\nEnter your choice: ");

            switch (Console.ReadLine())
            {
                case "1": AddItem(); break;
                case "2": RemoveItem(); break;
                case "3": AddTip(); break;
                case "4": DisplayBill(); break;
                case "5": ClearAll(); break;
                case "6": SaveToFile(); break;
                case "7": LoadFromFile(); break;
                case "8": Console.WriteLine("Good-bye!"); return;
                default: Console.WriteLine("Invalid choice"); break;
            }
            Console.WriteLine();
        }
    }


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

    static void AddTip()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items to add tip for");
            return;
        }

        decimal netTotal = CalculateNetTotal();
        Console.WriteLine($"Net Total: ${netTotal}");
        Console.WriteLine("1. Tip Percentage");
        Console.WriteLine("2. Tip Amount");
        Console.WriteLine("3. No Tip");
        Console.Write("Select method: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Enter tip %: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal percent))
                {
                    tipAmount = netTotal * percent / 100;
                    Console.WriteLine("Tip added");
                }
                else Console.WriteLine("Invalid input");
                break;

            case "2":
                Console.Write("Enter tip amount: ");
                if (decimal.TryParse(Console.ReadLine(), out tipAmount))
                {
                    Console.WriteLine("Tip added");
                }
                else Console.WriteLine("Invalid input");
                break;

            case "3":
                tipAmount = 0;
                Console.WriteLine("Tip reset");
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    static void DisplayBill()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items to display");
            return;
        }

        Console.WriteLine("Description\tPrice");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Description}\t${item.Price}");
        }

        decimal netTotal = CalculateNetTotal();
        decimal gst = netTotal * 0.05m;
        decimal total = netTotal + tipAmount + gst;

        Console.WriteLine($"\nNet Total\t${netTotal}");
        Console.WriteLine($"Tip Amount\t${tipAmount}");
        Console.WriteLine($"GST Amount\t${gst}");
        Console.WriteLine($"Total Amount\t${total}");
    }

    static void ClearAll()
    {
        items.Clear();
        tipAmount = 0;
        Console.WriteLine("All items cleared");
    }

    static void SaveToFile()
    {
        Console.Write("Enter filename: ");
        string path = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in items)
                {
                    writer.WriteLine($"{item.Description}|{item.Price}");
                }
                writer.WriteLine($"Tip|{tipAmount}");
            }
            Console.WriteLine($"Saved to {path}");
        }
        catch
        {
            Console.WriteLine("Error saving file");
        }
    }

    static void LoadFromFile()
    {
        Console.Write("Enter filename: ");
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found");
            return;
        }

        try
        {
            items.Clear();
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    if (parts[0] == "Tip")
                    {
                        tipAmount = decimal.Parse(parts[1]);
                    }
                    else
                    {
                        items.Add(new MenuItem(
                            parts[0],
                            decimal.Parse(parts[1])
                        ));
                    }
                }
            }
            Console.WriteLine($"Loaded from {path}");
        }
        catch
        {
            Console.WriteLine("Error loading file");
        }
    }



    static decimal CalculateNetTotal()
    {
        decimal total = 0;
        foreach (var item in items) total += item.Price;
        return total;
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
