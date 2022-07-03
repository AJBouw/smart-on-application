using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Light
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool LightIsOn { get; set; }

        public int IoTDeviceId { get; set; }
        public IoTDevice IoTDevice { get; set; }
    }
}
