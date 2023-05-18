using Contracts.Commands;
using Data.DbContext;
using Data.Model;
using MassTransit;

namespace Procedures.Consumers;

public class ProceduresCommandConsumer :
    IConsumer<IProcedureAddCommand>

{
    private readonly ApplicationContext _dbContext;

    public ProceduresCommandConsumer(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<IProcedureAddCommand> context)
    {
        var newProcedure = new Procedure { Item = context.Message.Item, ProcedureId = Guid.NewGuid() };

        _dbContext.Procedures.Add(newProcedure);

        await _dbContext.SaveChangesAsync(context.CancellationToken);

        await context.RespondAsync<IProcedureAddCommandResponse>(new { newProcedure.ProcedureId });
    }
}