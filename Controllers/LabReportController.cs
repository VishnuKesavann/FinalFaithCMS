using FinalCMS.LabRepository;
using FinalCMS.LabViewModel;
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
        //private readonly ILabreportRepository _labreportRepository;
        private readonly ILabTestList _labTestRepository;
        private readonly ILabreportRepository _labReportRepository;

        public LabReportController(ILabreportRepository labreportRepository, ILabTestList labTestRepository)
        {
            _labReportRepository = labreportRepository;
            _labTestRepository = labTestRepository;
        }

       /* private readonly ILabTestList _labTestRepository;
        private readonly ILabreportRepository _labReportRepository;
*/

        // constructor injection

        #region View Model
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabTestVM>>> LabTestsVM()
        {
            return await _labTestRepository.GetViewModelPrescriptions();
        }

        #endregion
        #region Listing
        [HttpGet]
        [Route("List")]

        public async Task<ActionResult<IEnumerable<LabReportVM>>> GetViewModelReport()
        {
            return await _labReportRepository.GetViewModelReport();
        }
        #endregion
        #region Add an employee
        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody] LabReportVM report)
        {
            //check the validation of code
            if (ModelState.IsValid)
            {
                try
                {
                    var ReportId = await _labReportRepository.AddReport(report);
                    if (ReportId > 0)
                    {
                        return Ok(ReportId);
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
            return BadRequest(report);
        }
        #endregion

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<GetIDVM>> GetIDViewModel(int labpresId)
        {
            var idvm= await _labReportRepository.GetIDViewModel(labpresId);
            return Ok(idvm);
        }

        [HttpGet]
        [Route("Bill/{{LabbillId}}")]

        public async Task<ActionResult<LabBillVM>>GetLabBillVm(int LabbillId)
        {
            var idvm =await _labReportRepository.GetBillVM(LabbillId);
            return Ok(idvm);
        }

        
    }
}
