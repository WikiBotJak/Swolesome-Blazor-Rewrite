using System.Text;
using System.Text.Json;

namespace Swolesome_vip.Services;

// This is a simple service that sends a request to the API and returns the response as a JsonDocument (breach.vip)
// Note: I had to use your existing proxy script to get this working because of cors. But I plan to add a blazor server api project to act as the proxy instead. 
public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly SearchConfigService _configService;
    
    public ApiService(HttpClient httpClient, SearchConfigService configService)
    {
        _httpClient = httpClient;
        _configService = configService;
    }
    
    public async Task<JsonDocument> SendRequestAsync(
        string term,
        List<string>? filelds = null, 
        bool wildcard = false,
        bool caseSensitive = false)
    {
        int timeout = _configService.Config.Timeout;
        CancellationTokenSource? cts = null;

        if (timeout > 0)
            cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));

        var targetUrl = "http://localhost:8001/";
        var body = new Dictionary<string, object?>
        {
            ["term"] = term,
            ["fields"] = filelds,
            ["wildcard"] = wildcard,
            ["case_sensitive"] = caseSensitive,
            ["_target_url"] = "https://breach.vip/api/search"
        };

        var jsonBody = JsonSerializer.Serialize(body);
        using var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        
        using var response = await _httpClient.PostAsync(
            targetUrl,
            content,
            cts?.Token ?? CancellationToken.None
        );

        await using var responseStream = await response.Content.ReadAsStreamAsync(cts?.Token ?? CancellationToken.None);
        return await JsonDocument.ParseAsync(responseStream, cancellationToken: cts?.Token ?? CancellationToken.None);
    }
}