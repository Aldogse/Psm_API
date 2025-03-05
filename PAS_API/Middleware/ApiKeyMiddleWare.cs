namespace PAS_API.Middleware
{
    public class ApiKeyMiddleWare
    {
        private readonly RequestDelegate _next;
        private string _apikey;

        public ApiKeyMiddleWare(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _apikey = configuration["AppSettings:KEY"];
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue("X-API-KEY",out var extracted_api_key))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsJsonAsync("Api key is missing");
                return;
            }

            if (!_apikey.Equals(extracted_api_key))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsJsonAsync("Api key not matched");
                return;
            }

            await _next(httpContext);
        }
    }
}
