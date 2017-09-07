using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawlers.Infra
{
    public abstract class BaseCrawler
    {
        protected ICrawlingContext Context { get; }
        protected IDictionary<int, ICrawlingStep> Steps { get; }

        protected BaseCrawler()
        {
            Context = ServicesFactory.GetService<ICrawlingContext>();
            Steps = new Dictionary<int, ICrawlingStep>();
        }

        public virtual async Task Crawl()
        {
            foreach (var step in Steps.OrderBy(x => x.Key).Select(x => x.Value))
            {
                await step.Execute(Context);
            }
        }
    }
}