using Practice.Models;
using Practice.Services;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Data;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Practice.Controllers;


[ApiController]
[Route("[controller]")]
public class WorkController : ControllerBase
{
    private readonly ILogger _logger;
    private WorkHoursContext context;
    public WorkController(ILogger<WorkersController> logger)
    {
        _logger = logger;
        
        context = new WorkHoursContext();
    }


    //Add a new task
    [HttpPost("/addWork")]
    public ActionResult<List<Work>> put(string name, int workerId, int totalHours, DateTime? endTime)
    {

        var worker = context.Workers.Where(x => x.WorkerId == workerId).First();
        var newWork = new Work() { WorkerName = name, EndTime = endTime, StartTime = DateTime.Now, TotalHours = totalHours, WorkerId = workerId, Worker = worker };
        context.Works.Add(newWork);
        context.SaveChanges();

        _logger.LogInformation("Added new work with id={0} and StartTime={1} for worker with id={2} at {3:DT} ", newWork.WorkId, newWork.StartTime, workerId,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));
        return context.Works.ToList();
    }


    //List all works in the database
    [Authorize]
    [HttpPost("/listWork")]
    public ActionResult<List<Work>> list()
    {
        return context.Works.ToList();
    }


    // Read all works in the current month
    [HttpPost("/getCurrentMonthWorks")]
    public ActionResult<List<Work>> currentMonth(string name, int workerId, int totalHours)
    {
        var now=DateTime.Now;
        var workList = context.Works.Where(x => x.StartTime.Month==now.Month).ToList();
        return workList;
    }


    //List all works in the database
    [HttpPatch("/updateWorkEnd")]
    public ActionResult<List<Work>> update(int workId,DateTime? endTime)
    {
        endTime ??=DateTime.Now;
        Work? selectedWork = context.Works.Where(x => x.WorkId == workId).FirstOrDefault();
        if (selectedWork is null)
        {
            _logger.LogWarning("Work with ID={0} not in database - {1:DT} ", workId,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));
            return context.Works.ToList();
        }
        selectedWork.EndTime = endTime;
        context.SaveChanges();
        _logger.LogInformation("Updated work with ID={0} to EndTime={1} - {2:DT} ", workId,endTime,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));

        return context.Works.ToList();
    }


    //Delete work by id
    [HttpDelete("/deleteWork")]
    public ActionResult<List<Work>> delete( int workId)
    {

        Work? selectedWork = context.Works.Where(x => x.WorkId == workId).First();
        if(selectedWork is null)
        {
            _logger.LogWarning("Work with ID={0} not in database - {1:DT} ",workId,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));
            
        }
        context.Remove(selectedWork);
        context.SaveChanges();
        _logger.LogInformation("Deleted work with ID={0} - {1:DT} ",workId,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));

        return context.Works.ToList();
    }


    [HttpPost("/finishAll")]
    public ActionResult<dynamic> finishAll()
    {
        var query = context.Works.Where(x=>x.EndTime==null).ToList();
        var now = DateTime.Now;
        foreach(Work work in query)
        {
            work.EndTime = now;
            _logger.LogInformation("Finished work with ID={0} on EndTime={1} - {2:DT} ", work.WorkId,now,
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
            CultureInfo.InvariantCulture));
        }
        context.SaveChanges();
        return query;
    }

    // Join a specific worker with all his works
    [HttpGet("/joinByWorker/{workerId}")]
    public ActionResult<dynamic> joinByWorker( int workerId)
    {
        var query = (from work in context.Works
                     join worker in context.Workers on work.WorkerId equals worker.WorkerId
                     where worker.WorkerId== workerId
                     select new { WorkerName = worker.Name,
                                  Hours = work.TotalHours,
                                  StartTime = work.StartTime, 
                                  EndTime = work.EndTime }
                     ).ToList();
        
        return query;
    }


    // Join all workers with their corresponding works
    [HttpPost("/joinAll")]
    public ActionResult<dynamic> joinAll()
    {
        var query = (from work in context.Works
                     join worker in context.Workers on work.WorkerId equals worker.WorkerId
                     select new { WorkerName = worker.Name, Hours = work.TotalHours ,StartTime=work.StartTime,EndTime=work.EndTime }
                     ).ToList();

        return query;
    }

    // Join a worker with all of their unfinished works
    [HttpPost("/joinUnfinishedTasks")]
    public ActionResult<dynamic> joinUnfinished(int workerId)
    {
        var query = (from work in context.Works
                     join worker in context.Workers on work.WorkerId equals worker.WorkerId
                     where worker.WorkerId == workerId
                     where work.EndTime ==null
                     select new { WorkerName = worker.Name, Hours = work.TotalHours, StartTime = work.StartTime, EndTime = work.EndTime }
                     ).ToList();

        return query;
    }

    // List all unfinished works
    [HttpPost("/listUnfinishedTasks")]
    public ActionResult<dynamic> listUnfinished()
    {
        var query = (from work in context.Works
                     join worker in context.Workers on work.WorkerId equals worker.WorkerId
                     where work.EndTime == null
                     select new { WorkerName = worker.Name, Hours = work.TotalHours, StartTime = work.StartTime, EndTime = work.EndTime }
                     ).ToList();

        return query;
    }
}




  