using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using simple_rabbitmq_eft.Database;
using simple_rabbitmq_eft.Database.Models;

namespace simple_rabbitmq_eft.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Environment.GetEnvironmentVariable("host") ?? "localhost";
            Console.WriteLine(host);
            EftContext context = new EftContext();
            var factory = new ConnectionFactory() { HostName = host, UserName = "admin", Password = "123456" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                //TODO: Received methoduyla gelen dataları yakalayıp işlem yapacağımız için EventingBasicConsumer classından nesne alıyoruz.
                var consumer = new EventingBasicConsumer(channel);
                //TODO: Yeni data geldiğinde bu event otomatik tetikleniyor.
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;//TODO: Kuyruktaki içerik bilgisi.
                    var message = Encoding.UTF8.GetString(body);//TODO: Gelen bodyi stringe çeviriyoruz.
                    SendingEftModel sendingEftModel = JsonConvert.DeserializeObject<SendingEftModel>(message); //TODO: Mesajdan dönen veriyi classa çeviriyoruz.
                    context.Send(sendingEftModel);//TODO: Contextimize gönderip Database.json dosyamıza kaydedilmesini sağlıyoruz.
                    Console.WriteLine($" {sendingEftModel.FromId} - {sendingEftModel.Money}₺ --> {sendingEftModel.ToId}");
                };
                channel.BasicConsume(queue: "Eft", //TODO: Consume edilecek kuyruk ismi
                    autoAck: true, //TODO: Kuyruk ismi doğrulansın mı
                    consumer: consumer); //TODO: Hangi consumer kullanılacak.

                Console.WriteLine("Eft kuyruğuna bağlantı başarılı. Dinleniyor...");
                Console.ReadLine();
            }

        }
    }
}
