using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    //Mapping class between Patients and Medicaments
    public class PatientMedicament
    {
        public PatientMedicament()
        {

        }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
        public ICollection<PatientMedicament> Prescriptions { get; set; } = new HashSet<PatientMedicament>();
    }
}
