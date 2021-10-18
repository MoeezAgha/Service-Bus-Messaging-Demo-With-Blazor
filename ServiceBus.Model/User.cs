using System.ComponentModel.DataAnnotations;

namespace ServiceBus.Model
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
