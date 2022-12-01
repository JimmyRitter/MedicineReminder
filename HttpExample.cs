using System;
using System.IO;
using System.Threading.Tasks;
using MedicineReminderProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicineReminderProj;

public static class HttpExample
{
    [FunctionName("HttpExample")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req,
        [CosmosDB(
            "MedicineReminderDB", "Treatments", ConnectionStringSetting = "CosmosDbConnectionString")]
        IAsyncCollector<dynamic> document,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string name = req.Query["name"];

        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name = name ?? data?.name;

        Console.WriteLine(Utils.ToUpperCase("Passou aqui!"));


        var treatment = new Treatment
        {
            MedicineName = "Paracetamol",
            Dosage = "10mg",
            PatientName = "Grazi"
        };

        // if (!string.IsNullOrEmpty(name))
        // Add a JSON document to the output container.

        await document.AddAsync(new
        {
            // create a random ID
            id = Guid.NewGuid().ToString(),
            data = treatment
        });

        var responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";

        return new OkObjectResult(responseMessage);
    }
}