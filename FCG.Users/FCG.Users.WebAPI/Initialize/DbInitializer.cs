using FCG.Users.Domain.Entities;
using FCG.Users.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FCG.Users.WebAPI.Initialize
{
    public static class DbInitializer
    {
        public static void Seed(this ApplicationDbContext context)
        {
            context.Database.Migrate();

            //if (!context.UsersGroup.Any())
            //{
            //    var grpAdministrador = new UserGroup("ADMINISTRADOR");

            //    context.UsersGroup.Add(
            //        grpAdministrador
            //    );

            //    context.SaveChanges();
            //}

            //if (!context.Users.Any())
            //{
            //    var administrador = new User("Admin","admin@gmail.com", "Admin@987", 1);

            //    context.Users.Add(
            //      administrador
            //    );

            //    context.SaveChanges();
            //}
        }
    }
}
