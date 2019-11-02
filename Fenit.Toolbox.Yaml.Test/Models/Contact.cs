using System;
using System.Collections.Generic;
using System.Text;

namespace Fenit.Toolbox.Yaml.Test.Models
{
    class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString() => $"name={Name}, tel={PhoneNumber}";
    }
}
