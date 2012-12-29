using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ploeh.Samples.Booking.DomainModel;

namespace Ploeh.Samples.Booking.WebModel
{
    public class BookingController : Controller
    {
        private readonly IReader<DateTime, int> remainingCapacityReader;
        private readonly IChannel<RequestReservationCommand> channel;

        public BookingController(
            IReader<DateTime, int> remainingCapacityReader,
            IChannel<RequestReservationCommand> channel)
        {
            this.remainingCapacityReader = remainingCapacityReader;
            this.channel = channel;
        }

        public ViewResult Get(DateViewModel id)
        {
            var date = id.ToDateTime();
            var remainingCapacity = this.remainingCapacityReader.Query(date);
            return this.View(
                new BookingViewModel
                {
                    Date = id.ToDateTime(),
                    RemainingCapacity = remainingCapacity
                });
        }

        [HttpPost]
        public ViewResult Post(BookingViewModel model)
        {
            this.channel.Send(model.MakeReservation());
            return this.View("Receipt", model);
        }
    }
}