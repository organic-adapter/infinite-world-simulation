using AEW.Contracts.Nucleus.Food;
using AEW.Nucleus.Food.Business;
using Microsoft.AspNetCore.Mvc;

namespace AEW.Nucleus.Food.API.Controllers;

[Route("food/api/[controller]")]
[Produces("application/json")]
public class FoodTickController : ControllerBase
{
	private readonly FoodManager manager;

	public FoodTickController(FoodManager manager)
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
	public async Task<ActionResult<IEnumerable<FoodTick>>> Get(string id)
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
	public async Task<ActionResult<FoodTick>> Post([FromBody] FoodTick foodTick)
	{
		if (foodTick == null) return ValidationProblem("Invalid input! FoodTick null");

		var result = await manager.SaveAsync(foodTick);
		return Ok(result);
	}

	[HttpPut]
	public async Task<IActionResult> Put([FromBody] FoodTick foodTick)
	{
		if (foodTick == null) return ValidationProblem("Invalid input! FoodTick null");

		var result = await manager.SaveAsync(foodTick);
		return Ok(result);
	}
}