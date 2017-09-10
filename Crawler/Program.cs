using System.Threading.Tasks;

namespace Crawlers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Op().Wait();
        }

        private static async Task Op()
        {
            await Task.FromResult(true);
        }
    }
}