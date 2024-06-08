using System.Drawing;
using IronBarCode;
using Kolosok.Application.Contracts.Contacts;
using Newtonsoft.Json;


namespace Kolosok.Persistence.Services;

public class QRCodeService
{
    public async Task<byte[]> GenerateQRCode<T>(T entity) where T : ContactResponse
    {
        //var json = JsonConvert.SerializeObject(entity);
        var filePath = Path.Combine(@"D:\", "MyQR.png");
        string vCardData = "BEGIN:VCARD\r\n" +
                          "VERSION:3.0\r\n" +
                          $"FN:{entity.FirstName} {entity.LastName}\r\n" + 
                          $"TEL:{entity.Phone}\r\n" + 
                          $"EMAIL:{entity.Email}\r\n" + 
                          "END:VCARD";
        QRCodeWriter.CreateQrCode(vCardData).SaveAsPng(filePath);
        var pngBytes = await File.ReadAllBytesAsync(filePath);
        File.Delete(filePath);
        
        return pngBytes;
    }
    
    public async Task<T> ReadQRCode<T>(byte[] qrCodeBytes) where T : class
    {
        var result = await BarcodeReader.ReadAsync(qrCodeBytes,
            new BarcodeReaderOptions() { ExpectBarcodeTypes = BarcodeEncoding.QRCode });
        var entity = JsonConvert.DeserializeObject<T>(result.First().Value);
        
        return entity;
    }
}
