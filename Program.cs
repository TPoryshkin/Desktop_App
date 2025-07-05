using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<MenuItem> items = new List<MenuItem>();
    static decimal tipAmount = 0;
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
