using FluentValidation;

namespace StationSearchService.StationSearch.Validators;

// ReSharper disable once ClassNeverInstantiated.Global
public class SearchRequestValidator : AbstractValidator<SearchRequest>
{
    public SearchRequestValidator()
    {
        RuleFor(z => z.SearchPhrase)
            .NotEmpty()
            .WithMessage("Search phrase cannot be empty.");
        RuleFor(z => z.SearchPhrase)
            .Matches("^[A-Za-z ]+$")
            .WithMessage("Search phrase can only contain letters and spaces.");
        RuleFor(z => z.SearchPhrase)
            .Must(z => !z.StartsWith(' '))
            .WithMessage("Search phrase cannot start with a space.");
    }
}