using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Servo
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int ServoCurrentPosition { get; set; }

        public int IoTDeviceId { get; set; }
        public IoTDevice IoTDevice { get; set; }
    }
}