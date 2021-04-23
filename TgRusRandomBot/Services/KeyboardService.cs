using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgRusRandomBot.Services
{
    public class KeyboardService
    {
        public static ReplyKeyboardMarkup ReplyMainMenu() => new()
        {
            Keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton("Числа🔢"),
                    new KeyboardButton("Пароли📲"),
                },
                new[]
                {
                    new KeyboardButton("Вопросы❔"),
                    new KeyboardButton("Факты📝"),
                    new KeyboardButton("Мудрости🧐"),
                },
                new[]
                {
                    new KeyboardButton("Испытать удачу🎲"),
                },
            },
            ResizeKeyboard = true
        };

        public static InlineKeyboardMarkup InlinePassword() => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Pass|||5", Text = "5"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||6", Text = "6"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||7", Text = "7"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||8", Text = "8"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||9", Text = "9"},
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Pass|||10", Text = "10"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||11", Text = "11"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||12", Text = "12"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||13", Text = "13"},
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Pass|||14", Text = "14"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||15", Text = "15"},
                    new InlineKeyboardButton{ CallbackData = $"Pass|||16", Text = "16"},
                },
            } );

        public static InlineKeyboardMarkup InlineTryYourLuck() => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||bone", Text = "Кинуть кость🎲" },
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||coin", Text = "Подбрось монетку💸" }
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||slotMachine", Text = "Однорукий бандит🎰" }
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||bowling", Text = "Боулинг🎳" },
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||darts", Text = "Дартс🎯" }
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||football", Text = "Пенальти⚽️" },
                    new InlineKeyboardButton{ CallbackData = $"TryYourLuck|||basketball", Text = "Трех-очковый🏀" }
                }
            });

        public static InlineKeyboardMarkup InlineQuestion(string answer1, string answer2, string answer3, string answer4, string id) => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Question|||{id}|||1", Text = answer1 },
                    new InlineKeyboardButton{ CallbackData = $"Question|||{id}|||2", Text = answer2 }
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Question|||{id}|||3", Text = answer3 },
                    new InlineKeyboardButton{ CallbackData = $"Question|||{id}|||4", Text = answer4 }
                }
            });

        public static InlineKeyboardMarkup InlineQuestionAns(string answer1, string answer2, string answer3, string answer4) => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"null", Text = answer1 },
                    new InlineKeyboardButton{ CallbackData = $"null", Text = answer2 }
                },
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"null", Text = answer3 },
                    new InlineKeyboardButton{ CallbackData = $"null", Text = answer4 }
                }
            });

        public static InlineKeyboardMarkup InlineFact() => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Fact|||dislike", Text = "Не интересно👎🏻" },
                    new InlineKeyboardButton{ CallbackData = $"Fact|||like", Text = "Интересно👍🏻" }
                }
            });

        public static InlineKeyboardMarkup InlineSaying() => new(
            new InlineKeyboardButton[][]
            {
                new[]
                {
                    new InlineKeyboardButton{ CallbackData = $"Saying|||dislike", Text = "Глупо👎🏻" },
                    new InlineKeyboardButton{ CallbackData = $"Saying|||like", Text = "Мудро👍🏻" }
                }
            });
    }
}
