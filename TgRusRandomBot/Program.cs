using CommonData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using TgRusRandomBot.Models.TgBotModels;

namespace TgRusRandomBot
{
    public class Program
    {
        #region CodeQuality
        [SuppressMessage("CodeQuality", "IDE0051:Удалите неиспользуемые закрытые члены", Justification = "<Ожидание>")]
        #endregion
        private static ITelegramBotClient BotClientTest { get; set; }//test
        private static ITelegramBotClient BotClient0 { get; set; }
        private static ITelegramBotClient BotClient1 { get; set; }
        private static ITelegramBotClient BotClient2 { get; set; }
        private static ITelegramBotClient BotClient3 { get; set; }
        private static ITelegramBotClient BotClient4 { get; set; }

        #region Usage
        [SuppressMessage("Usage", "CA2211:Поля, не являющиеся константами, не должны быть видимыми", Justification = "<Ожидание>")]
        #endregion
        public static List<string> BotTokens = new();

        private static void Main()
        {
            //var botTokenTest = SecretKeys.botTokenTest;//test
            //BotTokens.Add(botTokenTest);
            //BotClientTest = new TelegramBotClient(botTokenTest) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            //BotClientTest.OnMessage += UpdateMessage;
            //BotClientTest.OnCallbackQuery += UpdateCallbackButton;
            //BotClientTest.StartReceiving();

            var botToken0 = SecretKeys.botToken0;
            BotTokens.Add(botToken0);
            BotClient0 = new TelegramBotClient(botToken0) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            BotClient0.OnMessage += UpdateMessage;
            BotClient0.OnCallbackQuery += UpdateCallbackButton;
            BotClient0.StartReceiving();

            var botToken1 = SecretKeys.botToken1;
            BotTokens.Add(botToken1);
            BotClient1 = new TelegramBotClient(botToken1) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            BotClient1.OnMessage += UpdateMessage;
            BotClient1.OnCallbackQuery += UpdateCallbackButton;
            BotClient1.StartReceiving();

            var botToken2 = SecretKeys.botToken2;
            BotTokens.Add(botToken2);
            BotClient2 = new TelegramBotClient(botToken2) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            BotClient2.OnMessage += UpdateMessage;
            BotClient2.OnCallbackQuery += UpdateCallbackButton;
            BotClient2.StartReceiving();

            var botToken3 = SecretKeys.botToken3;
            BotTokens.Add(botToken3);
            BotClient3 = new TelegramBotClient(botToken3) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            BotClient3.OnMessage += UpdateMessage;
            BotClient3.OnCallbackQuery += UpdateCallbackButton;
            BotClient3.StartReceiving();

            var botToken4 = SecretKeys.botToken4;
            BotTokens.Add(botToken4);
            BotClient4 = new TelegramBotClient(botToken4) { Timeout = TimeSpan.FromSeconds(DefaultValues.botTimeOut) };
            BotClient4.OnMessage += UpdateMessage;
            BotClient4.OnCallbackQuery += UpdateCallbackButton;
            BotClient4.StartReceiving();

            Console.ReadKey();
        }

        private static void UpdateMessage(object sender, MessageEventArgs eventArgs)
        {
            var messageModel = new UpdateMessageModel
            {
                Sender = sender,
                EventArgs = eventArgs
            };
            var updateMessage = new Thread(new ParameterizedThreadStart(Services.UpdateMessageService.UpdateMessageMethod));
            updateMessage.Start(messageModel);
        }

        private static void UpdateCallbackButton(object sender, CallbackQueryEventArgs eventArgs)
        {
            var callbackButtonModel = new UpdateCallbackButtonModel
            {
                Sender = sender,
                EventArgs = eventArgs
            };
            var updateMessage = new Thread(new ParameterizedThreadStart(Services.UpdateCallbackButtonService.UpdateCallbackButtonMethod));
            updateMessage.Start(callbackButtonModel);
        }
    }
}
