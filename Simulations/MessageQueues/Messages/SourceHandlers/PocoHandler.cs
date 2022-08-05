namespace MessageQueues.Messages.SourceHandlers
{
	/// <summary>
	/// POCO Handlers are only supposed to be used as the only and only handler.
	/// If you would like to use this as a default, put it last in any
	/// list you are using to check IsCompatible.
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