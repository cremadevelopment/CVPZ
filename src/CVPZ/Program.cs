using CVPZ.Application;
using CVPZ.Application.Configuration.Queries.GetUserInfo;
using CVPZ.Application.Job;
using CVPZ.Core;
using CVPZ.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddCore();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.ToString().Replace("+", ""));
});
//builder.Services.ConfigureSwaggerGen(options => options.CustomSchemaIds(x => x.FullName.Replace("+", "_")));

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("GetUserInfo", async ([FromServices] IMediator mediator) => {
    return await mediator.Send(new GetUserInfo());
}).Produces<UserInfo>();

app.MapPost("CreateJob", async ([FromServices] IMediator mediator, CreateJob.Request request) => {
    var result = await mediator.Send(request);
    return result.Match(
        response => Results.Ok(response),
        error => Results.BadRequest(error)
    );
}).Produces<CreateJob.Response>();

app.MapPost("EndJob", async ([FromServices] IMediator mediator, EndJob.Request request) =>
{
    var response = await mediator.Send(request);
    return response;
}).Produces<EndJob.Response>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
