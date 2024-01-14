using FinalCMS.AdminViewModel;
using FinalCMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalCMS.AdminRepository
{
    public interface IStaffRepository
    {
        //list the staff
        Task<List<StaffViewModel>> GetViewModelEmployees();
        //add new staff
        Task<int> AddStaff(Staff staff);
        Task UpdateStaff(Staff staff);



    }
}
