using AutoFixture;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StationSearchService.Common.Extensions;

namespace StationSearchService.API.Tests.Common.Extensions;

[TestFixture]
public class ModelStateExtensionsTests
{
    [Test]
    public void AddValidationResult_ValidationFailed_ErrorsAdded()
    {
        var modelState = new ModelStateDictionary();
        var validationResult = new ValidationResult();
        var fixture = new Fixture();
        validationResult.Errors.AddRange(fixture.CreateMany<ValidationFailure>());
        
        modelState.AddValidationResult(validationResult);

        modelState.ErrorCount.Should().Be(validationResult.Errors.Count);
    }
}