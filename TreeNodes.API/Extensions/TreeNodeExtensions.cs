using TreeNodes.Data.Models;

namespace TreeNodes.API.Extensions
{
    public static class TreeNodeExtensions
    {
        public static TreeNode ToTree(this IEnumerable<TreeNode> treeNodes)
        {
            if (treeNodes == null || !treeNodes.Any())
                return null;
            
            TreeNode root = treeNodes.FirstOrDefault(n => n.ParentId == null);

            var treeNodesDictionary = treeNodes
                .Where(x => x.ParentId != null)
                .GroupBy(x => x.ParentId)
                .ToDictionary(key => key.Key, value => (IList<TreeNode>)value.ToList());

            FindChildsRecursively(root, treeNodesDictionary);

            return root;
        }

        public static void FindChildsRecursively(TreeNode parent, IDictionary<int?, IList<TreeNode>>? childNodes)
        {
            childNodes.TryGetValue(parent.Id, out var childs);

            if (childs != null)
            {
                parent.Childs = childs;

                foreach (var child in childs)
                {
                    FindChildsRecursively(child, childNodes);
                }
            }
        }
    }
}
