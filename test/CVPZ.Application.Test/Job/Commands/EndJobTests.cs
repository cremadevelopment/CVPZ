using CVPZ.Application.Job;
using CVPZ.Application.Test.Core;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CVPZ.Application.Test.Job.Commands;

public class EndJobTests
{
    [Fact]
    public async Task Can_end_job()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var entity = new Domain.Job
        {
            UserId = new Guid("32bfe497-4100-4351-a786-00d68e5af561"),
            Title = "Super Awesome Software Consultant",
            EmployerName = "Crema Development LLC {[/]",
            Description = "Awesome position where you get to do awesome things to make client awesome desires happen!",
            StartDate = DateTimeOffset.Now.AddDays(-365)
        };

        await ds.Context.AddAsync(entity);
        await ds.Context.SaveChangesAsync();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var request = new EndJob.Request(JobId: entity.Id.ToString(), EndDate: DateTimeOffset.Now);
        
        request.SetUserId(entity.UserId);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => {
                Assert.True(result.JobId == entity.Id.ToString());
                mockMediatR.Verify(x => x.Publish(new JobEvents.JobEnded(result.JobId), CancellationToken.None));
            },
            error => error.Assert());
    }

    [Fact]
    public async Task Should_error_for_UserId_not_matched() 
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var entity = new Domain.Job
        {
            UserId = new Guid("7739a003-611b-4164-abb5-9964139f206b"),
            Title = "Super Awesome Software Consultant",
            EmployerName = "Crema Development LLC {[/]",
            Description = "Awesome position where you get to do awesome things to make client awesome desires happen!",
            StartDate = DateTimeOffset.Now.AddDays(-365)
        };

        await ds.Context.AddAsync(entity);
        await ds.Context.SaveChangesAsync();

        var request = new EndJob.Request(JobId: entity.Id.ToString(), EndDate: DateTime.Now);

        var diffGuid = new Guid("32bfe497-4100-4351-a786-00d68e5af561");

        request.SetUserId(diffGuid);

        //Act
        Console.WriteLine(request);
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == EndJob.Errors.UserIdNotValid.Code)
        );

    }

    [Fact]
    public async Task Should_error_when_not_found()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var request = new EndJob.Request(JobId: 123.ToString(), EndDate: DateTimeOffset.Now);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == EndJob.Errors.JobNotFound.Code)
        );
    }

    [Fact]
    public async Task Must_use_valid_id()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var request = new EndJob.Request(JobId: "ABC", EndDate: DateTimeOffset.Now);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == EndJob.Errors.JobIdNotValid.Code)
        );
    }

    [Fact]
    public async Task Must_require_end_date()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var entity = new Domain.Job
        {
            Title = "Super Awesome Software Consultant",
            EmployerName = "Crema Development LLC {[/]",
            Description = "Awesome position where you get to do awesome things to make client awesome desires happen!",
            StartDate = DateTimeOffset.Now.AddDays(-365)
        };

        await ds.Context.AddAsync(entity);
        await ds.Context.SaveChangesAsync();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var request = new EndJob.Request(JobId: entity.Id.ToString(), EndDate: DateTimeOffset.MinValue);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == EndJob.Errors.JobEndDateRequired.Code)
        );
    }

    [Fact]
    public async Task Must_start_before_ending()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var entity = new Domain.Job
        {
            Title = "Super Awesome Software Consultant",
            EmployerName = "Crema Development LLC {[/]",
            Description = "Awesome position where you get to do awesome things to make client awesome desires happen!",
            StartDate = DateTimeOffset.Now
        };

        await ds.Context.AddAsync(entity);
        await ds.Context.SaveChangesAsync();

        var handler = new EndJob.Handler(ds.Context, mockMediatR.Object);

        var request = new EndJob.Request(JobId: entity.Id.ToString(), EndDate: DateTimeOffset.Now.AddDays(-1));

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == EndJob.Errors.JobEndDateGreaterThanStartDate.Code)
        );
    }
}
