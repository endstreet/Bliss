using Svg;
using System.Text;

namespace Bliss.WebApp.Services
{
    public static class InstrumentService
    {
        private static string? compassDial;
        private static string? newDial;
        private static string? lastbearing;
        public static string? DrawCompass(string bearing)
        {

            if (string.IsNullOrEmpty(compassDial))
            {
                compassDial = System.IO.File.ReadAllText(@"C:\Followthesun\code\Bliss.WebApp\wwwroot\images");
            }
            
            if (bearing == lastbearing && newDial is not null) return newDial;
            lastbearing = bearing;
            //using (Stream stream = new MemoryStream(Encoding.ASCII.GetBytes(background.Replace("[Bearing]", $"{bearing} 31 28.5"))))
            using (Stream stream = new MemoryStream(Encoding.ASCII.GetBytes(compassDial.Replace("[Bearing]", $"{bearing}"))))
            {
                var svgDocument = SvgDocument.Open<SvgDocument>(stream);
                newDial = svgDocument.ToString();
            }
            return newDial;
        }
    }
}
