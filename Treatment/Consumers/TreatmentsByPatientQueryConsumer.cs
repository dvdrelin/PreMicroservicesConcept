using Contracts.Queries;
using Data.DbContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Treatment.Consumers;

public class TreatmentsByPatientQueryConsumer:IConsumer<ITreatmentsByPatientQuery>
{
    private readonly ApplicationContext _dbContext;

    public TreatmentsByPatientQueryConsumer(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ITreatmentsByPatientQuery> context)
    {
        var treatments = await _dbContext.Treatments
            .Where(x => x.Patient.PatientId == context.Message.PatientId)
            .Select(x=>new Contracts.Data.Treatment(x.TreatmentId, x.Provider.ProviderId, x.Procedure.ProcedureId, x.Fee))
            .ToArrayAsync(context.CancellationToken);

        await context.RespondAsync<ITreatmentsByPatientQueryResponse>(new
        {
            context.Message.PatientId,
            Treatments = treatments
        });
    }
}