using System.Text.Json;
using API.Common;

public interface IAnalyzeCommentService
{
    Task<object> Analyze(string text);
}

public class AnalyzeCommentService : IAnalyzeCommentService
{
    private readonly HttpClient _httpClient;
    private readonly string _discoveryUrl;
    
    public AnalyzeCommentService(HttpClient httpClient, string discoveryUrl)
    {
        _httpClient = httpClient;
        _discoveryUrl = discoveryUrl;
    }
    
    public async Task<object> Analyze(string text)
    {
        var request = new
        {
            comment = new
            {
                text = text
            },
            languages = new[] { "en" },
            requestedAttributes = new
            {
                TOXICITY = new { }
            }
        };

        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_discoveryUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Perspective API request failed with status code {response.StatusCode}.");
            }
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var perspectiveApiResponse = JsonSerializer.Deserialize<PerspectiveApiResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            
            return perspectiveApiResponse?.AttributeScores?.Toxicity?.SummaryScore?.Value ?? 0.0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}