namespace MessageQueues.Messages.SourceHandlers
{
	/// <summary>
	/// POCO Handlers are only supposed to be used as the only and only handler.
	/// </summary>
	public class PocoHandler<T> : MessageSourceHandler<T>
		where T : class
	{
		public override bool IsCompatible(Stream stream)
		{
			return true;
		}
	}
}