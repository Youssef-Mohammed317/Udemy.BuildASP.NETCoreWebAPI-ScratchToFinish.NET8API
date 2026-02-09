using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infra.Identity.Domian.Models;

namespace NZWalks.Infra.Identity.Seads
{
    public static class SeadZNWalksAuthDb
    {
        public static void SeadRoles(ModelBuilder modelBuilder)
        {
            var readerRoleId = "a0117157-56cc-494e-805c-8e4ca22086ec";
            var writerRoleId = "b34006d3-0323-46c3-89a6-8997578aaae7";
            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new ApplicationRole()
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }

            };


            modelBuilder.Entity<ApplicationRole>().HasData(roles);
        }

    }
}
