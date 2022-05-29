using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class IoTDevice
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string IoTDeviceName { get; set; }
        public string IoTDeviceType { get; set; }

        public int McuId { get; set; }
        public Mcu Mcu { get; set; }
    }
}
