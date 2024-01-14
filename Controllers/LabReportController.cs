using FinalCMS.LabRepository;
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
    public class LabReportController : ControllerBase
    {
        private readonly ILabreportRepository _labreportRepository;

        public LabReportController(ILabreportRepository labreportRepository)
        {
            _labreportRepository = labreportRepository;
        }

        #region GEt all LabReport
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<LabReportGeneration>>> GetLabReportAll()
        {
            return await _labreportRepository.GetAllLabReportGeneration();
        }
        #endregion

        #region Add LabReportGeneration
        [HttpPost]
        public async Task<IActionResult> AddLabReport([FromBody] LabReportGeneration labReport)
        {
            if (ModelState.IsValid)  // check the validate the code
            {
                try
                {
                    var LabReportId = await _labreportRepository.AddLabReport(labReport);
                    if (LabReportId > 0)
                    {
                        return Ok(LabReportId);
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

        #region GetLabReport By id
        [HttpGet("{id}")]
        public async Task<ActionResult<LabReportGeneration>> GetLabReportById(int? id)
        {
            try
            {
                var labreport = await _labreportRepository.GetLabReportById(id);
                if (labreport == null)
                {
                    return NotFound();
                }
                return Ok(labreport);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region get all LabBillGeneration
        [HttpGet("GetBill")]
        //[HttpGet]
        //[Route("GetDepartment")]
        public async Task<ActionResult<IEnumerable<LabBillGeneration>>> GetLabBillAll()
        {
            return await _labreportRepository.GetAllLabBillGeneration();
        }
        #endregion
    }
}
