namespace MessageQueues.Messages.SourceHandlers
{
	public class StringHandler<T> : MessageSourceHandler<T>
		where T : class
	{
		public override bool IsCompatible(Stream stream)
		{
			return true;
		}
	}
}