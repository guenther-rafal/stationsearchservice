using AutoFixture;
using FluentAssertions;
using FluentResults;
using Moq;
using StationSearchService.Application.StationSearch.Commands;
using StationSearchService.Application.StationSearch.Commands.Handlers;
using StationSearchService.Application.Tests.StationSearch.SpecimenBuilders;
using StationSearchService.Domain.StationSearch.Interfaces;
using StationSearchService.Domain.StationSearch.ValueObjects;

namespace StationSearchService.Application.Tests.StationSearch.Commands.Handlers;

[TestFixture]
public sealed class SearchStationsCommandHandlerTests
{
    private SearchStationsCommandHandler _sut;
    private Mock<IStations> _stations;

    [SetUp]
    public void Setup()
    {
        _stations = new Mock<IStations>();
        _sut = new SearchStationsCommandHandler(_stations.Object);
    }

    [Test]
    public async Task Handle_SearchSuccessful_ReturnsOk()
    {
        var fixture = new Fixture();
        var searchPhrase = fixture.Create<string>();
        var request = SearchStationsCommand.Create(searchPhrase);
        fixture.Customizations.Add(new SearchDisplaySpecimenBuilder());
        var expectedResult = fixture.Create<SearchDisplay>();

        _stations.Setup(z => z.Search(request.SearchPhrase)).Returns(Result.Ok(expectedResult));
        
        var result = await _sut.Handle(request, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedResult);
        _stations.Verify(z => z.Search(request.SearchPhrase), Times.Once);
    }
    
    [Test]
    public async Task Handle_SearchFails_ReturnsFail()
    {
        var fixture = new Fixture();
        var searchPhrase = fixture.Create<string>();
        var request = SearchStationsCommand.Create(searchPhrase);

        _stations.Setup(z => z.Search(request.SearchPhrase)).Returns(Result.Fail(string.Empty));
        
        var result = await _sut.Handle(request, default);

        result.IsSuccess.Should().BeFalse();
        _stations.Verify(z => z.Search(request.SearchPhrase), Times.Once);
    }
}