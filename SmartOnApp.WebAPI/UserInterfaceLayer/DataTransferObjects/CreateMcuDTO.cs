using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    [Index(nameof(McuMacAddress), IsUnique = true)]
    public class CreateMcuDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name is too short")]
        [StringLength(maximumLength: 30, ErrorMessage = "Name is too long")]
        public string McuName { get; set; }

        [Required]
        [MinLength(17, ErrorMessage = "MAC address is too short")]
        [StringLength(maximumLength: 17, ErrorMessage = "MAC address is too long")]
        public string McuMacAddress { get; set; }

        [Required]
        [MinLength(15, ErrorMessage = "IP address is too short")]
        [StringLength(maximumLength: 15, ErrorMessage = "IP address is too long")]
        public string McuIp { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Hostname is too short")]
        [StringLength(maximumLength: 50, ErrorMessage = "Hostname is too long")]
        public string McuHostname { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Location name is too short")]
        [StringLength(maximumLength: 50, ErrorMessage = "Location name is too long")]
        public string Location { get; set; }
    }
}
