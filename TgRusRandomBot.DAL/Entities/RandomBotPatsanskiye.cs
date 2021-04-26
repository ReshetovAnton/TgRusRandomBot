using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgRusRandomBot.DAL.Entities
{
    public class RandomBotPatsanskiye
    {
        [Key]
        public int Id { get; set; }
        public string Quote { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
