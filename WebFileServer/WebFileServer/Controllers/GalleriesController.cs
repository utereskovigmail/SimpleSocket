using Microsoft.AspNetCore.Mvc;

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
