using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MedicineReminderProj;

public static class GetTreatment
{
    [FunctionName("GetTreatment")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req,
        [CosmosDB(
            "MedicineReminderDB", "Treatments", ConnectionStringSetting = "CosmosDbConnectionString")]
        IAsyncCollector<dynamic> document,
        ILogger log)
    {
        log.LogInformation("GetTreatment GET request");
        //
        string name = req.Query["name"];
        //
        // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        // dynamic data = JsonConvert.DeserializeObject(requestBody);
        // name = name ?? data?.name;

        // if (!string.IsNullOrEmpty(name))
        //     // Add a JSON document to the output container.
        //     await document.(new
        //     {
        //         // create a random ID
        //         id = Guid.NewGuid().ToString(), name
        //     });

        var responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";

        return new OkObjectResult(responseMessage);
    }
}