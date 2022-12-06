using System.Collections.Generic;
using MedicineReminderProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MedicineReminderProj;

public static class GetTreatment
{
    [FunctionName("GetTreatment")]
    public static ActionResult<IEnumerable<Treatment>> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req,
        [CosmosDB(
            databaseName: "MedicineReminderDB",
            collectionName:"Treatments",
            ConnectionStringSetting = "CosmosDbConnectionString",
            SqlQuery = "SELECT * FROM c order by c._ts desc")]
            IEnumerable<Treatment> treatments,
        ILogger log)
    {
        log.LogInformation("GetTreatment GET request");
        
        return new ActionResult<IEnumerable<Treatment>>(treatments);
    }
}