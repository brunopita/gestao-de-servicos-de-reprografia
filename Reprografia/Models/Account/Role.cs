using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models.Account
{
    public class Role
    {
        public Role(string roleName)
            : this()
        {
            this.Name = roleName;
        }
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
