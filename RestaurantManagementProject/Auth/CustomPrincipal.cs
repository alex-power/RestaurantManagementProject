using System;
using System.Linq;
using System.Security.Principal;

namespace RestaurantManagementProject.Auth
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }

        public bool IsInRole(string role)
        {
            return role.Equals(Type);
        }
    }
}