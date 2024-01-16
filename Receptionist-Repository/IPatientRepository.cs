using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Receptionist_Repository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatient();
        Task<int> AddPatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task<Patient> GetPatientById(int? id);
        Task<Patient> DisableStatus(int? paitientId);
        Task<List<Patient>> GetAllDisabledPatients();
    }
}
