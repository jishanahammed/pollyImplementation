using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace RndRequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
     //   public AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy { get; }


        public ClientPolicy()
        {
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode).RetryAsync(5);

            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

          //var   CircuitBreakerPolicy = Policy
          //      .Handle<HttpRequestException>()
          //      .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        }
    }
}
