using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Pir : IoTDevice
    {
        public DateTime Timestamp { get; set; }
        public bool MotionIsDetected { get; set; }
    }
}
