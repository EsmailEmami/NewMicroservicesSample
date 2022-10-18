using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.PipeLines;

public sealed class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    // We run a stopwatch on every request and log a warning for any requests that exceed our threshold.

    private readonly Stopwatch _timer;

    private readonly ILogger<TRequest> _logger;
    public PerformanceBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
        _timer = new Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;
            _logger.LogWarning($"Long Running Request: {name} ({_timer.ElapsedMilliseconds} milliseconds) {@request}");
        }

        return response;
    }
}