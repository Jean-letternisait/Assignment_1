using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_Classes___Inheritance
{
    internal class Program
        
    {
        static void Main(string[] args)
        {
            List<Appliance> appliances = new List<Appliance>();
            string Path = "..\\..\\Res\\appliances.txt"; //ReadFile
            string[] lines = File.ReadAllLines(Path);
            foreach (string line in lines)
            {
                string[] fields = line.Split(';');
                long itemNumber = long.Parse(fields[0]);
                string name = fields[1];
                int quantity = int.Parse(fields[2]);
                double wattage = double.Parse(fields[3]);
                string color = fields[4];
                double price = double.Parse(fields[5]);

                int firstDigit = int.Parse(Math.Abs(itemNumber).ToString()[0].ToString());

                if (firstDigit == 1)
                {
                    int numberOfDoors = int.Parse(fields[6]);
                    int height = int.Parse(fields[7]);
                    int width = int.Parse(fields[8]);
                    appliances.Add(new Refrigerator(itemNumber, name,
                        quantity, wattage, color, price, numberOfDoors, height, width));
                }
                else if (firstDigit == 2)
                {
                    string grade = fields[6];
                    int batteryVoltage = int.Parse(fields[7]);
                    appliances.Add(new Vacuum(itemNumber, name,
                        quantity, wattage, color, price, grade, batteryVoltage));
                }
                else if (firstDigit == 3)
                {
                    double capacity = double.Parse(fields[6]);
                    string roomType = fields[7];
                    appliances.Add(new Microwave(itemNumber, name,
                        quantity, wattage, color, price, capacity, roomType));
                }
                else if (firstDigit == 4 || firstDigit == 5)
                {
                    string feature = fields[6];
                    string soundRating = fields[7];

                    appliances.Add(new Dishwasher(itemNumber, name,
                        quantity, wattage, color, price, soundRating, feature));
                }
            }
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Modern Appliances!" +
                "\r\nHow may we assist you?" +
                "\r\n1 – Check out appliance" +
                "\r\n2 – Find appliances by brand" +
                "\r\n3 – Display appliances by type" +
                "\r\n4 – Produce random appliance list" +
                "\r\n5 – Save & exit" +
                "\r\n Enter option:");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option > 5)
                {

                    Console.WriteLine("Please Enter a Valid Option");
                }
                else if (option == 1)
                {

                    Console.WriteLine("Enter the item number of an appliance:");

                    long numberAppliance = Convert.ToInt32(Console.ReadLine());
                    Appliance match = appliances.FirstOrDefault(a => a.ItemNumber == numberAppliance);
                    if (match == null)
                    {
                        Console.WriteLine(" No appliances found with that item number.");
                    }
                    else if (match.Quantity > 0)
                    {
                        match.Quantity--;
                        File.WriteAllLines(Path, appliances.Select(a => a.ToFileLine()));
                        Console.WriteLine($"Appliance '{numberAppliance}' has been checked out.");

                    }
                    else
                    {
                        Console.WriteLine("The appliance is not available to be checked out.");
                    }

                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter a brand to search for:");
                    string brand = Console.ReadLine();
                    var match = appliances.Where(a =>
             string.Equals(a.Name, brand, StringComparison.OrdinalIgnoreCase));
                    if (match == null)
                    {
                        Console.WriteLine("No appliances found from that brand");
                    }
                    else
                    {
                        Console.WriteLine("Matching Appliances:");
                        foreach (var appliance in match)
                        {
                            Console.WriteLine(appliance);
                            Console.WriteLine(); 
                        }
                    }
                }
                else if (option == 3)
                {
                    Console.WriteLine("Appliance Types" +
                        "\r\n1 - Refriegrator " +
                        "\r\n2 - Vacuum" +
                        "\r\n3 - Microwaves" +
                        "\r\n4 - Dishwasher");
                    Console.WriteLine("Enter type of appliance");
                    int applianceType = Convert.ToInt32(Console.ReadLine());
                    if (applianceType == 1)
                    {
                        Console.WriteLine("Enter number of doors:" +
                            " 2 (double door), 3 (three doors) or 4 (four doors):");
                        
                        int doors = Convert.ToInt32(Console.ReadLine());
                        var fridgeMatches = appliances.OfType<Refrigerator>()
                            .Where(r => r.NumberOfDoors == doors).ToList();

                        Console.WriteLine("Matching refrigerators:");
                        foreach (var fridge in fridgeMatches)
                        {
                            Console.WriteLine(fridge);
                            Console.WriteLine();
                        }
                    }
                    else if (applianceType == 2)
                    {
                        Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high)");
                        string input = Console.ReadLine();

                        
                        if (input != "18" && input != "24")
                        {
                            Console.WriteLine("Invalid voltage. Please enter 18 or 24.");
                            
                        }

                        int voltage = int.Parse(input); 
                        var vacuumMatches = appliances.OfType<Vacuum>()
                            .Where(v => v.BatteryVoltage == voltage)
                            .ToList();

                        Console.WriteLine("Matching vacuums:");
                        if (vacuumMatches.Count == 0)
                        {
                            Console.WriteLine("No vacuums found with the specified voltage.");
                        }
                        else
                        {
                            foreach (var vacuum in vacuumMatches)
                            {
                                Console.WriteLine(vacuum);
                                Console.WriteLine(); 
                            }
                        }
                        
                        
                   
                    }
                    else if (applianceType == 3)
                    {
                        Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
                        string roomType = Console.ReadLine().ToUpper();
                        var microwaveMatches = appliances.OfType<Microwave>()
                            .Where(m => m.RoomType.ToUpper().StartsWith(roomType)).ToList();

                        Console.WriteLine("Matching microwaves:");
                        foreach (var microwave in microwaveMatches)
                        {
                            Console.WriteLine(microwave);
                            Console.WriteLine();
                        }
                       
                    }
                    else if (applianceType == 4)
                    {
                        Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):");
                        string soundRating = Console.ReadLine();
                        var dishwasherMatches = appliances.OfType<Dishwasher>()
                            .Where(d => d.SoundRating.Equals(soundRating, StringComparison.OrdinalIgnoreCase)).ToList();

                        Console.WriteLine("Matching dishwashers:");
                        foreach (var dishwasher in dishwasherMatches)
                        {
                            Console.WriteLine(dishwasher);
                            Console.WriteLine();
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid option");
                    }
                }

                else if (option == 4)
                {
                    Console.WriteLine("Enter number of appliances:");
                    int count = Convert.ToInt32(Console.ReadLine());

                    if (count <= 0 || count > appliances.Count)
                    {
                        Console.WriteLine("Invalid number of appliances requested.");
                        return;
                    }

                    Random random = new Random();
                    Console.WriteLine("Random appliances:");
                    foreach (var appliance in appliances.OrderBy(x => random.Next()).Take(count))
                    {
                        Console.WriteLine(appliance);
                        Console.WriteLine();
                    }
                }
                else if (option == 5)
                {
                    File.WriteAllLines(Path, appliances.Select(a => a.ToFileLine()));
                    Console.WriteLine("Appliance data has been saved. Exiting program.");
                    exit =  true;
                }
            }
        }  
    }
}
