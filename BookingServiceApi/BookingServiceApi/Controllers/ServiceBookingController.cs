using BookingServiceApi.Data;
using BookingServiceApi.Repo;
using Microsoft.AspNetCore.Mvc;

namespace BookingServiceApi.Controllers
{
    [ApiController]
    public class ServiceBookingController : Controller
    {
        private readonly ILogger<ServiceBookingController> _logger;

        public ServiceBookingController(ILogger<ServiceBookingController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("[controller]/getactiveservicebooking/{customerID}")]
        public ActionResult<List<ServiceBookingRDto>> getactiveServiceBooking(Int64 customerID)
        {
            List<ServiceBookingRDto> result = new List<ServiceBookingRDto>();
            using (ServiceBookingSQL sbRepro = new ServiceBookingSQL())
            {
                result = sbRepro.GetFutureServiceBooking(customerID);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("[controller]/createservicebooking")]
        public ActionResult<string> CreateServiceBooking(ServiceBookingCDto serviceBooking)
        {
            string result = string.Empty;
            using (ServiceBookingSQL sbRepro = new ServiceBookingSQL())
            {
                result = sbRepro.CreateServiceBooking(serviceBooking);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("[controller]/manupdservicebooking")]
        public ActionResult<string> manupdServiceBooking(ServiceBookingUDto serviceBooking)
        {
            string result = string.Empty;
            using (ServiceBookingSQL sbRepro = new ServiceBookingSQL())
            {
                result = sbRepro.ManagerUpdateServiceBooking(serviceBooking);
            }
            return Ok(result);
        }

    }
}
