using System;
namespace SmartOnApp.Shared.DomainLayer.Models
{
    public class Servo
    {
        public DateTime Timestamp { get; set; }
        public int ServoCurrentPosition { get; set; }
    }
}
