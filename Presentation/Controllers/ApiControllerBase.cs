using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SharedKernel.Result;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IStringLocalizer<ApiControllerBase> _localizer =>
        HttpContext.RequestServices.GetService<IStringLocalizer<ApiControllerBase>>();

    protected IActionResult Problem(List<Error> errors)
    {
        // If there are no errors, return a generic problem
        if (errors.Count is 0)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; // Set status code directly
            return CreateProblem("An unknown error occurred.");
        }

        // If all errors are validation errors, return a validation problem
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items["errors"] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        // Set the status code based on the error type
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        HttpContext.Response.StatusCode = statusCode; // Set status code directly

        // Return a ProblemDetails object with the localized error description
        return CreateProblem(_localizer[error.Description]);
    }

    private IActionResult CreateProblem(string title)
    {
        var problemDetails = new ProblemDetails
        {
            Status = HttpContext.Response.StatusCode,
            Title = title
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                _localizer[error.Description]);
        }

        return ValidationProblem(modelStateDictionary);
    }
}
