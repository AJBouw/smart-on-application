using System;
using System.Collections.Generic;

namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class IoTDevice
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string IoTDeviceName { get; set; }

        public int McuId { get; set; }
        public Mcu Mcu { get; set; }

        public IList<Ldr> Ldrs { get; set; }
        public IList<Light> Lights { get; set; }
        public IList<Pir> Pirs { get; set; }
        public IList<Servo> Servos { get; set; }
    }
}
