using AEW.Contracts;
using AEW.Contracts.Time;

namespace AEW.Common.Access.Aws.S3
{
	public interface FilePathBuilder
	{
		public string GetFilePath<T>(string domainName, string filename);

		public string GetFilePath(DefinedByName obj);

		public string GetFilePath(DefinedByName obj, Tick tick);

		public string GetFileTypeFolder(Type type);

		public string GetFileTypeFolder<T>();
	}
}