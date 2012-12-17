using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using Reprografia.Models;
using Reprografia.Models.Account;
using Reprografia.Data;

namespace Reprografia.lib
{

    public class ReprografiaRoleProvider : RoleProvider
    {
        ReprografiaContext db = new ReprografiaContext();


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                Role role = db.Roles.Find(roleName);
                foreach (var username in usernames)
                {
                    User user = db.Users.Find(username);
                    role.Users.Add(user);
                }
            }
            db.SaveChanges();
        }

        public override void CreateRole(string roleName)
        {
            Role r = new Role();
            r.Name = roleName;
            db.Roles.Add(r);
            db.SaveChanges();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return db.Roles
                .Find(roleName)
                .Users.Select(u => u.UserName)
                .ToArray();
        }

        public override string[] GetAllRoles()
        {
            return (from role in db.Roles
                    select role.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            return db.Users.Find(username)
                .Roles.Select(r => r.Name)
                .ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return db.Roles.Find(roleName)
                .Users.Select(u => u.UserName)
                .ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");

            return db.Roles.Find(roleName)
                .Users.SingleOrDefault(u => u.UserName == username) != default(User);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return (from r in db.Roles
                    where r.Name == roleName
                    select r).Count() > 0;
        }

        public override string ApplicationName
        {
            get;
            set;
        }
    }

}