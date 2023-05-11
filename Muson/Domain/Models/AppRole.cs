using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
        public AppRole(string name, string id) : base(name)
        {
            this.Id = id;
        }
    }
}
