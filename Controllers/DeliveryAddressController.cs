using CrustyCrab.Models;
using CrustyCrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyCrab.Controllers
{
    public class DeliveryAddressController : Controller
    {
        private readonly CrustyApiClient _api;

        public DeliveryAddressController(CrustyApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var addresses = await _api.GetAddresses();
            return addresses == null ? StatusCode(502) : Json(addresses);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var address = await _api.GetAddress(id);
            return address == null ? NotFound() : Json(address);
        }

        [HttpGet]
        public async Task<IActionResult> Customer(int customerId)
        {
            var addresses = await _api.GetCustomerAddresses(customerId);
            return addresses == null ? StatusCode(502) : Json(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliveryAddressRequest request)
        {
            var address = await _api.CreateAddress(request.CustomerID, request.Suburb, request.HouseNumber, request.StreetName, request.BuildingType);
            return address == null ? BadRequest() : Json(address);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] DeliveryAddressUpdateRequest request)
        {
            var address = await _api.UpdateAddress(id, request.Suburb, request.HouseNumber, request.StreetName, request.BuildingType);
            return address == null ? NotFound() : Json(address);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _api.DeleteAddress(id) ? NoContent() : NotFound();
        }
    }
}
