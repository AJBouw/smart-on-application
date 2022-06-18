using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreateLdrDTO : IoTDeviceDTO
    {
        public DateTime Timestamp { get; set; }

        [Range(0, 1500)]
        public int Brightness { get; set; }
    }
}
