namespace Contracts.Queries;
public interface ITreatmentsByPatientQuery
{
    Guid PatientId { get; set; }
}