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
        public int FeatureId { get; set; }
        public string MacAddress { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }

        public Device(int id, int featureId, string macAddress, string name, string friendlyName, string description = "", bool enabled = true)
        {
            Id = id;
            FeatureId = featureId;
            MacAddress = macAddress;
            Enabled = enabled;
            Name = name;
            Description = description;
            FriendlyName = friendlyName;
            Properties = new List<Function>();
        }

        public virtual List<Function> Properties { get; set; }
    }
}
