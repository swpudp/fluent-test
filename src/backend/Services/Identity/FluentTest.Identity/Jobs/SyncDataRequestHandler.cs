using MediatR;
using Microsoft.Extensions.Logging;

namespace FluentTest.Identity.Jobs
{
    public class SyncDataRequestHandler(ILogger<SyncDataRequestHandler> logger) : IRequestHandler<SyncDataRequest>
    {
        private readonly ILogger _logger = logger;
        public Task Handle(SyncDataRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(request.ToString(), cancellationToken);
            return Task.CompletedTask;
        }
    }
}
