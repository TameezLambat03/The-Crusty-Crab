using CrustyCrab.Models;
using CrustyCrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrustyCrab.Controllers
{
    public class AdminStaffController : Controller
    {
        private readonly CrustyApiClient _api;

        public AdminStaffController(CrustyApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> Staff()
        {
            var staff = await _api.GetStaff();
            return staff == null ? StatusCode(502) : Json(staff);
        }

        [HttpGet]
        public async Task<IActionResult> GetStaff(int id)
        {
            var staff = await _api.GetStaff(id);
            return staff == null ? NotFound() : Json(staff);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] StaffRequest request)
        {
            var staff = await _api.CreateStaff(request.FirstName, request.LastName, request.Role, request.PhoneNumber);
            return staff == null ? BadRequest() : Json(staff);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffRequest request)
        {
            var staff = await _api.UpdateStaff(id, request.FirstName, request.LastName, request.Role, request.PhoneNumber);
            return staff == null ? NotFound() : Json(staff);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            return await _api.DeleteStaff(id) ? NoContent() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Admins()
        {
            var admins = await _api.GetAdmins();
            return admins == null ? StatusCode(502) : Json(admins);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await _api.GetAdmin(id);
            return admin == null ? NotFound() : Json(admin);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequest request)
        {
            var admin = await _api.CreateAdmin(request.Username, request.EmailAddress, request.Password, request.StaffId);
            return admin == null ? BadRequest() : Json(admin);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AdminUpdateRequest request)
        {
            var admin = await _api.UpdateAdmin(id, request.Username, request.EmailAddress, request.StaffId);
            return admin == null ? BadRequest() : Json(admin);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            return await _api.DeleteAdmin(id) ? NoContent() : NotFound();
        }
    }
}
