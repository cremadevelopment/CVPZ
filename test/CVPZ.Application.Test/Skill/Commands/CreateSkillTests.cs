using CVPZ.Application.Skill.Commands;
using CVPZ.Application.Test.Core;
using CVPZ.Domain;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CVPZ.Application.Test.Skill.Commands;

public class CreateSkillTests
{
    [Fact]
    public async Task Can_Create_Skill()
    {
        //Arrange
        using var ds = new DataSource();

        var request = new CreateSkill.Request(
                "NeverBeenDone",
                "NameOfSkill",
                "Super Awesome Test No One Will Read",
                SkillLevel.Expert
            );

        var handler = new CreateSkill.Handler(ds.Context);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => {
                Assert.False(string.IsNullOrEmpty(result.Id));
            },
            error => error.Assert());
    }

    [Fact]
    public async Task Skill_should_have_Category()
    {
        //Arrange
        using var ds = new DataSource();

        var request = new CreateSkill.Request(
                null,
                "NameOfSkill",
                "Super Awesome Test No One Will Read",
                SkillLevel.Expert
            );

        var handler = new CreateSkill.Handler(ds.Context);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateSkill.Errors.CategoryRequired.Code)
        );
    }

    [Fact]
    public async Task Skill_should_have_Name()
    {
        //Arrange
        using var ds = new DataSource();

        var request = new CreateSkill.Request(
                "NeverBeenDone",
                null,
                "Super Awesome Test No One Will Read",
                SkillLevel.Expert
            );

        var handler = new CreateSkill.Handler(ds.Context);

        //Act
        var response = await handler.Handle(request, CancellationToken.None);

        //Assert
        response.Switch(
            result => Assert.Null(result),
            error => Assert.True(error.Code == CreateSkill.Errors.NameRequired.Code)
        );
    }

}
