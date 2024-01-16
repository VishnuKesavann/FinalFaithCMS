using FinalCMS.AdminRepository;
using FinalCMS.AdminViewModel;
using FinalCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AStaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        // construction Injection
        public AStaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;

        }
        #region Get ViewModel staff
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<StaffViewModel>>> GetStaffs()
        {
            return await _staffRepository.GetStaffDetails();
        }

        #endregion
        #region GetStaff By id
        [HttpGet("{staffId}")]
        public async Task<ActionResult<StaffViewModel>> GetStaffDetailsById(int? staffId)
        {
            try
            {
                var staffDetails = await _staffRepository.GetStaffDetailsById(staffId);

                if (staffDetails == null)
                {
                    return NotFound(); // Staff not found, return 404 Not Found
                }

                return Ok(staffDetails); // Return staff details with 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Consider returning a more specific status code or message
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        #endregion



        #region Update staff
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Staff sta)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    await _staffRepository.UpdateStaff(sta);

                    return Ok(sta);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        #endregion
        [HttpPost]

        public async Task<IActionResult> AddStaffWithRelatedData([FromBody] StaffViewModel staffDetails)
        {
            try
            {
                var StaffId = await _staffRepository.AddStaffWithRelatedData(staffDetails);

                return Ok(StaffId);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "Internal Server Error");
            }



        }

    }
}
