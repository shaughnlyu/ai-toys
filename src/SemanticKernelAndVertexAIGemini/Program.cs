using Google.Apis.Auth.OAuth2;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
Kernel kernel = Kernel.CreateBuilder()
    .AddVertexAIGeminiChatCompletion(
        modelId: "gemini-1.5-pro-001",
        bearerTokenProvider: () =>
        {
            return GetBearerToken();
        },
        location: "us-central1",
        projectId: "project-153-424805")
    .Build();

Console.WriteLine(await kernel.InvokePromptAsync("What color is the sky?"));
Console.WriteLine();

static async Task<String> GetBearerToken()
{
    string? credentialFile = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
    if (string.IsNullOrEmpty(credentialFile))
    {
        throw new InvalidOperationException("GOOGLE_APPLICATION_CREDENTIALS environment variable is not set.");
    }

    return await GoogleCredential
        .FromFile(credentialFile)
        .CreateScoped(new[] { "https://www.googleapis.com/auth/cloud-platform" })
        .UnderlyingCredential
        .GetAccessTokenForRequestAsync();
}
