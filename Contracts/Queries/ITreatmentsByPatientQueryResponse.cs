using Contracts.Data;
namespace Contracts.Queries;
public interface ITreatmentsByPatientQueryResponse
{
    Guid PatientId { get; set; }
    Treatment[] Treatments { get; set; }
}