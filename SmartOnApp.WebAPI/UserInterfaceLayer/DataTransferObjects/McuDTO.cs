using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class McuDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        [StringLength(maximumLength:30, ErrorMessage = "Name is too long")]
        public string McuName { get; set; }

        [Required]
        [StringLength(maximumLength:17, ErrorMessage ="MacAddress is too long")]
        public string McuMacAddress { get; set; }

        [Required]
        [StringLength(maximumLength:15, ErrorMessage = "IP address is too long")]
        public string McuIp { get; set; }

        [Required]
        [StringLength(maximumLength:50, ErrorMessage = "Hostname is too long")]
        public string McuHostname { get; set; }

        [Required]
        [StringLength(maximumLength:50, ErrorMessage = "Location name is too long")]
        public string Location { get; set; }

        public IList<IoTDeviceDTO> IoTDevices { get; set; }
    }
}
