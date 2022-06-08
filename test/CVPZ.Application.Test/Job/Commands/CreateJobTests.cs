using CVPZ.Application.Job;
using CVPZ.Application.Job.Commands;
using CVPZ.Application.Test.Core;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CVPZ.Application.Test.Job.Commands;

public class CreateJobTests
{
    [Fact]
    public async Task Can_Create_Job()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var request = new CreateJob.Request(
            "Super Awesome Software Consultant",
            "Crema Development LLC {[/]",
            "Awesome position where you get to do awesome things to make client awesome desires happen!",
            DateTimeOffset.Now,
            null);

        var handler = new CreateJob.Handler(ds.Context, mockMediatR.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => {
                Assert.False(string.IsNullOrEmpty(result.Id));
                mockMediatR.Verify(x => x.Publish(new JobEvents.JobCreated(result.Id), CancellationToken.None));
            },
            error => error.Assert());

    }

    [Fact]
    public async Task Should_require_title()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var request = new CreateJob.Request(
            "",
            "Crema Development LLC {[/]",
            "Awesome position where you get to do awesome things to make client awesome desires happen!",
            DateTimeOffset.Now,
            null);

        var handler = new CreateJob.Handler(ds.Context, mockMediatR.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateJob.Errors.JobTitleRequired.Code)
        );
    }

    [Fact]
    public async Task Should_require_employer_name()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var request = new CreateJob.Request(
            "Super Awesome Software Consultant",
            "",
            "Awesome position where you get to do awesome things to make client awesome desires happen!",
            DateTimeOffset.Now,
            null);

        var handler = new CreateJob.Handler(ds.Context, mockMediatR.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateJob.Errors.JobEmployerNameRequired.Code)
        );
    }

    [Fact]
    public async Task Should_require_start_date()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var request = new CreateJob.Request(
            "Super Awesome Software Consultant",
            "Crema Development LLC {[/]",
            "Awesome position where you get to do awesome things to make client awesome desires happen!",
            DateTimeOffset.MinValue, // Nulls aren't allowed so we check for min & max.
            null);

        var handler = new CreateJob.Handler(ds.Context, mockMediatR.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateJob.Errors.JobStartDateRequired.Code)
        );
    }

    [Fact]
    public async Task Should_start_before_ending()
    {
        //Arrange
        using var ds = new DataSource();
        var mockMediatR = new Mock<IMediator>();

        var request = new CreateJob.Request(
            "Super Awesome Software Consultant",
            "Crema Development LLC {[/]",
            "Awesome position where you get to do awesome things to make client awesome desires happen!",
            DateTimeOffset.Now.AddDays(-2),
            DateTimeOffset.Now.AddDays(-5));

        var handler = new CreateJob.Handler(ds.Context, mockMediatR.Object);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateJob.Errors.JobEndDateGreaterThanStartDate.Code)
        );
    }
}
