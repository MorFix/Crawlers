namespace Crawlers.Tabu
{
    public class NesachViewModel
    {
        public string Gush { get; set; }
        public string Helka { get; set; }
        public string SubHelka { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public NesachType Type { get; set; }

        public NesachViewModel()
        {
            Type = NesachType.F;
        }
    }
}