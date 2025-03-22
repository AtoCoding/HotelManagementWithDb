using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Bases;
using BusinessLogicLayer.Dtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class ReportService
    {
        private static ReportService _Instance = null!;

        private readonly IService<BookingDetail> _BookingDetailService;
        private ReportService()
        {
            _BookingDetailService = BookingDetailService.GetInstance();
        }

        public static ReportService GetInstance() => _Instance ??= new ReportService();

        public List<ReportDto> GenerateReport(DateOnly start, DateOnly end)
        {
            List<BookingDetail> bookingDetails = _BookingDetailService.GetAll()
                                                 .FindAll(x => x.StartDate >= start && x.EndDate <= end);

            List<ReportDto> reportDtos = new();

            foreach (BookingDetail bookingDetail in bookingDetails)
            {
                reportDtos.Add(new()
                {
                    BookingReservationId = bookingDetail.BookingReservationId,
                    RoomId = bookingDetail.RoomId,
                    CustomerId = bookingDetail.BookingReservation.CustomerId,
                    StartDate = bookingDetail.StartDate,
                    EndDate = bookingDetail.EndDate,
                    TotalPrice = bookingDetail.ActualPrice
                });
            }

            return reportDtos;
        }

        public List<ReportDto> GetAll()
        {
            List<BookingDetail> bookingDetails = _BookingDetailService.GetAll();

            List<ReportDto> reportDtos = new();

            foreach (BookingDetail bookingDetail in bookingDetails)
            {
                reportDtos.Add(new()
                {
                    BookingReservationId = bookingDetail.BookingReservationId,
                    RoomId = bookingDetail.RoomId,
                    CustomerId = bookingDetail.BookingReservation.CustomerId,
                    StartDate = bookingDetail.StartDate,
                    EndDate = bookingDetail.EndDate,
                    TotalPrice = bookingDetail.ActualPrice
                });
            }

            return reportDtos;
        }
    }
}
