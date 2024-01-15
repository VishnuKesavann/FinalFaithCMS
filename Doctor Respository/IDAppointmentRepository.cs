using FinalCMS.Doctor_ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Respository
{
    public interface IDAppointmentRepository
    {

        Task<List<Todayapp>> GetAppointmentViewAsync(int docId);

    }
}
