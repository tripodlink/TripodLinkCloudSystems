using Microsoft.EntityFrameworkCore;
using SampleDatabaseIdentityAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDatabaseIdentityAuthentication.Data
{
    public static class AppModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedUserAccounts(modelBuilder);
        }


        private static void SeedUserAccounts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData(
               new UserAccount { UserID = "SYSAD", UserName = "SYSTEM ADMINISTRATOR", Password = ".00000" }
               );
        }

    }
}
