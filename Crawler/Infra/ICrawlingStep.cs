using System.Threading.Tasks;

namespace Crawlers.Infra
{
    public interface ICrawlingStep
    {
        Task Execute(ICrawlingContext context);
    }
}