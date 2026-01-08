using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Users.WebAPI.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;

            if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Title = "Erros de validações.";
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                List<string> validationErrors = new List<string>();

                var errorsDictionary = fluentException.Errors
                        .Where(x => x != null)
                        .GroupBy(
                         x => x.PropertyName,
                         x => x.ErrorMessage,
                         (propertyName, errorMessages) => new
                         {
                             Key = propertyName,
                             Values = errorMessages.Distinct().ToArray()
                         })
                        .ToDictionary(x => x.Key, x => x.Values);

                problemDetails.Extensions.Add("errors", errorsDictionary);
            }
            else if (exception is ArgumentException argumentException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = exception.Message;
            }
            else
            {
                problemDetails.Title = exception.Message;
            }

            logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
