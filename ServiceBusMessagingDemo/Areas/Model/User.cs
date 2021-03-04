using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusMessagingDemo.Areas.Model
{
    public class User
    {
        private int _Id;
        [Required]
        public string id {


            get {

                return _Id.ToString();

            }


            set {

                _Id = int.Parse(value);

            }
        }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }

    }
}
