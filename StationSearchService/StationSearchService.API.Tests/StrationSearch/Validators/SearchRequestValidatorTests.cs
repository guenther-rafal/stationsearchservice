using FluentAssertions;
using StationSearchService.StationSearch;
using StationSearchService.StationSearch.Validators;

namespace StationSearchService.API.Tests.StrationSearch.Validators;

[TestFixture]
public class SearchRequestValidatorTests
{
    private SearchRequestValidator _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new SearchRequestValidator();
    }

    [Test]
    public async Task ValidateAsync_ValidObject_ReturnsValidResult()
    {
        var searchRequest = new SearchRequest
        {
            SearchPhrase = "some search phrase with spaces"
        };

        var result = await _sut.ValidateAsync(searchRequest);

        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public async Task ValidateAsync_EmptyPhrase_ReturnsInvalidResult()
    {
        var searchRequest = new SearchRequest
        {
            SearchPhrase = string.Empty
        };

        var result = await _sut.ValidateAsync(searchRequest);

        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public async Task ValidateAsync_WhitespaceOnlyPhrase_ReturnsInvalidResult()
    {
        var searchRequest = new SearchRequest
        {
            SearchPhrase = " "
        };

        var result = await _sut.ValidateAsync(searchRequest);

        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public async Task ValidateAsync_PhraseStartsWithWhitespace_ReturnsInvalidResult()
    {
        var searchRequest = new SearchRequest
        {
            SearchPhrase = " leading space"
        };

        var result = await _sut.ValidateAsync(searchRequest);

        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public async Task ValidateAsync_ContainsIncorrectCharacters_ReturnsInvalidResult()
    {
        var searchRequest = new SearchRequest
        {
            SearchPhrase = Guid.NewGuid().ToString()
        };

        var result = await _sut.ValidateAsync(searchRequest);

        result.IsValid.Should().BeFalse();
    }
}