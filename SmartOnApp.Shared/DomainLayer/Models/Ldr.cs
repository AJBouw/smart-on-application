using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Ldr : IoTDevice
    {
        public DateTime Timestamp { get; set; }
        public int Brightness { get; set; }
    }
}
