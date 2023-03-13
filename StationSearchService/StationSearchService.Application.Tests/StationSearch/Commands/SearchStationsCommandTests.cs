using AutoFixture;
using FluentAssertions;
using StationSearchService.Application.StationSearch.Commands;

namespace StationSearchService.Application.Tests.StationSearch.Commands;

[TestFixture]
public sealed class SearchStationsCommandTests
{
    [Test]
    public void Constructor_FillsDesiredFields()
    {
        var fixture = new Fixture();
        var searchPhrase = fixture.Create<string>();
        
        var sut = SearchStationsCommand.Create(searchPhrase);

        sut.SearchPhrase.Should().Be(searchPhrase);
    }
}