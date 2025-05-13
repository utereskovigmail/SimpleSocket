using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace WebFileServer.Controllers;

//Об'єкт,який зберігає зображення
public class UploadImage
{
    //Це Base64
    public string Photo { get; set; } = String.Empty;
}

[Route("api/[controller]")]
[ApiController]
public class GalleriesController : ControllerBase
{
    //можна передвати великі об'єми інформації
    [HttpPost] //тип запиту на сервер, тобто щось має змінюватися - зберігатися зображення 
    [Route("upload")] //адреса url - http://localhost:5298/api/Galleries/upload
    public async Task<IActionResult> UploadBase64([FromBody] UploadImage model)
    {
        try
        {
            string fileName = $"{Guid.NewGuid()}.jpg";

            if (model.Photo.Contains(','))
                model.Photo = model.Photo.Split(',')[1];
            byte[] byteArray = Convert.FromBase64String(model.Photo);

            // Читаємо байти як зображення
            using Image image = Image.Load(byteArray);

            // Масштабуємо зображення до 600x600
            image.Mutate(x => x.Resize(
                new ResizeOptions
                {
                    Size = new Size(600, 600), // Максимальний розмір
                    Mode = ResizeMode.Max // Зберігає пропорції без обрізання
                }
                ));

            string folderName = "images";
            string pathFolder = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            // Шлях до збереження файлу
            string outputFilePath = Path.Combine(pathFolder, fileName);

            // Зберігаємо файл у вказаному форматі
            image.Save(outputFilePath);
            //Усе пройшло успішно
            return Ok(new { image = fileName });//код 200 - усе пройшло успішно
        }
        catch(Exception ex)
        {
            //Повертаємо результат помилки
            return BadRequest(new { error = ex.Message });
        }
    }

}
