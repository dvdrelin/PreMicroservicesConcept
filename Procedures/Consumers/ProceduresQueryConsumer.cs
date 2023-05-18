using Contracts.Queries;
using Data.DbContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Procedures.Consumers;

public class ProceduresQueryConsumer :
    IConsumer<IProceduresQuery>
{
    private readonly ApplicationContext _dbContext;

    public ProceduresQueryConsumer(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<IProceduresQuery> context)
    {
        var procedures = await _dbContext.Procedures
            .Select(x => new Contracts.Data.Procedure(x.ProcedureId, x.Item))
            .ToArrayAsync(context.CancellationToken);

        await context.RespondAsync<IProceduresQueryResponse>(new
        {
            Procedures = procedures
        });
    }
}