namespace Contracts.Data;

public record Treatment(Guid TreatmentId, Guid ProviderId, Guid ItemId, decimal Fee);