namespace MedicineReminderProj.Models;

public class Treatment : AzureCosmosDBResponse
{
    public string MedicineName { get; set; }
    public string Dosage { get; set; }
    public string PatientName { get; set; }
}
