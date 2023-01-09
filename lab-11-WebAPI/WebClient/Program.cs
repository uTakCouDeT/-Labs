using System.Net.Http.Json;
using WebApiModel;

string uri = "https://localhost:7019/api/Suppliers";
HttpClient client = new HttpClient();
var suppliers = await client.GetFromJsonAsync<IEnumerable<Supplier>>(uri);
if (suppliers is not null)
{
    foreach (var supp in suppliers)
    {
        Console.WriteLine($"{supp.City}: {supp.ContactName} ");
    }
}
Console.ReadLine();


