using FluentResults;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StationSearchService.Common.Extensions;

public static class ModelStateExtensions
{
    public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult result) 
    {
        foreach (var error in result.Errors) 
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

    public static ModelStateDictionary AddErrors(this ModelStateDictionary modelState, ICollection<IError> errors)
    {
        foreach (var error in errors)
        {
            modelState.AddModelError(string.Empty, error.Message);
        }
        return modelState;
    }
}