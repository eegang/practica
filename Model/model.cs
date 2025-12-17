namespace Model
{
    public abstract class SpaceObject { public string Name { get; set; } }

    public class Planet : SpaceObject
    {
        public string DiscoveryDate { get; set; }
        public double Radius { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Comet : SpaceObject
    {
        public double Luminosity { get; set; }
        public double Distance { get; set; }
        public string SpectralType { get; set; }
    }

    public class Satellite : SpaceObject
    {
        public string PlanetName { get; set; }
        public double OrbitalRadius { get; set; }
        public double Mass { get; set; }
    }
}
