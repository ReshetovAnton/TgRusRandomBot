using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TgRusRandomBot.DAL;
using TgRusRandomBot.DAL.Entities;
using TgRusRandomBot.Models.TgBotModels;

namespace TgRusRandomBot.Services
{
    public class DBService
    {
        private static readonly Random Random = new();

        public static void SaveUserDb(UpdateMessageModel messageModel)
        {
            var messageE = messageModel.EventArgs;
            var botClient = (ITelegramBotClient)messageModel.Sender;

            var userId = messageE.Message.From.Id;
            var userName = messageModel.EventArgs.Message.From.Username;
            var firstName = messageModel.EventArgs.Message.From.FirstName;
            var lastName = messageModel.EventArgs.Message.From.LastName;

            var dateAdded = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(DefaultValues.timeZone));

            var _contextRandomBot = SsData.GetRandomBotDbContex();

            var user = _contextRandomBot.RandomBotUsers.SingleOrDefault(u => u.UserId == userId);
            if (user == null)
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

        public static void SaveLogDb(UpdateMessageModel messageModel = null, UpdateCallbackButtonModel callbackButtonModel = null)
        {
            int botId;
            long userId;
            string content;

            var createDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(DefaultValues.timeZone));
            var _contextRandomBot = SsData.GetRandomBotDbContex();

            if (messageModel != null)
            {
                var messageE = messageModel.EventArgs;
                var botClient = (ITelegramBotClient)messageModel.Sender;

                botId = botClient.BotId;
                userId = messageE.Message.From.Id;
                content = messageE.Message.Text;
            }
            else if (callbackButtonModel != null)
            {
                var callbackE = callbackButtonModel.EventArgs;
                var botClient = (ITelegramBotClient)callbackButtonModel.Sender;

                botId = botClient.BotId;
                userId = callbackE.CallbackQuery.From.Id;
                content = callbackE.CallbackQuery.Data;
            }
            else
                return;

            var user = _contextRandomBot.RandomBotUsers.SingleOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                var log = new RandomBotLogs
                {
                    UserGuid = user.Guid,
                    BotId = botId,
                    Content = content,
                    CreateDate = createDate
                };
                _contextRandomBot.RandomBotLogs.Add(log);
                _contextRandomBot.SaveChanges();
            }
            else
                return;
        }

        public static long GetCountUsers()
        {
            var _contextRandomBot = SsData.GetRandomBotDbContex();

            var countUsers = _contextRandomBot.RandomBotUsers.LongCount();

            return countUsers;
        }

        public static RandomBotPatsanskiye GetPatsanskiye()
        {
            var _contextRandomBot = SsData.GetRandomBotDbContex();

            var countPats = _contextRandomBot.RandomBotPatsanskiye.Count();
            var randId = Random.Next(1, countPats);

            var pats = _contextRandomBot.RandomBotPatsanskiye.SingleOrDefault(u => u.Id == randId);
            if(pats != null)
            {
                pats.Show += 1;
                _contextRandomBot.SaveChanges();
            }
            return pats;
        }

        public static void SetPatsanskiyeAppraisal(int id, int appraisal)
        {
            var _contextRandomBot = SsData.GetRandomBotDbContex();

            var pats = _contextRandomBot.RandomBotPatsanskiye.SingleOrDefault(u => u.Id == id);
            if(pats != null)
            {
                pats.Like = appraisal == 1 ? pats.Like += 1 : pats.Like;
                pats.Dislike = appraisal == 0 ? pats.Dislike += 1 : pats.Dislike;
                _contextRandomBot.SaveChanges();
            }
        }
    }
}
