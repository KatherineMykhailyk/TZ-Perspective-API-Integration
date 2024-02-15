using API.Common;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase 
{
    private readonly IAnalyzeCommentService _analyzeCommentService;
    
    public CommentController(IAnalyzeCommentService analyzeCommentService)
    {
        _analyzeCommentService = analyzeCommentService;
    }
    
    [HttpPost("analyze")]
    public async Task<IActionResult> AnalyzeComment([FromBody] AnalyzeText request) 
    {
        if (string.IsNullOrEmpty(request.Text)) 
        {
            return BadRequest("The 'Text' field cannot be empty"); 
        }
        
        var toxicityScore = await _analyzeCommentService.Analyze(request.Text);

        return Ok(new { ToxicityScore = toxicityScore }); 
    }
}
