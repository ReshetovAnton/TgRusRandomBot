using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TgRusRandomBot.TgBotModels
{
    public class FactMain
    {
        [JsonPropertyName("fact")]
        public FactClass Fact = new();

        public class FactClass
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("text")]
            public string Text { get; set; }
        }
    }
}
