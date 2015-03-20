using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anthR.Web.Models.Core
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string GravatarHash{
            get{
                return anthR.Utils.Crypto.MD5.GetHash(this.Email).ToLower();
            }
        }

    }
}
