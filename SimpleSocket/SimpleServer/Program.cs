// See https://aka.ms/new-console-template for more information
//ifconfig - список IP адрес для даного ПК
//ipconfig - для windows
//127.0.0.1 - локальна - тобто цей ПК і
//спілкування в мережах даного ПК
//0.0.0.0 - і з локахост і з мережі де я є. Наприклад до 
//моєму IP адресу в мережі 

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
// var hostName = Dns.GetHostName();
// Console.WriteLine($"Мій хост: {hostName}");
//Список усіх IP адрес доступних для даного ПК
// var locahost = await Dns.GetHostEntryAsync(hostName);
//
// int i = 0;
// foreach (var item in locahost.AddressList)
// {
//     Console.WriteLine($"{++i}.{item}");
// }
//
// Console.Write("->_");
// int numberIP = int.Parse(Console.ReadLine());
//
// Console.WriteLine("Вкажіть порт:");
//
// int serverPort = int.Parse(Console.ReadLine()); // порт запуску додатка

string[] file = File.ReadLines("config.txt").ToArray();
IPAddress serverIP = IPAddress.Parse(file.First());
int serverPort = int.Parse(file.Last());


Console.WriteLine($"server started at {serverIP}:{serverPort}");

//Згідно цих налаштувань працює наш сервер,
//тобто по цій IP адресі і цьому порту можна буде 
//на сервер надсилати запити і він буде їх обробляти
var ipEndPoint = new IPEndPoint(serverIP, serverPort);

//Створюємо сам сокер, який буде обробляти запити від клієнтів
//Клієнтів може бути багато
//Нашатовуємо наш сокет під мережу Інтернет, у вигляді поток даних
//будуть насилатися нам запити, протокол взаємодії буде TCP
//https - який є надбудовою над TCP і автоматично усе шифрує.
//- це роблем не було.
Socket server = new Socket(AddressFamily.InterNetwork, 
    SocketType.Stream, ProtocolType.Tcp);

try
{
    server.Bind(ipEndPoint); //яку IP адресу і порт він слухає
    server.Listen(10); //Розмір черги клієнтів.

    while(true) //Сервер буде вічно чекати на своєї кієнтів
    {
        //Очікуємо хочаб одного клієнта
        Socket client = server.Accept();
        Console.WriteLine($"На постукав нвступний носорог {client.RemoteEndPoint}");
        int bytes = 0; //кількість байт, які ми отримаємо 
        byte[] buffer = new byte[1024]; //офер, на збережння даних клієнта
        do
        {
            bytes = await client.ReceiveAsync(buffer); //отримали байти від клієнта
            Console.WriteLine($"Повідомлення: {Encoding.Unicode.GetString(buffer)}");
        } while (client.Available > 0); //Поки не прочитали усіх даних від клієнта

        string message = $"Дякую дружок. {DateTime.Now}";
        buffer = Encoding.Unicode.GetBytes(message);
        client.Send(buffer); //Сервер відправляє повімлення назад клієнту
        client.Shutdown(SocketShutdown.Both); //Розриваємо з'яднання
        client.Close(); //видаляють об'єкт
    }
}
catch(Exception ex)
{
    Console.WriteLine($"Помилка в роботі {ex.Message}");
}

Console.ReadKey();