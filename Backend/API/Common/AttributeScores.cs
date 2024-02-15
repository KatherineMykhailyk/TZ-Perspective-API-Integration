using System.Text.Json.Serialization;

namespace API.Common;

public class AttributeScores
{
    [JsonPropertyName("TOXICITY")]
    public ToxicityAttribute Toxicity { get; set; }
}