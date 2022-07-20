using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bliss.Models
{
    public class BleGateway
    {
        [Key]
        public int Id { get; set; }
        public string ApName { get; set; }
        public string MacAddress { get; set; }
        public string Description { get; set; }

        public BleGateway(int id, string apName, string macAddress, string description)
        {
            Id = id;
            ApName = apName;
            MacAddress = macAddress;
            Description = description;
            Devices = new List<Device>();
        }

        public virtual List<Device> Devices { get; set; }
    }
}
