using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects
{
    public class McuDTO : CreateMcuDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IList<IoTDeviceDTO> IoTDevices { get; set; }
    }
}
