using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TgRusRandomBot.TgBotModels
{
    public class SayingMain
    {
        [JsonPropertyName("saying")]
        public SayingClass Saying = new();

        public class SayingClass
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("author")]
            public string Author { get; set; }
        }
    }
}
