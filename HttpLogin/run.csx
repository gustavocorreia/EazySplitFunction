#r "Newtonsoft.Json"

using System.Net;
using System.Data.SQLClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

[FunctionName("DadosAPP")]
public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("Conectando");

    string login = req.Query["login"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;

    return name != null
        ? (ActionResult)new OkObjectResult($"Hello, {name}")
        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
}

private static Cliente ObterCliente(string login)
{

}
