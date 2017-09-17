using System.Collections.Generic;
using AForge.Imaging.Filters;
using Tesseract;
using System.Drawing;
using System.Drawing.Imaging;

namespace Crawlers.Infra.CaptchaSolver
{
    public class CaptchaSolver
    {
        public static string Solve(Image img, string validDigits = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ", bool useFilters = true)
        {
            var rectangle = new Rectangle(0, 0, img.Width, img.Height);
            var bitmap = new Bitmap(img).Clone(rectangle, PixelFormat.Format24bppRgb);

            return Ocr(useFilters ? ApplyFilters(bitmap) : bitmap, validDigits)?.Trim();
        }

        private static string Ocr(Bitmap b, string validDigits)
        {
            using (var engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", validDigits);
                engine.SetVariable("tessedit_unrej_any_wd", true);

                using (var page = engine.Process(b, PageSegMode.SingleLine))
                {
                    return page.GetText();
                } 
            }
        }

        private static Bitmap ApplyFilters(Bitmap img)
        {
            var colorFiltering = new ColorFiltering
            {
                Blue = new AForge.IntRange(0, 110),
                Red = new AForge.IntRange(0, 110),
                Green = new AForge.IntRange(0, 110)
            };
            var inverter = new Invert();
            var blobsFiltering = new BlobsFiltering { MinHeight = 10 };
            var opening = new Opening();
            var gaussianSharper = new GaussianSharpen();
            var contrastCorrection = new ContrastCorrection();

            var filters = new List<IFilter>
            {
                gaussianSharper,
                /*inverter,
                opening,
                inverter,*/
                blobsFiltering,
                inverter,
                /*opening,*/
                contrastCorrection,
                //colorFiltering,
                blobsFiltering,
                colorFiltering, // Mine
                /*inverter*/
            };

            return new FiltersSequence(filters.ToArray()).Apply(img);
        }
    }
}