using Practice.Models;
using Practice.Services;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Practice.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkersController : ControllerBase
{
    private readonly ILogger _logger;

    private WorkHoursContext context;
    public WorkersController(ILogger<WorkersController> logger)
    {

        context = new WorkHoursContext();
        _logger = logger;
    }


    //Add a new user
    [Authorize]
    [HttpPut("{name}")]
    public ActionResult<List<Worker>> put(string name)
    {
        var newWorker = new Worker() { Name = name };
        context.Workers.Add(newWorker);
        context.SaveChanges();

        _logger.LogInformation(context.Roles.ToList().ToString());
        _logger.LogInformation("Added new worker with id={0} and Name={1} at {2:DT} ",newWorker.WorkerId,newWorker.Name,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));
        return context.Workers.ToList();
        
    }


    //Return a list of all users
  
    [HttpGet("getWorkers")]
    public ActionResult<List<Worker>> getWorkers()
    {
        return context.Workers.ToList();
    }

    //Delete the user with the corresponding id
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<List<Worker>> deleteWorker(int id)
    {
        var selectedWorker = context.Workers.Where(x => x.WorkerId == id).FirstOrDefault();
        if (selectedWorker is null)
        {
          _logger.LogWarning("User with ID={0} is not in database - {1} ", id,
          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            return context.Workers.ToList();
        }
        context.Workers.Remove(selectedWorker);
        context.SaveChanges();
        return context.Workers.ToList();
    }


    // Update the user whose id is sent to the new Name
    [Authorize]
    [HttpPatch]
    [Route("update")]
    public ActionResult<List<Worker>> update(string newName, int id)
    {
         Worker? selectedWorker = context.Workers.Where(x => x.WorkerId == id).FirstOrDefault();
        if (selectedWorker is null)
        {
            _logger.LogWarning("User with ID={0} is not in database - {1} ", id,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            return context.Workers.ToList();
        }
        selectedWorker.Name = newName;
        context.SaveChanges();
        return context.Workers.ToList();
    }
}




  