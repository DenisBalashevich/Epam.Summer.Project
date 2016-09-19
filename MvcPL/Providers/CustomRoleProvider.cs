using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Interfaces;
using MvcPL.ViewModels;
using MvcPL.Infastructure.Mappers;
using BLL.Interfaces.Entities;
namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserRepository
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleRepository
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string name, string roleName)
        {
            UserEntity user = UserRepository.GetUserByName(name);
            if (ReferenceEquals(user, null))
                return false;
            if (user.Roles.Where(r => r.Name.Equals(roleName)).Count() != 0)
                return true;
            return false;
        }

        public override string[] GetRolesForUser(string name)  
        {
            var user = UserRepository.GetUserByName(name);
            return ReferenceEquals(user, null) ? new string[] { } : user.Roles.Select(r => r.Name).ToArray();
        }

        public override void CreateRole(string roleName) 
        {
            try
            {
                RoleRepository.Create(new RoleEntity { Name = roleName });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}