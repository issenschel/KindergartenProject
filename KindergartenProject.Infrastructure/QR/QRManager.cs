using QRCoder.Xaml;
using QRCoder;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KindergartenProject.Infrastructure.QR
{
    namespace Infrastructure.QR
    {
        public class QRManager
        {
            public DrawingImage Generate(object info)
            {
                // Предварительная валидация info
                if (info == null)
                {
                    // Обработка неверных данных
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                };
                var jsonString = JsonSerializer.Serialize(info, options);

                var generator = new QRCodeGenerator();
                var data = generator.CreateQrCode(jsonString, QRCodeGenerator.ECCLevel.H);
                var qrCode = new XamlQRCode(data);
                var image = qrCode.GetGraphic(20);

                return image;

            }
        }
    }
}
