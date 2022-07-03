using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class PirDTO : CreatePirDTO
    {
        [Required]
        public int Id { get; set; }

        public IoTDeviceDTO IoTDevice { get; set; }
    }
}
