using FluentValidation;
using System.Collections.ObjectModel;

namespace WeMediatCrud;
public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TResponse : class
where TRequest :IRequest<TResponse>, IValidateable
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<TRequest> _logger;

    public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        /* var failures = _validators.Select(x => x.Validate(context))
             .SelectMany(x => x.Errors)
             .Where(x => x != null)
             .ToList();*/
        //var res=await (_validators.FirstOrDefault()?.ValidateAsync(context, cancellationToken)).ConfigureAwait(false);

        var tasks = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context,cancellationToken)));
        var failures = tasks.SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();
        /*var failures = _validators.Select(async x => await (x.ValidateAsync(context, cancellationToken)).ConfigureAwait(false))
            .SelectMany(x => x.Result.Errors)
            .Where(x => x != null)
            .ToList();*/
        //failures.AddRange(fails);
        if (failures.Any())
        {
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(ValidateableResponse<>).MakeGenericType(resultType);

                var invalidResponse =
                    Activator.CreateInstance(invalidResponseType, null, failures.Select(s => s.ErrorMessage).ToList()) as TResponse;

                return invalidResponse;
            }
        }



        var response = await next();

        return response;
    }
}

public class ValidateableResponse
{
    private readonly IList<string> _errorMessages;

    public ValidateableResponse(IList<string> errors = null)
    {
        _errorMessages = errors ?? new List<string>();
    }

    public bool IsValidResponse => !_errorMessages.Any();

    public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
}

public class ValidateableResponse<TModel> : ValidateableResponse
    where TModel : class
{
    public ValidateableResponse():this(default(TModel))
    {

    }
    public ValidateableResponse(TModel model, IList<string> validationErrors = null)
        : base(validationErrors)
    {
        Result = model;
    }

    public TModel Result { get; }
}

public interface IValidateable
{

}

