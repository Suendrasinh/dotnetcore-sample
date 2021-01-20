using AutoMapper;
using MyGym.Core.Entity;
using MyGym.Core.Model;

namespace MyGym.Core.Mapper
{
    /// <summary>
    ///     Mapper Configuration
    /// </summary>
    public static class MapperConfiguration
    {
        #region Variable Declaration
        public static IMapper Mapper { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Initialize.
        /// </summary>
        public static void Initialize()
        {
            var config = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.CreateMap<SaveCustomerRequest, Customer>().ReverseMap();
                mc.CreateMap<Customer, CustomerResponse>().ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
        #endregion
    }
}
