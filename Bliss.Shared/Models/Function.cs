using System.ComponentModel.DataAnnotations;


namespace Bliss.Models
{
    public class Function
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public FunctionType FunctionType { get; set; } //Get/Set/Reset
        public string Description { get; set; } //"Get motor RPM"
        public object LastValue { get; set; }

        public Function(int id, int deviceId, string name, FunctionType functionType, string description, string lastValue = "", bool enabled = true)
        {
            Id = id;
            DeviceId = deviceId;
            Enabled = enabled;
            Name = name;
            FunctionType = functionType;
            Description = description;
            LastValue = lastValue;
        }
    }

    public enum FunctionType
    {
        GetValue,
        SetValue,
        Reset,
        AutoUpdate
    }
}
