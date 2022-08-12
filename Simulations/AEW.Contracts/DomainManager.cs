namespace AEW.Contracts
{
	public abstract class DomainManager
	{
		public abstract Supply DispatchSupply(Demand demand);

		public abstract Supply GenerateSupply();

		public abstract void Tick();
	}
}