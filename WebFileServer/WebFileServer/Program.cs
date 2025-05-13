using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

string folderImage = "images";
//Будую шлях до папки
string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderImage);
//Створюємо дану папку у системі
Directory.CreateDirectory(folderPath);

//Налаштовує доступ по url - адреса. до наших файлів на сервері
app.UseStaticFiles(new StaticFileOptions
{
    //вказує до якої папки потрібен доступ
    FileProvider = new PhysicalFileProvider(folderPath), //Шлях на сервері фізично де знаходяться файли
    //Наприклад доступ буде ось так http://localhost:5298/images/1.jpg
    RequestPath = "/images" //Налаштування для url - на сервері
});


app.UseAuthorization();

app.MapControllers();

app.Run();
