namespace Property_And_Supply_Management_API.Services
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private string? _apiKey;
        public ApiKeyMiddleware( RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["AppSettings:Key"];
        }

        public async Task Invoke(HttpContext _httpContext)
        {
            try
            {
                if (!_httpContext.Request.Headers.TryGetValue("X-API-KEY", out var extracted_api_key))
                {
                    _httpContext.Response.StatusCode = 401;
                    await _httpContext.Response.WriteAsync("No Api key included");
                    return;
                }

                if (_apiKey == null)
                {
                    throw new UnauthorizedAccessException("Key not found");
                }

                if (!_apiKey.Equals(extracted_api_key))
                {
                    _httpContext.Response.StatusCode = 401;
                    await _httpContext.Response.WriteAsync($"{extracted_api_key} is not matched to out key");
                    return;
                }
                await _next(_httpContext);
            }
            catch (Exception ex)
            {
                throw  new Exception($"Error : {ex.Message}");
            }
        }
    }
}
 