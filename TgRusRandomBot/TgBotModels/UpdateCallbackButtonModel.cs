using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TgRusRandomBot.Models.TgBotModels
{
    public class UpdateCallbackButtonModel
    {
        public object Sender { get; set; }
        public CallbackQueryEventArgs EventArgs { get; set; }
    }
}
