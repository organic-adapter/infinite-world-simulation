using AEW.Nucleus.Water.Business;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AEW.Nucleus.Water.API.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class HealthController : ControllerBase
	{
		public readonly IServiceCollection? services;

		public HealthController(IServiceCollection? services = null)
		{
			this.services = services;
		}

		[HttpGet("check")]
		public IActionResult Check()
		{
			if (services == null)
				return Ok();
			return Ok(JsonSerializer.Serialize(services.ToList().Select(service=>service.ServiceType.AssemblyQualifiedName)));
		}

		[HttpGet("check/{serviceType}")]
		public IActionResult Check(string serviceType)
		{
			if (serviceType == null)
				return Ok();
			try
			{
				var provider = services.BuildServiceProvider();
				var type = Type.GetType(serviceType);
				var service = provider.GetService(type);
				if (service == null)
					throw new Exception($"Cannot find ServiceType: {serviceType}");
				return Ok();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("echo/{echoMe}")]
		public IActionResult Echo(string echoMe)
		{
			return Ok(echoMe);
		}

		[HttpPost("echo")]
		public IActionResult Echo([FromBody] HealthPing echoMe)
		{
			return Ok(echoMe);
		}

		public class HealthPing
		{
			public string? Message { get; set; }
		}
	}
}