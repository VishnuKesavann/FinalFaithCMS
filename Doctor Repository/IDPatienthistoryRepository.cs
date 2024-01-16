using FinalCMS.Doctor_Viewmodel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Repository
{
    public interface IDPatienthistoryRepository
    {
        Task<List<Patienthis>> GetPatientHistoryAsync(int patientId);

    }
}
