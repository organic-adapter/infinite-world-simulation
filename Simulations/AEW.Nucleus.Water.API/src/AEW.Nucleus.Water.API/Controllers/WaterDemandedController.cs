using AEW.Events.Nucleus.Water;
using AEW.Nucleus.Water.Business;
using Microsoft.AspNetCore.Mvc;

namespace AEW.Nucleus.Water.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class WaterDemandedController : ControllerBase
{
	private readonly WaterManager manager;

	public WaterDemandedController(WaterManager manager)
	{
		this.manager = manager;
	}

	[HttpPost]
	public async Task<ActionResult> Handle([FromBody] WaterDemanded demand)
	{
		await manager.ProcessDemand(demand);
		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> Put([FromBody] List<WaterDemanded> demands)
	{
		await manager.ProcessDemands(demands);
		return Ok();
	}
}