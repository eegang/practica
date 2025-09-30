using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

class Planet
{
    public string Name { get; set; }
    public string DiscoveryDate { get; set; }
    public double Radius { get; set; }
    public double x { get; set; }

    public double y { get; set; }

}

class Program
{
    static void Main()
    {
        int Space_x = 1;
        int Space_x1 = 2;
        int Space_y = 10;
        int Space_y1 = 14;
        var planets = new List<Planet>();
        var lines = File.ReadAllLines("1.txt");


        var regex = new Regex("\"([^\"]+)\"\\s+(\\d{4}\\.\\d{2}\\.\\d{2})\\s+(\\d+(\\.\\d+)?)(\\s+([-+]?[0-9]*\\.?[0-9]+)\\s+([-+]?[0-9]*\\.?[0-9]+))?");

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                var name = match.Groups[1].Value;
                var date = match.Groups[2].Value;
                var radius = double.Parse(match.Groups[3].Value.Replace('.', ','));

                double x = 0, y = 0;
                if (match.Groups[6].Success && match.Groups[7].Success)
                {
                    x = double.Parse(match.Groups[6].Value.Replace('.', ','));
                    y = double.Parse(match.Groups[7].Value.Replace('.', ','));
                }

                planets.Add(new Planet
                {
                    Name = name,
                    DiscoveryDate = date,
                    Radius = radius,
                    x = x,
                    y = y
                });
            }
            else
            {
                Console.WriteLine("Строка не распознана: " + line);
            }
        }

        GetMax(planets);

        findPlanetBetweenCords(planets, Space_y, Space_x, Space_x1,Space_y1 );
        

        Console.ReadKey();
    }

    static void GetMax(List<Planet> planets)
    {
        if (planets.Count > 0)
        {
            var maxPlanet = planets.OrderByDescending(p => p.Radius).First();
            Console.WriteLine($"Планета с наибольшим радиусом: \"{maxPlanet.Name}\" Дата: {maxPlanet.DiscoveryDate} Радиус: {maxPlanet.Radius}, X: {maxPlanet.x}, Y: {maxPlanet.y}");
        }
        else
        {
            Console.WriteLine("Данные о планетах не найдены.");
        }
    }
    static void findPlanetBetweenCords(List<Planet> planets, int Space_y, int Space_x, int Space_x1, int Space_y1 )
    {
        
        foreach (var planet in planets)
        { 
            if (Space_x < planet.x && Space_x1 < planet.x && Space_y > planet.y && Space_y1 > planet.y)
            {
                Console.WriteLine($"Планета {planet.Name} находится!");
            }
        }
        
       
        
    }
}
