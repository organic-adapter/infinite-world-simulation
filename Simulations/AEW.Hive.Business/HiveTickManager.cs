using AEW.Common.Access;
using AEW.Contracts.Time;
using AEW.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEW.Hive.Business
{
	public class HiveTickManager : TickManager
	{
		private readonly string FQN;
		private readonly NotificationBus notificationBus;
		public HiveTickManager(NotificationBus notificationBus)
		{
			FQN = GetType().AssemblyQualifiedName ?? "HiveTickManager";
			this.notificationBus = notificationBus;
		}

		public async Task Handle(TickEnded tickEnded)
		{
			
		}

		public async Task StartTickAsync()
		{
			var uniqueId = Guid.NewGuid().ToString();
			var startMe = new Tick(uniqueId);

			await StartTickAsync(startMe);
		}

		public async Task StartTickAsync(Tick tick)
		{
			var sendMe = new TickStarted(FQN, tick);

		}
	}
}
