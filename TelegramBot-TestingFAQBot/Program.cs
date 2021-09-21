using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_TestingFAQBot
{
    class Program
    {

        //https://youtu.be/OTKJfGbgfxM

        private static string token { get; set; } = "2014272170:AAHDzsJOmmD0v4yWkAz9ttZC7JTzctKKBZ4";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            var me = client.GetMeAsync().Result;
            Console.WriteLine(me.Username);

            client.OnMessage += OnMessageHandler;
            client.StartReceiving();
            Console.ReadLine();
            client.StopReceiving();

        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Сообщение пришло с текстом: {msg.Text}");

                //await client.SendTextMessageAsync(msg.Chat.Id, "Привет "+ msg.Text, replyToMessageId:msg.MessageId);

                switch(msg.Text)
                {
                    case "Стикер":
                        var stic = await client.SendStickerAsync(
                                    chatId: msg.Chat.Id,
                                    sticker: "https://tgram.ru/wiki/stickers/img/pusheen_vk/gif/12.gif",
                                    replyToMessageId: msg.MessageId,
                                    replyMarkup: GetButton()
                                    );
                        break;
                    case "Картинка":
                        var photo = await client.SendPhotoAsync(
                                    chatId: msg.Chat.Id,
                                    photo: "https://avatarko.ru/img/kartinka/2/fantastika_cherep_1663.jpg",
                                    replyToMessageId: msg.MessageId,
                                    replyMarkup: GetButton()
                                    );
                        break;


                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Привет " + msg.Text, replyToMessageId: msg.MessageId, replyMarkup: GetButton());
                        break;
                }

                

                //await client.SendTextMessageAsync(msg.Chat.Id, "Поговорим " + msg.Text, replyMarkup: GetButton());


            }
        }

        private static IReplyMarkup GetButton()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Стикер"}, new KeyboardButton { Text = "Картинка"} },
                    new List<KeyboardButton>{new KeyboardButton { Text = "Tree Word"}, new KeyboardButton { Text="Four Word"} }
                }
            };
        }
    }
}
