using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp15
{
    internal class Program
    {
        static string token = "5356985464:AAHw4rPVg_VlhAQQD2p4NazEqWK1TsfNsSc";
        static ITelegramBotClient bot = new TelegramBotClient(token);

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text == null) return;
                if (message.Text.ToLower() == "/start")
                {
                    string text = "Привет! Этот бот позволит тебе управлять твоим магазином, чтобы узнать список возможных команд введи '/help'";
                    await botClient.SendTextMessageAsync(message.Chat, text);
                    return;
                }
                else if (message.Text.ToLower() == "/help")
                {
                    string text = "Hello! How are you?";
                    await botClient.SendPhotoAsync(message.Chat, "https://e7.pngegg.com/pngimages/549/388/png-clipart-shrek-illustration-shrek-the-third-donkey-princess-fiona-gingerbread-man-t-shirt-shrek-heroes-fictional-character.png");
                    return;
                }
                // /add_product <Название продукта> <кол-во> <стоимость>
                else if(message.Text.ToLower().StartsWith("/add_product"))
                {
                    string[] input = message.Text.Split('\u002C');
                    if (input.Length < 3)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "неправильный формат");
                        return;
                    }
                    string name = input[1];
                    int count = Convert.ToInt32(input[2]);
                    double price = Convert.ToDouble(input[3]);
                    Product product = new Product(name, "1",count, price);
                }
                // /buy_product <Название продукта> <кол-во>
                else if(message.Text.ToLower().StartsWith("/buy_product"))
                {
                    string[] input = message.Text.Split('\u002C');
                    if (input.Length < 2)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "неправильный формат");
                        return;
                    }
                    string name = input[1];
                    int count = Convert.ToInt32(input[2]);

                }
                // /edit_product <Название продукта> <кол-во> <стоимость>
                else if(message.Text.ToLower().StartsWith("/edit_product"))
                {
                    string[] input = message.Text.Split('\u002C');
                    if (input.Length < 3)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "неправильный формат");
                        return;
                    }
                    string name = input[1];
                    int count = Convert.ToInt32(input[2]);
                    double price = Convert.ToDouble(input[3]);
                }
                else if(message.Text.ToLower() == "/list")
                {

                }
                // /login <login> <password>
                else if(message.Text.ToLower().StartsWith("/login"))
                {
                    string[] input = message.Text.ToLower().Split('\u002C');
                    if (input.Length < 2)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "неправильный формат");
                        return;
                    }
                    
                    string name = input[1];
                    string password = input[2];
                    string userId = update.Message.From.Id.ToString();
                    User user = new User(name, password, "user", userId);
                }
                else
                {

                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient,
            Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();

        }
    }
}
