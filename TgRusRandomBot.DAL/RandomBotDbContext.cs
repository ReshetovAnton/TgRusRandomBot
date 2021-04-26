using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgRusRandomBot.DAL.Entities;

namespace TgRusRandomBot.DAL
{
    public class RandomBotDbContext : DbContext
    {
        public RandomBotDbContext(DbContextOptions<RandomBotDbContext> options)
            : base(options)
        {
        }

        public DbSet<RandomBotUsers> RandomBotUsers { get; set; }
        public DbSet<RandomBotLogs> RandomBotLogs { get; set; }
        public DbSet<RandomBotPatsanskiye> RandomBotPatsanskiye { get; set; }
    }
}
