using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TgRusRandomBot.Models.TgBotModels
{
    public class UpdateMessageModel
    {
        public object Sender { get; set; }
        public MessageEventArgs EventArgs { get; set; }
    }
}
