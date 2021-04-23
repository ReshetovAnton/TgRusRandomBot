using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TgRusRandomBot.TgBotModels
{
    public class QuestionMain
    {
        [JsonPropertyName("question")]
        public QuestionClass Question = new();

        public class QuestionClass
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("answer1")]
            public string Answer1 { get; set; }

            [JsonPropertyName("answer2")]
            public string Answer2 { get; set; }

            [JsonPropertyName("answer3")]
            public string Answer3 { get; set; }

            [JsonPropertyName("answer4")]
            public string Answer4 { get; set; }

            [JsonPropertyName("level")]
            public int Level { get; set; }
        }
    }
}
