using FinalCMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FinalCMS.Repository;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmedicineController : ControllerBase
    {
        private readonly IPharmacistRepository _medicineRepository;

        // construction Injection
        public PmedicineController(IPharmacistRepository employeeRepository)
        {
            _medicineRepository = employeeRepository;
        }
        #region GEt all Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetAllMedicine()
        {
            return await _medicineRepository.GetAllMedicine();
        }
        #endregion


        #region GetDetails By id
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetDetailsById(int? id)
        {
            try
            {
                var med = await _medicineRepository.GetDetailsById(id);
                if (med == null)
                {
                    return NotFound();
                }
                return Ok(med);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Medicine>>> SearchMedicineByName([FromQuery] string name)
        {
            try
            {
                var medicines = await _medicineRepository.SearchMedicineByName(name);
                return Ok(medicines);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
