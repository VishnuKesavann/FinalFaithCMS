using FinalCMS.Models;
using FinalCMS.Receptionist_ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Receptionist_Repository
{
    public interface IAppointmentRepository
    {
        Task<List<Department>> GetAllDepartment();
        Task<List<Specialization>> GetAllSpecializationByDepartmentId(int? departmentId);
        Task<List<DoctorViewModel>> GetAllDoctorBySpecializationId(int? specializationId);
    }
}
