using System;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class ServoDTO : IoTDeviceDTO
    {
        public DateTime Timestamp { get; set; }

        [Range(0, 180)]
        public int ServoCurrentPosition { get; set; }
    }
}
