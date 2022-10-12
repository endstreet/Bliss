using System.ComponentModel.DataAnnotations;

namespace Bliss.Models
{
    public class ConfigurationSet
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public BleGateway Gateway;

        public ConfigurationSet(string configName, int gatewayId, string apName, string macAddress = "", string apDescription = "")
        {
            Name = configName;
            Gateway = new BleGateway(gatewayId, apName, macAddress, apDescription);
        }
    }
}
