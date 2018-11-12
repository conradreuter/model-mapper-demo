using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ModelMapperDemo.GraphQL;
using System;

namespace ModelMapperDemo.Middlewares
{
    /// <summary>
    /// Handles GraphQL requests.
    /// </summary>
    public class GraphQLMiddleware : IMiddleware
    {
        private delegate Task ExecuteRequest(HttpResponse response, CancellationToken cancellationToken);

        private readonly Func<GraphQLExecutionContext> _createExecutionContext;
        private readonly ISchema _schema;

        public GraphQLMiddleware(
            Func<GraphQLExecutionContext> createExecutionContext,
            ISchema schema)
        {
            _createExecutionContext = createExecutionContext;
            _schema = schema;
        }

        async Task IMiddleware.InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            var executeRequest = await ParseRequestAsync(httpContext.Request);
            await executeRequest(httpContext.Response, httpContext.RequestAborted);
        }

        private async Task<ExecuteRequest> ParseRequestAsync(HttpRequest request)
        {
            if (request.Method != "POST")
            {
                return Error(
                    HttpStatusCode.MethodNotAllowed,
                    ("Allow", "POST"));
            }
            if (request.ContentType != "application/json")
            {
                return Error(
                    HttpStatusCode.UnsupportedMediaType,
                    ("Accept", "application/json"));
            }

            string body;
            using (var reader = new StreamReader(request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            Parameters parameters;
            try
            {
                parameters = JsonConvert.DeserializeObject<Parameters>(body);
            }
            catch
            {
                return Error(HttpStatusCode.BadRequest);
            }

            return Execute(parameters);
        }

        private static ExecuteRequest Error(
            HttpStatusCode statusCode,
            params (string key, StringValues values)[] headers)
        {
            return (response, cancellationToken) =>
            {
                response.StatusCode = (int)statusCode;
                foreach (var (key, values) in headers)
                {
                    response.Headers.Add(key, values);
                }
                return Task.FromResult(0);
            };
        }

        private ExecuteRequest Execute(Parameters parameters)
        {
            return async (response, cancellationToken) =>
            {
                var executionContext = _createExecutionContext();
                var result = await new DocumentExecuter().ExecuteAsync(new ExecutionOptions
                {
                    CancellationToken = cancellationToken,
                    // TODO: ComplexityConfiguration
                    ExposeExceptions = true, // TODO: only in dev-environment?
                    Inputs = parameters.Variables.ToInputs(),
                    OperationName = parameters.OperationName,
                    Query = parameters.Query,
                    Schema = _schema,
                    UserContext = executionContext,
                });

                var statusCode = result.Errors?.Count > 0 ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
                response.StatusCode = (int)statusCode;
                response.ContentType = "application/json";

                var json = new DocumentWriter(indent: true /* TODO: dev? */).Write(result);
                await response.WriteAsync(json, cancellationToken);
            };
        }

        private class Parameters
        {
            public string OperationName { get; set; }
            public string Query { get; set; }
            public JObject Variables { get; set; }
        }
    }
}
