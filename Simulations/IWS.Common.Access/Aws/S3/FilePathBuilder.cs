using IWS.Contracts;
using IWS.Contracts.Time;

namespace IWS.Common.Access.Aws.S3
{
	public interface FilePathBuilder
	{
		public string GetFilePath(DefinedByName obj);

		public string GetFilePath(DefinedByName obj, Tick tick);
	}
}