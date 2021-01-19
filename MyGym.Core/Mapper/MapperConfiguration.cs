using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Model = MyGym.Core.Model;

namespace MyGym.Core.Mapper
{
    public class MapperConfiguration
    {
        public static IMapper Mapper { get; private set; }

        public static void Initialize()
        {
            var config = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.CreateMap<Entity.SaveCustomerRequest, Model.Customer>();
                mc.CreateMap<Model.Customer, Entity.CustomerResponse>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
