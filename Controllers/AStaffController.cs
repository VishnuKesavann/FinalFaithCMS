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
        //construction injection
        public AStaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        #region get staff 

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<StaffViewModel>>> Getemployeeview()
        {
            return await _staffRepository.GetViewModelEmployees();
        }

        #endregion

        #region Add staff
        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] Staff staff)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    var staffId = await _staffRepository.AddStaff(staff);
                    if (staffId > 0)
                    {
                        return Ok(staffId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        #endregion


        #region Update Medicine
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

    }
}
