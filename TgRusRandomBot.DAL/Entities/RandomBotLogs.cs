using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgRusRandomBot.DAL.Entities
{
    public class RandomBotLogs
    {
        [Key]
        public long Id { get; set; }
        public Guid UserGuid { get; set; }
        public int BotId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
