using FluentAssertions;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StationSearchService.Application.StationSearch.Commands;
using StationSearchService.Domain.StationSearch.Errors;
using StationSearchService.StationSearch;
using StationSearchService.StationSearch.Controllers;

namespace StationSearchService.API.Tests.StrationSearch.Controllers;

[TestFixture]
public class StationControllerTests
{
    private StationController _sut;
    private Mock<IMediator> _mediator;
    private Mock<IValidator<SearchRequest>> _searchRequestValidator;

    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _searchRequestValidator = new Mock<IValidator<SearchRequest>>();
        _sut = new StationController(_mediator.Object, _searchRequestValidator.Object);
    }

    [Test]
    public async Task Search_SuccessfulRequest_ReturnsOk()
    {
        var request = new SearchRequest
        {
            SearchPhrase = "some search phrase with spaces"
        };
        var validationResult = new ValidationResult();

        _searchRequestValidator
            .Setup(z => z.ValidateAsync(request, default))
            .ReturnsAsync(validationResult);
        _mediator
            .Setup(z => z.Send(It.IsAny<SearchStationsCommand>(), default))
            .ReturnsAsync(Result.Ok());
        
        var result = await _sut.Search(request, default);

        result.Should().BeOfType<OkObjectResult>();
        _searchRequestValidator.Verify(z => z.ValidateAsync(request, default), Times.Once);
        _mediator.Verify(z => z.Send(It.IsAny<SearchStationsCommand>(), default), Times.Once);
    }
    
    [Test]
    public async Task Search_ValidationFails_ReturnsBadRequest()
    {
        var request = new SearchRequest
        {
            SearchPhrase = "some search phrase with spaces"
        };
        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure());

        _searchRequestValidator
            .Setup(z => z.ValidateAsync(request, default))
            .ReturnsAsync(validationResult);
        _mediator.Setup(z => z.Send(It.IsAny<SearchStationsCommand>(), default));
        
        var result = await _sut.Search(request, default);

        result.Should().BeOfType<BadRequestObjectResult>();
        _searchRequestValidator.Verify(z => z.ValidateAsync(request, default), Times.Once);
        _mediator.Verify(z => z.Send(It.IsAny<SearchStationsCommand>(), default), Times.Never);
    }
    
    [Test]
    public async Task Search_SearchingFails_ReturnsBadRequest()
    {
        var request = new SearchRequest
        {
            SearchPhrase = "some search phrase with spaces"
        };
        var validationResult = new ValidationResult();

        _searchRequestValidator
            .Setup(z => z.ValidateAsync(request, default))
            .ReturnsAsync(validationResult);
        _mediator
            .Setup(z => z.Send(It.IsAny<SearchStationsCommand>(), default))
            .ReturnsAsync(Result.Fail(CorruptedSearchPhraseError.Create()));
        
        var result = await _sut.Search(request, default);

        result.Should().BeOfType<BadRequestObjectResult>();
        _searchRequestValidator.Verify(z => z.ValidateAsync(request, default), Times.Once);
        _mediator.Verify(z => z.Send(It.IsAny<SearchStationsCommand>(), default), Times.Once);
    }
}