using Crawlers.Infra.Ecom;

namespace Crawlers.CrawlersImpl.Tabu
{
    public class NesachViewModel : BaseViewModel
    {
        public string Gush { get; set; }
        public string Helka { get; set; }
        public string SubHelka { get; set; }
        public NesachType Type { get; set; }

        public NesachViewModel()
        {
            Type = NesachType.F;
        }
    }
}