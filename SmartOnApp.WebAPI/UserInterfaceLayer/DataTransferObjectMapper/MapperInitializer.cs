using System;
using AutoMapper;
using SmartOnApp.Shared.DomainLayer.Models;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjects;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjectMapper
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<IoTDevice, IoTDeviceDTO>().ReverseMap();
            CreateMap<IoTDevice, CreateIoTDeviceDTO>().ReverseMap();
            CreateMap<IoTDevice, UpdateIoTDeviceDTO>().ReverseMap();

            CreateMap<Mcu, McuDTO>().ReverseMap();
            CreateMap<Mcu, CreateMcuDTO>().ReverseMap();
            CreateMap<Mcu, UpdateMcuDTO>().ReverseMap();

            CreateMap<Ldr, LdrDTO>().ReverseMap();
            CreateMap<Ldr, CreateLdrDTO>().ReverseMap();
            CreateMap<Ldr, UpdateLdrDTO>().ReverseMap();

            CreateMap<Light, LightDTO>().ReverseMap();
            CreateMap<Light, CreateLightDTO>().ReverseMap();
            CreateMap<Light, UpdateLightDTO>().ReverseMap();

            CreateMap<Pir, PirDTO>().ReverseMap();
            CreateMap<Pir, CreatePirDTO>().ReverseMap();
            CreateMap<Pir, UpdatePirDTO>().ReverseMap();

            CreateMap<Servo, ServoDTO>().ReverseMap();
            CreateMap<Servo, CreateServoDTO>().ReverseMap();
            CreateMap<Servo, UpdateServoDTO>().ReverseMap();
        }
    }
}
