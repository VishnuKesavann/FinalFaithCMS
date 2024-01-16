using FinalCMS.AdminViewModel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public interface IStaffRepository
    {
        Task<List<StaffViewModel>> GetStaffDetails();
        Task<StaffViewModel> GetStaffDetailsById(int? staffId);
        Task UpdateStaff(Staff staff);
        Task<int> AddStaffWithRelatedData(StaffViewModel staffDetails);

    }
}
