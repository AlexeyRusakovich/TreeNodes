namespace TreeNodes.API.Models.DTOs
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TreeNode> Childs { get; set; }
    }
}
