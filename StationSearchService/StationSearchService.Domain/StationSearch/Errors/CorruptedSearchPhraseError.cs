using FluentResults;
using StationSearchService.Domain.StationSearch.Constants;

namespace StationSearchService.Domain.StationSearch.Errors;

public sealed class CorruptedSearchPhraseError : Error
{
    private CorruptedSearchPhraseError(string message)
        : base(message) { }

    public static CorruptedSearchPhraseError Create() => new(ErrorMessages.CorruptedSearchPhraseErrorMessage);
}