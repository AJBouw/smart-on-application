using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class IoTDeviceDTO : CreateIoTDeviceDTO
    {
        [Required]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public McuDTO Mcu { get; set; }

        public IList<LdrDTO> Ldrs { get; set; }
        public IList<LightDTO> Lights { get; set; }
        public IList<PirDTO> Pirs { get; set; }
        public IList<ServoDTO> Servos { get; set; }
    }
}
