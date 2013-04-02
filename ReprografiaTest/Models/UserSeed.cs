using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reprografia.Models.Account;

namespace ReprografiaTest.Models
{
    static class UserSeed
    {
        public static User Paolo
        {
            get
            {
                return new User()
                        {
                            FullName = "Paolo Bueno",
                            Email = "paolo@bueno.com.br",
                            Password = "123",
                            Roles = new[] { new Role("Administrador") }
                        };
            }
        }
    }
}
