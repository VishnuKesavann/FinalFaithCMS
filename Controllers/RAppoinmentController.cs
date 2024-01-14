using FinalCMS.Models;
using FinalCMS.Receptionist_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RAppoinmentController : ControllerBase
    { private readonly IAppointmentRepository _appointmentRepository;
        public RAppoinmentController(IAppointmentRepository appointmentRepository)
        {
                _appointmentRepository = appointmentRepository;
        }
        #region GetAllDepartment
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartment()
        {
            return await _appointmentRepository.GetAllDepartment();
        }
        #endregion
    }
}
