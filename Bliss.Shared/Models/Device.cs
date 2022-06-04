using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public int Speed { get; set; }

        //{ new Guid("{4d36e978-e325-11ce-bfc1-08002be10318}"),"COM Ports" }
        //{ new Guid("{745a17a0-74d3-11d0-b6fe-00a0c90f57da}"),"HID Interface" }
        public Guid ClassGuid
        {
            get
            {
                return new Guid("{4d36e978-e325-11ce-bfc1-08002be10318}");
            }
        }
        [NotMapped]
        public string Port {
            get
            {
                try
                {
                    return FriendlyName.Substring(FriendlyName.IndexOf('(') + 1, 4);
                }
                catch
                {
                    return "Unknown";
                }
                
            }
        }

        [NotMapped]
        public string State { get; set; }

    }
}
