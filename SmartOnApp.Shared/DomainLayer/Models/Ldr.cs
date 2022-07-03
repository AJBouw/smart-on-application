using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Ldr
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Brightness { get; set; }

        public int IoTDeviceId { get; set; }
        public IoTDevice IoTDevice { get; set; }
    }
}
