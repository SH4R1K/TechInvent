using QRCoder;
using System.Text;
using TechInvent.BLL.DtoModels.DtoMVC.Workplace;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace WebMVC.Services
{
    public class QRService
    {
        private readonly IConverter _converter;

        public QRService(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratePdfForPrinting(List<WorkplaceNameUrlDto> items)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='UTF-8'><style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 0; padding: 0; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("td { width: 150px; text-align: center; vertical-align: top; page-break-inside: avoid; }");
            sb.AppendLine("img { width: 150px; height: 150px; }");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine("<table><tr>");

            foreach (var item in items)
            {
                var base64 = GenerateQrBase64(item.Url);
                sb.AppendLine("<td>");
                sb.AppendLine($"<img src='data:image/jpeg;base64,{base64}' alt='QR Code' />");
                sb.AppendLine($"<div>{System.Net.WebUtility.HtmlEncode(item.Name)}</div>");
                sb.AppendLine("</td>");

                // Добавьте перенос строки после определенного количества элементов
                if ((items.IndexOf(item) + 1) % 5 == 0) // Например, 5 элементов в строке
                {
                    sb.AppendLine("</tr><tr>");
                }
            }

            sb.AppendLine("</tr></table></body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A4,
            },
                Objects = {
                new ObjectSettings() {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" },
                },
            }
            };

            return _converter.Convert(doc);
        }
        public string GenerateQrBase64(string data, int size = 5)
        {
            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);
            return qrCode.GetGraphic(size);
        }
    }
}
