using static IWS.Common.DomainHierarchy;

namespace IWS.Common.Builders
{
	/// <summary>
	/// KISS FOR NOW
	/// </summary>
	public class DomainHierarchyBuilder
	{
		public static DomainHierarchy Build(string rootDomainName)
		{
			var catalog = new Dictionary<string, HierarchyTree>();
			var root = new HierarchyTree(rootDomainName, null, new List<HierarchyTree>(), catalog);

			catalog.Add(root.DomainName, root);

			var returnMe = new DomainHierarchy(root);

			return returnMe;
		}
	}
}