using FinalCMS.Doctor_ViewModel;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Respository
{
    public interface IDPatientviewRepository
    {

        Task<PatientDet> GetPatientViewAsync(int appointmentId);

    }
}
