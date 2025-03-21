using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Constant;
using BusinessLogicLayer.Dtos;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer
{
    public class ServiceCommon
    {
        private static ServiceCommon _Instance = null!;
        private static IService<CustomerDto> _CustomerService = null!;

        private ServiceCommon()
        {
            _CustomerService = CustomerService.GetInstance();
        }

        public static ServiceCommon GetInstance() => _Instance ??= new ServiceCommon();

        public static List<CustomerDto> SetCustomerStatusName(List<CustomerDto> customerDtos)
        {
            return customerDtos.Select(x =>
            {
                x.CustomerStatusName = x.CustomerStatus switch
                {
                    1 => HMSDisplayConst.CUSTOMER_ACTIVE,
                    2 => HMSDisplayConst.CUSTOMER_DELETED,
                    _ => string.Empty
                };
                return x;
            }).ToList();
        } 
    }
}
