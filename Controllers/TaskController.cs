using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
  [ApiController]
  [EnableCors("AllowAnyOrigin")]
  [Route("v1/tasks")]
  public class TaskController : ControllerBase
  {
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<API.Models.Task>>> Get(
      [FromServices] DataContext context)
    {
      var tasks = await context.Tasks
        .AsNoTracking()
        .ToListAsync();
      return tasks.FindAll(x => x.IsDeleted == false);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<API.Models.Task>> GetById (
      [FromServices] DataContext context,
      int id
    )
    {
      var task = await context.Tasks
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);
      return task;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<API.Models.Task>> Post(
      [FromServices] DataContext context,
      [FromBody] API.Models.Task model
    )
    {
      if(ModelState.IsValid)
      {
        model.CreateDate = DateTime.Now;
        context.Tasks.Add(model);
        await context.SaveChangesAsync();
        return model;
      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<API.Models.Task>> PutUpdate(
      [FromServices] DataContext context,
      [FromBody] API.Models.Task model,
      int id)
      {
        if(id != model.Id)
        {
          return BadRequest();
        }

        context.Entry(model).State = EntityState.Modified;

        try
        {
          context.Tasks.Update(model);
          await context.SaveChangesAsync();
        }
        catch(Exception e)
        {
          throw;
        }
        return model;
      }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<API.Models.Task>> Delete(
      [FromServices] DataContext context,
      int id
    )
    {
      var task = await context.Tasks
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == id);
      
      if(task != null)
      {
        task.IsDeleted = true;
        task.UpdateDate = DateTime.Now;
        context.Tasks.Update(task);
        await context.SaveChangesAsync();
        return task;
      }
      else
      {
        return NotFound();
      }
    }

  }

}
