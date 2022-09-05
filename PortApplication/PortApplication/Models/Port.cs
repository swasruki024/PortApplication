namespace PortApplication.Models
{
    /// <summary>
    /// Port Model
    /// </summary>
    public class Port
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PortCode { get; set; }
        public string UnctadPortCode { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Url { get; set; }
        public string MainPortCode { get; set; }
    }
}
