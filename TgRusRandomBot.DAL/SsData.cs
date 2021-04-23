using CommonData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgRusRandomBot.DAL
{
    public class SsData
    {
        private static readonly string _PgConnectionsStringMGTender = SecretKeys.pgConnectionString;

        public static RandomBotDbContext GetRandomBotDbContex()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RandomBotDbContext>();
            optionsBuilder.UseNpgsql(_PgConnectionsStringMGTender);

            return new RandomBotDbContext(optionsBuilder.Options);
        }
    }
}
