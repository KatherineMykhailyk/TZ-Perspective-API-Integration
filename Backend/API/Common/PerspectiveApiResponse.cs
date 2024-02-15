namespace API.Common;

public class PerspectiveApiResponse
{
    public AttributeScores AttributeScores { get; set; }
    public IList<string> Languages { get; set; }
    public IList<string> DetectedLanguages { get; set; }
}
