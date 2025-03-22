using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Constant;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer
{
    public class ServiceCommon
    {
        private static ServiceCommon _Instance = null!;
        private static IService<Customer> _CustomerService = null!;
        public record CustomerStatus(byte CustomerStatusId, string CustomerStatusName);
        public record RoomStatus(byte RoomStatusId, string RoomStatusName);
        public record BookingStatus(byte BookingStatusId, string BookingStatusName);
        private ServiceCommon()
        {
            _CustomerService = CustomerService.GetInstance();
        }

        public static ServiceCommon GetInstance() => _Instance ??= new ServiceCommon();

        public static List<Customer> SetCustomerStatusName(List<Customer> Customers)
        {
            //return Customers.Select(x =>
            //{
            //    x.CustomerStatusName = x.CustomerStatus switch
            //    {
            //        1 => HMSDisplayConst.CUSTOMER_ACTIVE,
            //        2 => HMSDisplayConst.CUSTOMER_DELETED,
            //        _ => string.Empty
            //    };
            //    return x;
            //}).ToList();

            return [];
        }

        public static List<CustomerStatus> GetCustomerStatusName()
        {
            return new()
            {
                new CustomerStatus(1, HMSDisplayConst.CUSTOMER_ACTIVE),
                new CustomerStatus(2, HMSDisplayConst.CUSTOMER_DELETED)
            };
        }

        public static List<RoomStatus> GetRoomStatusName()
        {
            return new()
            {
                new RoomStatus(1, HMSDisplayConst.ROOM_INFORMATION_ACTIVE),
                new RoomStatus(2, HMSDisplayConst.ROOM_INFORMATION_DELETED)
            };
        }

        public static List<BookingStatus> GetBookingStatusName()
        {
            return new()
            {
                new BookingStatus(1, HMSDisplayConst.BOOKING_RESERVATION_ACTIVE),
                new BookingStatus(2, HMSDisplayConst.BOOKING_RESERVATION_PENDING)
            };
        }
    }
}
