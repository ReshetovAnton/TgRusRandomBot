using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgRusRandomBot.DAL.Entities
{
    public class RandomBotUsers
    {
        [Key]
        public Guid Guid { get; set; }
        public int BotId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
