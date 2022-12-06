using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MedicineReminderProj.Models;

public static class CreateTreatment
{
    [FunctionName("CreateTreatment")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        HttpRequest req,
        [CosmosDB(
            databaseName: "MedicineReminderDB",
            collectionName:"Treatments",
            ConnectionStringSetting = "CosmosDbConnectionString")]
        IAsyncCollector<Treatment> document,
        ILogger log)
    {
        log.LogInformation("Create Treatment initialized");
       
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject<Treatment>(requestBody);
        
        var newTreatment = new Treatment {
            MedicineName = data.MedicineName,
            Dosage = data.Dosage,
            PatientName = data.PatientName
        };
        
        await document.AddAsync(newTreatment);
        log.LogInformation("Create Treatment finished");
        
        return new OkObjectResult(newTreatment);
    }
}