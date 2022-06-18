using System;
namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreatePirDTO : IoTDeviceDTO
    {
        public DateTime Timestamp { get; set; }
        public bool MotionIsDetected { get; set; }
    }
}
