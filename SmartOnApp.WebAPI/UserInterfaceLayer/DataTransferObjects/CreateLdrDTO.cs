using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreateLdrDTO
    {
        [Range(0, 1500)]
        public int Brightness { get; set; }

        [Required]
        public int IoTDeviceId { get; set; }
    }
}

