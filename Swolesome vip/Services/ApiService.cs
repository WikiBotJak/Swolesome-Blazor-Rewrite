using System.Text;
using System.Text.Json;

namespace Swolesome_vip.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> SendRequestAsync(
        string term,
        List<string>? filelds = null, 
        bool wildcard = false,
        bool caseSensitive = false)
    {
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
        var contnet = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(targetUrl, contnet);
        var responseData = await response.Content.ReadAsStringAsync();
        
        return responseData;
    }
}