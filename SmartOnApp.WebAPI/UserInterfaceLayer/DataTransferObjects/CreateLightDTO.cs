using System;
namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class CreateLightDTO : IoTDeviceDTO
    {
        public DateTime Timestamp { get; set; }
        public bool LightIsOn { get; set; }
    }
}
