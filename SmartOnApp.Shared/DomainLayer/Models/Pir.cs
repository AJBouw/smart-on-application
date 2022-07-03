using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Pir
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool MotionIsDetected { get; set; }

        public int IoTDeviceId { get; set; }
        public IoTDevice IoTDevice { get; set; }
    }
}
