namespace IWS.Common
{
	public class DomainHierarchy
	{
		private readonly HierarchyTree root;

		public DomainHierarchy(HierarchyTree root)
		{
			this.root = root;
		}

		public IEnumerable<string> GetLineage(string domainName)
		{
			var node = root.FindNode(domainName);
			if (node == HierarchyTree.NotFound)
				throw new DomainNameNotFound(domainName);

			var lineage = new List<string>();
			lineage = GetAncestors(lineage, node);
			lineage.Add(domainName);
			
			return lineage;
		}

		private List<string> GetAncestors(List<string> ancestors, HierarchyTree node)
		{
			if (node.Parent == null)
				return ancestors;

			ancestors.Insert(0, node.Parent.DomainName);
			return GetAncestors(ancestors, node.Parent);
		}

		public class DomainNameNotFound : Exception
		{
			public DomainNameNotFound(string domainName)
			{
				DomainName = domainName;
			}

			public string DomainName { get; set; }
		}

		public class HierarchyTree
		{
			public static HierarchyTree NotFound = new HierarchyTree(@"!!NotFound!!");
			public readonly string DomainName;
			public readonly HierarchyTree? Parent;
			private readonly IEnumerable<HierarchyTree> children;
			private readonly Dictionary<string, HierarchyTree> dictionary;

			public HierarchyTree(string domainName)
			{
				this.DomainName = domainName;
				children = new List<HierarchyTree>();
				dictionary = new Dictionary<string, HierarchyTree>();
			}

			public HierarchyTree
					(
						string domainName
						, HierarchyTree? parent
						, IEnumerable<HierarchyTree> children
						, Dictionary<string, HierarchyTree> dictionary
					)
			{
				this.DomainName = domainName;
				this.Parent = parent;
				this.children = children;
				this.dictionary = dictionary;
			}

			public HierarchyTree FindNode(string searchForDomainName)
			{
				return dictionary[searchForDomainName];
			}
		}
	}
}