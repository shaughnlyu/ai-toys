using OllamaSharp;
using OllamaSharp.Models.Chat;
using OllamaSharp.Streamer;

// Create an instance of the OllamaApiClient with the base URL of the Ollama API server
OllamaApiClient ollamaApiClient = new(new Uri("http://localhost:11434"));

// Create a ChatRequest object to send a chat request to the Ollama API
ChatRequest chatRequest = new()
{
    Messages =
    [
        new Message(ChatRole.System, "You are a helpful assistant."),
        new Message(ChatRole.User, "Why is the sky blue?")
    ],
    Model = "phi3"
};

// Create an instance of ActionResponseStreamer to handle the chat response stream
ActionResponseStreamer<ChatResponseStream> responseStreamer = new(response => {
    Console.Write(response.Message?.Content ?? "");
    if (response.Done)
    {
        Console.WriteLine();
    }
});

// Send the chat request to the Ollama API and handle the response using the responseStreamer
await ollamaApiClient.SendChat(chatRequest, responseStreamer);
