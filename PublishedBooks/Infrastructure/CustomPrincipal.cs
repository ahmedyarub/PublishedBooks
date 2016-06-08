using System.Linq;
using System.Security.Principal;

namespace PublishedBooks.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return true;
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public string Username { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public string Username { get; set; }
    }
}