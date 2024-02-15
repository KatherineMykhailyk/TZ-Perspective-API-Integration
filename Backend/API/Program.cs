var builder = WebApplication.CreateBuilder(args);

ConfigureApplicationBuilder(builder);

var app = builder.Build();
ConfigureApplication(app);
app.Run();

void ConfigureApplicationBuilder(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IAnalyzeCommentService, AnalyzeCommentService>((provider) =>
    {
        string discoveryUrl = "https://commentanalyzer.googleapis.com/v1alpha1/comments:analyze?key=";
        var httpClient = provider.GetRequiredService<HttpClient>();
        return new AnalyzeCommentService(httpClient, discoveryUrl);
    });

}

void ConfigureApplication(WebApplication app)
{
    app.MapControllers();
}