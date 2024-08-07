namespace SimpleTrader.Domain.Models
{
    public enum MajorIndexType
    {
        DowJones,
        Nasdaq,
        SP500
    }
    public class MajorIndex
    {
        public double Prices { get; set; }
        public double Changes { get; set; }
        public MajorIndexType MajoxIndexType { get; set; }
    }
}
