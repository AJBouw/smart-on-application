using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class UpdateIoTDeviceDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "IoT device name is too long")]
        public string IoTDeviceName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "IoT device type is too long")]
        public string IoTDeviceType { get; set; }
    }
}
