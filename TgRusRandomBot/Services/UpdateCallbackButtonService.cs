using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TgRusRandomBot.Models.TgBotModels;
using Telegram.Bot.Types.Enums;
using System.Threading;
using RestSharp;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TgRusRandomBot.TgBotModels;
using TgRusRandomBot.Cache;
using TgRusRandomBot.TgBotModels.Statistics;

namespace TgRusRandomBot.Services
{
    public class UpdateCallbackButtonService
    {
        private static readonly Random Random = new();

        public static void UpdateCallbackButtonMethod(object tempCallbackButtonModel)
        {
            var callbackButtonModel = (UpdateCallbackButtonModel)tempCallbackButtonModel;

            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var callbackQueryData = callbackE.CallbackQuery.Data.Split("|||");
            var userId = callbackE.CallbackQuery.From.Id;

            DBService.SaveLogDb(null, callbackButtonModel);

            switch (callbackQueryData[0])
            {
                case "Pass":
                    SendActionService.SendAction(botClient, userId, ChatAction.Typing);
                    Thread.Sleep(20);
                    Password(callbackButtonModel);
                    break;
                case "TryYourLuck":
                    SendActionService.SendAction(botClient, userId, ChatAction.Typing);
                    Thread.Sleep(20);
                    TryYourLuck(callbackButtonModel);
                    break;
                case "Question":
                    Question(callbackButtonModel);
                    break;
                case "Fact":
                    Fact(callbackButtonModel);
                    break;
                case "Saying":
                    Saying(callbackButtonModel);
                    break;
                case "Patsanskiye":
                    Patsanskiye(callbackButtonModel);
                    break;
            }

            switch (callbackQueryData[0])
            {
                case "AdminCountUsers":
                    SendActionService.SendAction(botClient, userId, ChatAction.Typing);
                    Thread.Sleep(20);
                    AdminCountUsers(callbackButtonModel);
                    break;
            }
            SendActionService.Spiner(botClient, callbackE);
        }

        private static void Password(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var callbackQueryData = callbackE.CallbackQuery.Data.Split("|||");
            var userId = callbackE.CallbackQuery.From.Id;

            var countSumbols = Convert.ToInt16(callbackQueryData[1]);

            var password = "";
            while(password.Length < countSumbols)
            {
                var symbol = (char)Random.Next(48, 122);
                if (char.IsLetterOrDigit(symbol))
                    password += symbol;
            }

            var text = $"{DefaultMessages.messageTextPasswordCQ}\n\n" +
                $"{DefaultMessages.messageTextPasswordCS} {countSumbols}\n" +
                $"`{password}`";
            SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, KeyboardService.ReplyMainMenu());
        }

        private static void TryYourLuck(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var callbackQueryData = callbackE.CallbackQuery.Data.Split("|||");
            var userId = callbackE.CallbackQuery.From.Id;

            string text;
            string emoji;

            switch (callbackQueryData[1])
            {
                case "bone":
                    text = "Так так:";
                    emoji = "🎲";
                    break;
                case "coin":
                    var value = Random.Next(0, 5);
                    text = "Так так:\n\n";
                    text += value <= 2 ? "Орел" : "Решка";
                    SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, KeyboardService.ReplyMainMenu());
                    return;
                case "slotMachine":
                    text = "Так так:";
                    emoji = "🎰";
                    break;
                case "bowling":
                    text = "Так так:";
                    emoji = "🎳";
                    break;
                case "darts":
                    text = "Так так:";
                    emoji = "🎯";
                    break;
                case "football":
                    text = "Так так:";
                    emoji = "⚽️";
                    break;
                case "basketball":
                    text = "Так так:";
                    emoji = "🏀";
                    break;
                default:
                    return;
            }

            SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, KeyboardService.ReplyMainMenu());
            Thread.Sleep(200);
            SendActionService.SendDice(botClient, userId, emoji);
        }

        private static void Question(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var callbackQueryData = callbackE.CallbackQuery.Data.Split("|||");

            var client = new RestClient($"https://{SecretKeys.urlRandSite}/question/answer/")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", SecretKeys.urlRandSite);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddParameter("id", callbackQueryData[1]);
            request.AddParameter("number", callbackQueryData[2]);
            var response = client.Execute(request);

            var ans = JsonConvert.DeserializeObject<AnswerMain>(response.Content);

            var text = ans.Answer.Success == true ? "Верно✅\n\n" : "Не верно❌\n\n";
            if (ans.Answer.Correct == "1")
            {
                text += $"{callbackE.CallbackQuery.Message.Text}";
                var but1 = "✅" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[0].Text;
                var but2 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[1].Text;
                var but3 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[0].Text;
                var but4 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[1].Text;

                SendActionService.EditMessageWithInlineKeyboard(
                    botClient, callbackE, text,
                    KeyboardService.InlineQuestionAns(but1, but2, but3, but4));
                return;
            }
            if (ans.Answer.Correct == "2")
            {
                text += $"{callbackE.CallbackQuery.Message.Text}";
                var but1 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[0].Text;
                var but2 = "✅" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[1].Text;
                var but3 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[0].Text;
                var but4 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[1].Text;

                SendActionService.EditMessageWithInlineKeyboard(
                    botClient, callbackE, text,
                    KeyboardService.InlineQuestionAns(but1, but2, but3, but4));
                return;
            }
            if (ans.Answer.Correct == "3")
            {
                text += $"{callbackE.CallbackQuery.Message.Text}";
                var but1 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[0].Text;
                var but2 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[1].Text;
                var but3 = "✅" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[0].Text;
                var but4 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[1].Text;

                SendActionService.EditMessageWithInlineKeyboard(
                    botClient, callbackE, text,
                    KeyboardService.InlineQuestionAns(but1, but2, but3, but4));
                return;
            }
            if (ans.Answer.Correct == "4")
            {
                text += $"{callbackE.CallbackQuery.Message.Text}";
                var but1 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[0].Text;
                var but2 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[0].ToList()[1].Text;
                var but3 = "❌" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[0].Text;
                var but4 = "✅" + callbackE.CallbackQuery.Message.ReplyMarkup.InlineKeyboard.ToList()[1].ToList()[1].Text;

                SendActionService.EditMessageWithInlineKeyboard(
                    botClient, callbackE, text,
                    KeyboardService.InlineQuestionAns(but1, but2, but3, but4));
                return;
            }
        }

        private static void Fact(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var text = $"{callbackE.CallbackQuery.Message.Text}";
            SendActionService.EditMessage(
                    botClient, callbackE, text);
        }

        private static void Saying(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var text = $"{callbackE.CallbackQuery.Message.Text}";
            SendActionService.EditMessage(
                    botClient, callbackE, text);
        }

        private static void Patsanskiye(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

            var callbackQueryData = callbackE.CallbackQuery.Data.Split("|||");
            DBService.SetPatsanskiyeAppraisal(Convert.ToInt32(callbackQueryData[1]), callbackQueryData[2] == "like" ? 1 : 0);

            var text = $"{callbackE.CallbackQuery.Message.Text}";
            SendActionService.EditMessage(
                    botClient, callbackE, text);
        }

        private static void AdminCountUsers(UpdateCallbackButtonModel callbackButtonModel)
        {
            var callbackE = callbackButtonModel.EventArgs;
            var botClient = (ITelegramBotClient)callbackButtonModel.Sender;
            var userId = callbackE.CallbackQuery.From.Id;

            if (userId == SecretKeys.AdminTgUserId)
            {
                var countUsers = new CountUsers() { Count = 0 };
                if (CacheService.TryGetValue(CacheKeys.CountUsers, out long count))
                    countUsers.Count = count;

                var text = $"{DefaultMessages.messageCountUsers} {countUsers.Count}";
                SendActionService.SendMessageWithReplyKeyboard(botClient, userId, text, KeyboardService.ReplyMainMenu());
            }
            else
                SendActionService.SendMessageWithReplyKeyboard(botClient, userId, DefaultMessages.messageNoAccessAdmin, KeyboardService.ReplyMainMenu());
        }
    }
}
