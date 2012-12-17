using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web.Mvc;

namespace Reprografia.Models.Account
{
    public class User
    {
        public User(IIdentity user)
            : this()
        {
            this.UserName = user.Name;
        }
        public User(string name)
            : this()
        {
            this.UserName = name;
        }
        public User()
        {
            this.Roles = new HashSet<Role>();
        }

        [Key]
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }


}