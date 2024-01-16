using FinalCMS.Doctor_Viewmodel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Repository
{
    public interface IDAppointmentRepository
    {

        Task<List<Todayapp>> GetAppointmentViewAsync(int docId);

    }
}
