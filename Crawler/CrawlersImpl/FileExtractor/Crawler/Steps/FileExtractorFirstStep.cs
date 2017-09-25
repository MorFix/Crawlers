using System;
using System.Threading.Tasks;
using Crawlers.Infra;

namespace Crawlers.CrawlersImpl.FileExtractor.Crawler.Steps
{
    public class FileExtractorFirstStep : ICrawlingStep
    {
        public async Task Execute(ICrawlingContext context)
        {
            context.Client.BaseAddress = new Uri("http://fileextractor.justice.gov.il");
            context.HttpHandler.AllowAutoRedirect = false;

            var captcha = await GetSolvedCaptha(context);
        }

        private async Task<string> GetSolvedCaptha(ICrawlingContext context)
        {
            var captchaResponse = await context.Client.GetAsync("/CreateCaptche");
            var imageStream = await captchaResponse.Content.ReadAsStreamAsync();

            // return CaptchaSolver.Solve(Image.FromStream(imageStream), "123456789");
            return await Task.FromResult(string.Empty);
        }
    }
}