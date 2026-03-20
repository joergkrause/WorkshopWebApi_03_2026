Console.WriteLine("Hello, World!");

var client = new Workshop.Client.WorkshopApiClient("https://localhost:7252", new HttpClient());

var projects = await client.ProjectAllAsync();

foreach(var project in projects)
{
  Console.WriteLine(project.Id);
  Console.WriteLine(project.Name);
}
