using HttpClientConsoleApp;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

Console.OutputEncoding = Encoding.Unicode;

HttpClient client = new()
{
    BaseAddress = new Uri("https://localhost:7152")
};

var obj = new
{
    username = "zakir",
    password = "zakir007"
};
string jsonObj = JsonConvert.SerializeObject(obj);
StringContent content = new(jsonObj, Encoding.UTF8, MediaTypeNames.Application.Json);

HttpResponseMessage postStatus = await client.PostAsync("/api/account", content);
if (postStatus.IsSuccessStatusCode)
{
    string postData = await postStatus.Content.ReadAsStringAsync();
    BearerResult result = JsonConvert.DeserializeObject<BearerResult>(postData);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
}
else
{
    Console.WriteLine($"Error: {postStatus.StatusCode}.");
}

HttpResponseMessage peopleStatus = await client.GetAsync("/api/people");

if (peopleStatus.IsSuccessStatusCode)
{
    string peopleData = await peopleStatus.Content.ReadAsStringAsync();
    Console.WriteLine(peopleData);
}
else
{
    Console.WriteLine($"Error: {peopleStatus.StatusCode}.");
}

Console.ReadKey();