using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    [Index(nameof(IoTDeviceName), IsUnique = true)]
    public class CreateIoTDeviceDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "IoTDevice name is too short")]
        [StringLength(maximumLength: 50, ErrorMessage = "IoTDevice name is too long")]
        public string IoTDeviceName { get; set; }

        [Required]
        public int McuId { get; set; }
    }
}
