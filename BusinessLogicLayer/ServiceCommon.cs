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
        public record CustomerStatus(byte CustomerStatusId, string CustomerStatusName);
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

        public static List<CustomerStatus> GetCustomerStatusName()
        {
            return new()
            {
                new CustomerStatus(1, HMSDisplayConst.CUSTOMER_ACTIVE),
                new CustomerStatus(2, HMSDisplayConst.CUSTOMER_DELETED)
            };
        }
    }
}
