using Google.Api.Gax.Grpc;
using Google.Cloud.AIPlatform.V1;

// Project and model parameters
string projectId = "project-153-424805";
string location = "us-central1";
string publisher = "google";
string model = "gemini-1.5-flash-001";

string prompt = @"Why is the sky blue?";

// Create a PredictionServiceClient using the PredictionServiceClientBuilder
PredictionServiceClient client = new PredictionServiceClientBuilder
{
    GrpcAdapter = GrpcNetClientAdapter.Default.WithAdditionalOptions(options => options.HttpHandler = new HttpClientHandler()),
    Endpoint = $"{location}-aiplatform.googleapis.com"
}.Build();

// Create a GenerateContentRequest to generate content using the specified model
GenerateContentRequest request = new()
{
    Model = $"projects/{projectId}/locations/{location}/publishers/{publisher}/models/{model}",
    Contents =
    {
        new Content
        {
            Role = "USER",
            Parts =
            {
                new Part { Text = prompt },
            }
        }
    }
};

// Generate content asynchronously using the PredictionServiceClient
GenerateContentResponse response = await client.GenerateContentAsync(request);

// Get the generated response text from the first candidate
string responseText = response.Candidates[0].Content.Parts[0].Text;

// Print the response text to the console
Console.WriteLine(responseText);
