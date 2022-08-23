using AEW.Contracts.Time;
using AEW.Events;

namespace AEW.Hive.Business
{
	public interface TickManager
	{
		Task Handle(TickEnded tickEnded);

		Task StartTickAsync();

		Task StartTickAsync(Tick tick);
	}
}