using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Receptionist_Repository
{
    public interface IAppointmentRepository
    {
        Task<List<Department>> GetAllDepartment();
        Task<List<Specialization>> GetAllSpecializationByDepartmentId(int? departmentId);
    }
}
