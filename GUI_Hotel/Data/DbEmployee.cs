using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GUI_Hotel.Data
{
    public class DbEmployee
    {
        public static List<string> emails = new List<string> { "Kitchen@HotelHost", "Reception@HotelHost", "Restaurant@HotelHost" };
        public static List<string> passwords = new List<string> { "Kitchen123/", "Reception123/", "Restaurant123/" };
        public static List<Tuple<string, string>> claims = new List<Tuple<string, string>> { new("Kitchen", "KITCHEN"), new("Reception", "RECEPTION"), new("Restaurant", "RESTAURANT") };

        public static void CreateKitchenEmployee(UserManager<IdentityUser> userManager, ILogger log)
        {

            if (userManager.FindByNameAsync(emails[0]).Result == null)
            {
                log.LogWarning("Seeding the " + emails[0] + " user");
                var user = new IdentityUser
                {
                    UserName = emails[0],
                    Email = emails[0]
                };
                IdentityResult result = userManager.CreateAsync
                    (user, passwords[0]).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim(claims[0].Item1, claims[0].Item2);
                    userManager.AddClaimAsync(user, claim);
                }

            }

        }
        
        public static void CreateReceptionEmployee(UserManager<IdentityUser> userManager, ILogger log)
        {

            if (userManager.FindByNameAsync(emails[1]).Result == null)
            {
                log.LogWarning("Seeding the " + emails[1] + " user");
                var user = new IdentityUser
                {
                    UserName = emails[1],
                    Email = emails[1]
                };
                IdentityResult result = userManager.CreateAsync
                    (user, passwords[1]).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim(claims[1].Item1, claims[1].Item2);
                    userManager.AddClaimAsync(user, claim);
                }

            }

        }

        public static void CreateRestaurantEmployee(UserManager<IdentityUser> userManager, ILogger log)
        {

            if (userManager.FindByNameAsync(emails[2]).Result == null)
            {
                log.LogWarning("Seeding the " + emails[2] + " user");
                var user = new IdentityUser
                {
                    UserName = emails[2],
                    Email = emails[2]
                };
                IdentityResult result = userManager.CreateAsync
                    (user, passwords[2]).Result;
                if (result.Succeeded)
                {
                    var claim = new Claim(claims[2].Item1, claims[2].Item2);
                    userManager.AddClaimAsync(user, claim);
                }

            }

        }
    }
}
