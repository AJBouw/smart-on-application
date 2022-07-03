using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreateServoDTO
    {
        [Range(0, 180)]
        public int ServoCurrentPosition { get; set; }

        [Required]
        public int IoTDeviceId { get; set; }
    }
}
