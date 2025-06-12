using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Classes___Inheritance
{
    public class Appliance

    {
        long itemNumber;
        string name;
        int quantity;
        double wattage;
        string color;
        double price;
        public Appliance()
        {
             
        }

        public Appliance(long itemNumber, string name, int quantity, double wattage, string color, double price)
        {
            this.ItemNumber = itemNumber;
            this.Name = name;
            this.Quantity = quantity;
            this.Wattage = wattage;
            this.Color = color;
            this.Price = price;
        }

        public long ItemNumber { get => itemNumber; set => itemNumber = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Wattage { get => wattage; set => wattage = value; }
        public string Color { get => color; set => color = value; }
        public double Price { get => price; set => price = value; }

        public virtual string ToFileLine()
        {
            return $"{itemNumber},{name},{quantity},{wattage} {color} {price}";
        }
        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\n" +
                   $"Brand: {Name}\n" +
                   $"Quantity: {Quantity}\n" +
                   $"Wattage: {Wattage}\n" +
                   $"Color: {Color}\n" +
                   $"Price: {Price:C}\n" ;
        }
    }

}
