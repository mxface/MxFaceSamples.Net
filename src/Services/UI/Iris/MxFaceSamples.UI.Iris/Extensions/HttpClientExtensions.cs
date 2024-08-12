using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MxFaceSamples.UI.Iris.Extensions;

public static class HttpClientExtensions
{
    public static IHttpClientBuilder AddMFScanKey(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.TryAddTransient<HttpClientClientKeyAuthorizationDelegatingHandler>();

        builder.AddHttpMessageHandler<HttpClientClientKeyAuthorizationDelegatingHandler>();

        return builder;
    }

    public static IHttpClientBuilder AddSubscriptionKey(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.TryAddTransient<HttpClientSubscriptionKeyAuthorizationDelegatingHandler>();

        builder.AddHttpMessageHandler<HttpClientSubscriptionKeyAuthorizationDelegatingHandler>();

        return builder;
    }

    private class HttpClientClientKeyAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public HttpClientClientKeyAuthorizationDelegatingHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClientClientKeyAuthorizationDelegatingHandler(IConfiguration configuration, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            await request.Content.ReadFromJsonAsync<Dictionary<string, string>>(cancellationToken: cancellationToken).ContinueWith(task =>
            {
                var content = task.Result;
                content.Add("ClientKey", _configuration["MxFace:ClientKey"]);
                request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            });

            
            return await base.SendAsync(request, cancellationToken);
        }
    }

    private class HttpClientSubscriptionKeyAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public HttpClientSubscriptionKeyAuthorizationDelegatingHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClientSubscriptionKeyAuthorizationDelegatingHandler(IConfiguration configuration, HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("subscriptionkey", _configuration["MxFace:SubscriptionKey"]);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

