using System;
using System.Collections.Generic;

namespace SmartOnApp.Shared.DomainLayer.Models
{
	public class Mcu
	{
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string McuName { get; set; }
        public string McuMacAddress { get; set; }
        public string McuIp { get; set; }
        public string McuHostname { get; set; }
        public string Location { get; set; }

        public IList<IoTDevice> IoTDevices { get; set; }
    }
}

