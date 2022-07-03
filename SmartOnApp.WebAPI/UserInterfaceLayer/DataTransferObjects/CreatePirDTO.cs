using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreatePirDTO
    {
        public bool MotionIsDetected { get; set; }

        [Required]
        public int IoTDeviceId { get; set; }
    }
}
