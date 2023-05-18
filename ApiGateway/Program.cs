using Contracts.Commands;
using Contracts.Queries;
using Data.DbContext;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Procedures.Consumers;
using Treatment.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(cfg => cfg
    .UsingInMemory());
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumer<TreatmentsByPatientQueryConsumer>();
    cfg.AddConsumer<ProceduresQueryConsumer>();
    cfg.AddConsumer<ProceduresCommandConsumer>();
});
builder.Services.AddDbContext<ApplicationContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlProvider")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .MapGet("/procedures",
        async (IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator
                .CreateRequestClient<IProceduresQuery>()
                .GetResponse<IProceduresQueryResponse>(new {  }, cancellationToken);
            return response.Message.Procedures;
        })

    .WithName("procedures")
    .WithOpenApi();
app
    .MapPost("/procedure",
        async ([FromBody]string item,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator
                .CreateRequestClient<IProcedureAddCommand>()
                .GetResponse<IProcedureAddCommandResponse>(new { Item=item }, cancellationToken);
            return response.Message.ProcedureId;
        })

    .WithName("procedure-add")
    .WithOpenApi();

app
    .MapGet("/treatment/{patientId}",
        async (Guid patientId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var response = await mediator
                .CreateRequestClient<ITreatmentsByPatientQuery>()
                .GetResponse<ITreatmentsByPatientQueryResponse>(new { PatientId = patientId }, cancellationToken);
            return response.Message.Treatments;
        })

    .WithName("treatment")
    .WithOpenApi();

app
    .MapGet("/guid/", Guid.NewGuid)
    .WithName("guid")
    .WithOpenApi();

app.Run();