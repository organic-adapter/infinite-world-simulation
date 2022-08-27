using AEW.ShowCases.Business;
using Microsoft.AspNetCore.Mvc;

namespace AEW.ShowCases.API.Controllers
{
	[Route("hive/api/[controller]")]
	[ApiController]
	public class EndpointController : ControllerBase
	{
		private readonly IHiveEndpointService hiveEndpointService;

		public EndpointController(IHiveEndpointService hiveEndpointService)
		{
			this.hiveEndpointService = hiveEndpointService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await hiveEndpointService.GetEndpointsAsync());
		}
	}
}