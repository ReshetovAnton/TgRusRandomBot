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
    public class UpdateMessageService
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
            Thread.Sleep(20);

            DBService.SaveUserDb(messageModel);
            DBService.SaveLogDb(messageModel, null);

            switch (textNewMessage)
            {
                case "/start":
                    SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.startMessageText, KeyboardService.ReplyMainMenu());
                    return;
                case "/help":
                    SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.helpMessageText, KeyboardService.ReplyMainMenu());
                    return;
                case "/admin":
                    Administrator(messageModel);
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
                case "Пацанские цитаты👆🏻":
                    Patsanskiye(messageModel);
                    return;
                case "Испытать удачу🎲":
                    TryYourLuck(messageModel);
                    return;
            }

            switch (textNewMessage)
            {
                case "/admin":
                    Administrator(messageModel);
                    return;
            }
        }

        public static void Numbers(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextNumber}\n\n" +
                $"от 0 до 9999\n" +
                $"{Random.Next(0, 9999)}";
            SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, KeyboardService.ReplyMainMenu());
        }

        public static void Password(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextPassword}";
            SendActionService.SendMessageWithInlineKeyboard(botClient, userId, text, KeyboardService.InlinePassword());
        }

        public static void TryYourLuck(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;

            var text = $"{DefaultMessages.messageTextTryYourLuck}";
            SendActionService.SendMessageWithInlineKeyboard(botClient, userId, text, KeyboardService.InlineTryYourLuck());
        }

        public static void Questions(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

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
                KeyboardService.InlineQuestion(quest.Question.Answer1, quest.Question.Answer2, quest.Question.Answer3, quest.Question.Answer4, quest.Question.Id));
        }

        public static void Facts(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

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
                KeyboardService.InlineFact());
        }

        public static void Saying(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

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
                KeyboardService.InlineSaying());
        }

        public static void Patsanskiye(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;

            var pats = DBService.GetPatsanskiye();
            if(pats != null)
            {
                var text = $"{DefaultMessages.messageTextPatsanskiye}\n\n" +
                    $"{pats.Quote}👆🏻";
                SendActionService.SendMessageWithInlineKeyboard(
                    botClient, userId, text,
                    KeyboardService.InlineQPatsanskiye(pats.Id));
            }
        }

        public static void Administrator(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;

            if (userId == SecretKeys.AdminTgUserId)
                SendActionService.SendMessageWithInlineKeyboard(botClient, userId, DefaultMessages.messageAccessAdmin, KeyboardService.InlineAdministrator());
            else
                SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.messageNoAccessAdmin, KeyboardService.ReplyMainMenu());
        }
    }
}