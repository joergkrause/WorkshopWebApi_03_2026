using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.BusinessLogicLayer;
using Workshop.DataTransferObjects;

namespace Workshop.ServiceLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Produces("application/json")]
// [Authorize]
public class ProjectController(IProjectService projectService) : ControllerBase
{

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetProjects()
  {
    return Ok(await projectService.GetProjects());
  }

  [HttpGet("{id:int}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> GetProjectById([FromRoute] int id)
  {
    var dto = await projectService.GetProjectById(id);
    if (dto == null)
    {
      return NotFound();
    }
    return Ok(dto);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> CreateProject([FromBody] ProjectNewDto project)
  {
    var result = await projectService.CreateProject(project);
    return CreatedAtAction(nameof(GetProjectById), new { id = result.Id }, result);
  }

  [HttpPut("{id:int}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] ProjectUpdateDto project)
  {
    if (id != project.Id)
    {
      return BadRequest();
    }
    var result = await projectService.UpdateProject(project);
    if (!result)
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpDelete("{id:int}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> DeleteProject([FromRoute] int id)
  {
    var result = await projectService.DeleteProject(id);
    if (!result)
    {
      return BadRequest();
    }
    return NoContent();
  }
}