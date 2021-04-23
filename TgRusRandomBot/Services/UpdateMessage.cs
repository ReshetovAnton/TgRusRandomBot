using CommonData;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TgRusRandomBot.DAL;
using TgRusRandomBot.DAL.Entities;
using TgRusRandomBot.Models.TgBotModels;
using TgRusRandomBot.TgBotModels;

namespace TgRusRandomBot.Services
{
    public class UpdateMessage
    {
        private static readonly Random Random = new();

        public static void UpdateMessageMethod(object tempMessageModel)
        {
            var messageModel = (UpdateMessageModel)tempMessageModel;

            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            SendActionService.SendAction(botClient, userId, ChatAction.Typing);
            Thread.Sleep(200);

            SaveUserDb(messageModel);

            switch (textNewMessage)
            {
                case "/start":
                    SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.startMessageText, ReplyKeyboardService.ReplyMainMenu());
                    return;
                case "/help":
                    SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.helpMessageText, ReplyKeyboardService.ReplyMainMenu());
                    return;
            }

            switch(textNewMessage)
            {
                case "Числа🔢":
                    Numbers(messageModel);
                    return;
                case "Пароли📲":
                    Password(messageModel);
                    return;
                case "Вопросы❔":
                    Questions(messageModel);
                    return;
                case "Факты📝":
                    Facts(messageModel);
                    return;
                case "Мудрости🧐":
                    Saying(messageModel);
                    return;
                case "Испытать удачу🎲":
                    TryYourLuck(messageModel);
                    return;
            }
        }

        public static void SaveUserDb(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;
            var userName = messageModel.EventArgs.Message.From.Username;
            var firstName = messageModel.EventArgs.Message.From.FirstName;
            var lastName = messageModel.EventArgs.Message.From.LastName;

            var dateAdded = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));

            var _contextRandomBot = SsData.GetRandomBotDbContex();

            var user = _contextRandomBot.RandomBotUsers.SingleOrDefault(u => u.UserId == userId);
            if(user == null)
            {
                var guid = Guid.NewGuid();
                var saveUser = new RandomBotUsers
                {
                    Guid = guid,
                    BotId = botClient.BotId,
                    UserId = userId,
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    DateAdded = dateAdded
                };
                _contextRandomBot.RandomBotUsers.Add(saveUser);
            }
            else
            {
                user.BotId = botClient.BotId;
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;
            }
            _contextRandomBot.SaveChanges();
        }

        public static void Numbers(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextNumber}\n\n" +
                $"от 0 до 9999\n" +
                $"{Random.Next(0, 9999)}";
            SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, ReplyKeyboardService.ReplyMainMenu());
        }

        public static void Password(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextPassword}";
            SendActionService.SendMessageWithInlineKeyboard(botClient, userId, text, ReplyKeyboardService.InlinePassword());
        }

        public static void TryYourLuck(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextTryYourLuck}";
            SendActionService.SendMessageWithInlineKeyboard(botClient, userId, text, ReplyKeyboardService.InlineTryYourLuck());
        }

        public static void Questions(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var client = new RestClient($"https://{SecretKeys.urlRandSite}/question/generate/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", SecretKeys.urlRandSite);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            var response = client.Execute(request);

            var quest = JsonConvert.DeserializeObject<QuestionMain>(response.Content);

            var text = $"{DefaultMessages.messageTextQuestion}\n\n" +
                $"{quest.Question.Text}";
            SendActionService.SendMessageWithInlineKeyboard(
                botClient, userId, text, 
                ReplyKeyboardService.InlineQuestion(quest.Question.Answer1, quest.Question.Answer2, quest.Question.Answer3, quest.Question.Answer4, quest.Question.Id));
        }

        public static void Facts(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var client = new RestClient($"https://{SecretKeys.urlRandSite}/fact/generate/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", SecretKeys.urlRandSite);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            var response = client.Execute(request);

            var fact = JsonConvert.DeserializeObject<FactMain>(response.Content);

            var text = $"{DefaultMessages.messageTextFact}\n\n" +
                $"{fact.Fact.Text}";
            SendActionService.SendMessageWithInlineKeyboard(
                botClient, userId, text,
                ReplyKeyboardService.InlineFact());
        }

        public static void Saying(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            //var textNewMessage = messageE.Message.Text;
            var userId = messageE.Message.From.Id;

            var client = new RestClient($"https://{SecretKeys.urlRandSite}/saying/generate/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", SecretKeys.urlRandSite);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            var response = client.Execute(request);

            var saying = JsonConvert.DeserializeObject<SayingMain>(response.Content);

            var text = $"{DefaultMessages.messageTextSaying}\n\n" +
                $"{saying.Saying.Text}\n\n" +
                $"© {saying.Saying.Author}";
            SendActionService.SendMessageWithInlineKeyboard(
                botClient, userId, text,
                ReplyKeyboardService.InlineSaying());
        }
    }
}