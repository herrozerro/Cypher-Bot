using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CypherBot.Blazor.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string DiscordID { get; set; }

    }

    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
