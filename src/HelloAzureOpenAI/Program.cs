using Azure;
using Azure.AI.OpenAI;
using static System.Environment;


// Retrieve and verify environment variables
string? GetEnvironmentVariableAndValidate(string variableName)
{
    string? variableValue = GetEnvironmentVariable(variableName);
    if (variableValue == null)
    {
        Console.WriteLine($"Please set the {variableName} environment variable.");
    }
    return variableValue;
}

string? endpoint = GetEnvironmentVariableAndValidate("AZURE_OPENAI_ENDPOINT");
string? apikey = GetEnvironmentVariableAndValidate("AZURE_OPENAI_API_KEY");

// If the environment variable is invalid, terminate the program.
if (endpoint == null || apikey == null)
{
    return;
}

// Create OpenAI Client
OpenAIClient client = new (new Uri(endpoint), new AzureKeyCredential(apikey));

// Set Chat Options
ChatCompletionsOptions chatCompletionsOptions = new()
{
    DeploymentName = "gpt-4o",
    Messages =
    {
        new ChatRequestSystemMessage("You are a helpful assistant."),
        new ChatRequestUserMessage("Does Azure OpenAI support customer managed keys?"),
        new ChatRequestAssistantMessage("Yes, customer managed keys are supported by Azure OpenAI."),
        new ChatRequestUserMessage("Do other Azure AI services support this too?"),
    },
    MaxTokens = 150,
};

await foreach (StreamingChatCompletionsUpdate chatUpdate in client.GetChatCompletionsStreaming(chatCompletionsOptions))
{
    if (chatUpdate.Role.HasValue)
    {
        Console.Write($"{chatUpdate.Role.Value.ToString().ToUpperInvariant()}: ");
    }
    if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
    {
        Console.Write(chatUpdate.ContentUpdate);
    }
}
