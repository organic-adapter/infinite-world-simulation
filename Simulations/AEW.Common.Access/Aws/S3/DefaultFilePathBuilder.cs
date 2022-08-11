using AEW.Contracts;
using AEW.Contracts.Operations;
using AEW.Contracts.Time;

namespace AEW.Common.Access.Aws.S3
{
	public class DefaultFilePathBuilder : FilePathBuilder
	{
		private readonly DomainHierarchy domainHierarchy;
		private readonly Dictionary<Type, string> fileTypeFolderMap = new Dictionary<Type, string>();

		public DefaultFilePathBuilder(DomainHierarchy domainHierarchy)
		{
			this.domainHierarchy = domainHierarchy;

			fileTypeFolderMap.Add(typeof(Operation), "operations");
		}

		public string GetFilePath(DefinedByName obj)
		{
			var lineage = domainHierarchy.GetLineage(obj.DomainName);
			var rootPath = string.Join("/", lineage);
			var fileTypeFolder = GetFileTypeFolder(obj);
			var filePath = $"{rootPath}/{fileTypeFolder}/{obj.Name}.json";

			return filePath;
		}

		public string GetFilePath(DefinedByName obj, Tick tick)
		{
			var lineage = domainHierarchy.GetLineage(obj.DomainName);
			var rootPath = string.Join("/", lineage);
			var fileTypeFolder = GetFileTypeFolder(obj);
			var filePath = $"{rootPath}/{fileTypeFolder}/{obj.Name}-{tick.Id}.json";

			return filePath;
		}

		public string GetFilePath<T>(string domainName, string prefix)
		{
			var lineage = domainHierarchy.GetLineage(domainName);
			var rootPath = string.Join("/", lineage);
			var fileTypeFolder = GetFileTypeFolder<T>();
			var filePath = $"{rootPath}/{fileTypeFolder}/{prefix}.json";

			return filePath;
		}

		public string GetFileTypeFolder<T>()
		{
			var type = typeof(T);
			return GetFileTypeFolder(type);
		}

		public string GetFileTypeFolder(Type type)
		{
			foreach (var impType in type.GetInterfaces())
			{
				if (fileTypeFolderMap.ContainsKey(impType))
					return fileTypeFolderMap[impType];
			}
			throw new FileTypeNotFound(type);
		}

		private string GetFileTypeFolder(DefinedByName obj)
		{
			var objType = obj.GetType();
			foreach (var impType in objType.GetInterfaces())
			{
				if (fileTypeFolderMap.ContainsKey(impType))
					return fileTypeFolderMap[impType];
			}
			throw new FileTypeNotFound(objType);
		}

		public class FileTypeNotFound : Exception
		{
			public FileTypeNotFound(Type type) : base("This type does not implement a supported FileType.")
			{
				Type = type;
			}

			public Type Type { get; set; }
		}
	}
}