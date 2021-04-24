using Newtonsoft.Json.Converters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgRusRandomBot.Services
{
    public class SendActionService
    {
        public static async void SendMessageWithReplyKeyboard(ITelegramBotClient botClient, long userId, string text, ReplyKeyboardMarkup replyKeyboard)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                    chatId: userId,
                    text: text,
                    ParseMode.Html,
                    true,
                    false,
                    0,
                    replyKeyboard
                    ).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void SendMessageWithInlineKeyboard(ITelegramBotClient botClient, long userId, string text, InlineKeyboardMarkup inlineKeyboard)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                    chatId: userId,
                    text: text,
                    ParseMode.Default,
                    false,
                    false,
                    0,
                    inlineKeyboard
                    ).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void SendMessage(ITelegramBotClient botClient, long userId, string text)
        {
            try
            {
                await botClient.SendTextMessageAsync(
                    chatId: userId,
                    text: text,
                    ParseMode.Default,
                    false,
                    false,
                    0
                    ).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void SendDice(ITelegramBotClient botClient, long userId, string emoji)
        {
            try
            {
                var botToken = Program.BotTokens.SingleOrDefault(u => Convert.ToInt32(u.Split(":")[0]) == botClient.BotId);
                var client = new RestClient($"https://api.telegram.org/bot{botToken}/sendDice")
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddParameter("chat_id", userId);
                request.AddParameter("emoji", emoji);

                await client.ExecuteAsync(request).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void EditMessage(ITelegramBotClient botClient, CallbackQueryEventArgs e, string text)
        {
            try
            {
                await botClient.EditMessageTextAsync(
                    chatId: e.CallbackQuery.Message.Chat.Id,
                    messageId: e.CallbackQuery.Message.MessageId,
                    text: text,
                    ParseMode.Default,
                    false
                    ).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void EditMessageWithInlineKeyboard(ITelegramBotClient botClient, CallbackQueryEventArgs e, string text, InlineKeyboardMarkup inlineKeyboard)
        {
            try
            {
                await botClient.EditMessageTextAsync(
                    chatId: e.CallbackQuery.Message.Chat.Id,
                    messageId: e.CallbackQuery.Message.MessageId,
                    text: text,
                    ParseMode.Default,
                    false,
                    inlineKeyboard
                    ).ConfigureAwait(false);
            }
            catch { }
        }

        public static async void SendAction(ITelegramBotClient botClient, long userId, ChatAction chatAction)
        {
            try
            {
                await botClient.SendChatActionAsync(userId, chatAction);
            }
            catch { }
        }

        public static async void Spiner(ITelegramBotClient botClient, CallbackQueryEventArgs eventArgs)
        {
            try
            {
                await botClient.AnswerCallbackQueryAsync(eventArgs.CallbackQuery.Id);
            }
            catch { }
        }
    }
}
