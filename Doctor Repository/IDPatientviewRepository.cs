using FinalCMS.Doctor_Viewmodel;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Repository
{
    public interface IDPatientviewRepository
    {

        Task<Patientview> GetPatientViewAsync(int appointmentId);

    }
}
