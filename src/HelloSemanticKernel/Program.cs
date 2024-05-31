using Microsoft.SemanticKernel;

string deploymentName = "gpt-4o";
string? endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
string? apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("The endpoint or API key is not set in the environment variables.");
    return;
}

Kernel kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(
        deploymentName: deploymentName,
        endpoint: endpoint,
        apiKey: apiKey)
    .Build();

Console.WriteLine(await kernel.InvokePromptAsync("What color is the sky?"));
Console.WriteLine();


