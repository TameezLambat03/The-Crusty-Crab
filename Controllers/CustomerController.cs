using CrustyCrab.Models;
using CrustyCrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyCrab.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CrustyApiClient _api;

        public CustomerController(CrustyApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _api.GetCustomers();
            return customers == null ? StatusCode(502) : Json(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _api.GetCustomer(id);
            return customer == null ? NotFound() : Json(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequest request)
        {
            var customer = await _api.CreateCustomer(request.FirstName, request.LastName, request.PhoneNumber, request.EmailAddress, request.Password);
            return customer == null ? BadRequest() : Json(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpdateRequest request)
        {
            var customer = await _api.UpdateCustomer(id, request.FirstName, request.LastName, request.PhoneNumber, request.EmailAddress);
            return customer == null ? BadRequest() : Json(customer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return await _api.DeleteCustomer(id) ? NoContent() : NotFound();
        }
    }
}
