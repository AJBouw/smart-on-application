using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class UpdateIoTDeviceDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "IoTDevice name is too long")]
        public string IoTDeviceName { get; set; }

        [Required]
        public int McuId { get; set; }
    }
}
