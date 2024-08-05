using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Infrastructure.Data
{
    public class UsersHotels
    {
        [Key]
        public string Id { get; set; }
        public IdentityUser User { get; set; }
        public List<Building> Buildings { get; set; }
    }
}
