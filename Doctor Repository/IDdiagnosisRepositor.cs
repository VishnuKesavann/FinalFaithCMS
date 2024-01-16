using FinalCMS.Doctor_Viewmodel;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Repository
{
    public interface IDdiagnosisRepositor
    {

        Task<int?> FillDiagForm(Diagnosisform diagnosisVM);

    }
}
