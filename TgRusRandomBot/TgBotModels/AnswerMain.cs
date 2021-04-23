using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TgRusRandomBot.TgBotModels
{
    public class AnswerMain
    {
        [JsonPropertyName("answer")]
        public AnswerClass Answer = new();

        public class AnswerClass
        {
            [JsonPropertyName("correct")]
            public string Correct { get; set; }

            [JsonPropertyName("success")]
            public bool Success { get; set; }
        }
    }
}
