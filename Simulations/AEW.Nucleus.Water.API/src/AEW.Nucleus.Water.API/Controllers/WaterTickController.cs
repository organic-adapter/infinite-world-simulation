using AEW.Contracts.Nucleus.Water;
using AEW.Nucleus.Water.Business;
using Microsoft.AspNetCore.Mvc;

namespace AEW.Nucleus.Water.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class WaterTickController : ControllerBase
{
	private readonly WaterManager manager;

	public WaterTickController(WaterManager manager)
	{
		this.manager = manager;
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(string id)
	{
		await manager.DeleteAsync(id);

		return Ok();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<IEnumerable<WaterTick>>> Get(string id)
	{
		try
		{
			return Ok(await manager.GetAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<ActionResult<WaterTick>> Post([FromBody] WaterTick waterTick)
	{
		if (waterTick == null) return ValidationProblem("Invalid input! WaterTick null");

		var result = await manager.SaveAsync(waterTick);
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody] WaterTick waterTick)
	{
		if (waterTick == null) return ValidationProblem("Invalid input! WaterTick null");

		var result = await manager.SaveAsync(waterTick);
		return Ok(result);
	}
}