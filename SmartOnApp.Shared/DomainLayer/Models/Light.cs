using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Light : IoTDevice
    {
        public DateTime Timestamp { get; set; }
        public bool LightIsOn { get; set; }
    }
}
