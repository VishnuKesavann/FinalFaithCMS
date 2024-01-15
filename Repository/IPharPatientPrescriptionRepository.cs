using FinalCMS.Models;
using FinalCMS.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Repository
{
    public interface IPharPatientPrescriptionRepository
    {
        Task<List<PharmacistViewModel>> GetAllPatientPrescriptions();

        Task<List<PharmacistViewModel>> SearchPatientPrescriptionsByPatientId(int? patientId);






    }
}
