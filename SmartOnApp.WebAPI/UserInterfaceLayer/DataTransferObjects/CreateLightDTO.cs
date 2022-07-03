using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreateLightDTO
    {
        public bool LightIsOn { get; set; }

        [Required]
        public int IoTDeviceId { get; set; }
    }
}
