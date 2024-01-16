using FinalCMS.Doctor_Viewmodel;
using FinalCMS.Models;
using System.Threading.Tasks;

namespace FinalCMS.Doctor_Repository
{
    public class DdiagnosisRepository : IDdiagnosisRepositor
    {

        private readonly FinalCMS_dbContext _dbcontext;
        public DdiagnosisRepository(FinalCMS_dbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }



        public async Task<int?> FillDiagForm(Diagnosisform diagnosisVM)
        {
            var diagnosis = new Diagnosis
            {
                Symptoms = diagnosisVM.Symptoms ?? "",
                Diagnosis1 = diagnosisVM.Diagnosis1 ?? "",
                Note = diagnosisVM.Note ?? "",
                AppointmentId = diagnosisVM.AppointmentId
            };

            _dbcontext.Diagnosis.Add(diagnosis);
            await _dbcontext.SaveChangesAsync();

            // Insert into tbl_Medicineprescription
            var medicinePrescription = new MedicinePrescriptions
            {
                PrescribedMedicineId = diagnosisVM.PrescribedMedicineId,
                Dosage = diagnosisVM.Dosage ?? "",
                DosageDays = diagnosisVM.DosageDays,
                MedicineQuantity = diagnosisVM.MedicineQuantity,
                AppointmentId = diagnosisVM.AppointmentId
            };

            _dbcontext.MedicinePrescriptions.Add(medicinePrescription);
            await _dbcontext.SaveChangesAsync();

            // Insert into tbl_LabPrescription
            var labPrescription = new LabPrescriptions
            {
                LabTestId = diagnosisVM.LabTestId,
                LabNote = diagnosisVM.LabNote ?? "",
                AppointmentId = diagnosisVM.AppointmentId,
                LabTestStatus = diagnosisVM.LabTestStatus
            };

            _dbcontext.LabPrescriptions.Add(labPrescription);
            await _dbcontext.SaveChangesAsync();

            return diagnosisVM.AppointmentId;
        }

    }
}
