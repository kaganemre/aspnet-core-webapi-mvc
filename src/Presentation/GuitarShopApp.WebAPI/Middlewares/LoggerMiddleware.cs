using System.Text;

namespace GuitarShopApp.WebAPI.Middlewares;

public class LoggerMiddleware : IMiddleware
{
    public readonly ILogger _logger;

    public LoggerMiddleware(ILogger<LoggerMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = await FormatRequest(context.Request);

        _logger.LogInformation(request);

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();

        context.Response.Body = responseBody;

        await next(context);

        var response = await FormatResponse(context.Response);

        _logger.LogInformation(response);

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();

        var backupBody = request.Body;

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        var requestBody = Encoding.UTF8.GetString(buffer);
        backupBody.Seek(0, SeekOrigin.Begin);
        request.Body = backupBody;

        string requestText = $"Request Host : {request.Host} Request Path : {request.Path} Query String : {request.QueryString} Request Body : {requestBody}";

        return requestText;
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        string responseBody = await new StreamReader(response.Body).ReadToEndAsync();

        response.Body.Seek(0, SeekOrigin.Begin);

        string responseText = $"Response Body : {Encoding.UTF8.GetString(Encoding.Default.GetBytes(responseBody))}";

        return responseText;
    }
}