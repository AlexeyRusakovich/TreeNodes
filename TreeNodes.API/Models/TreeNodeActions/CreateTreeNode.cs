namespace TreeNodes.API.Models.TreeNodeActions
{
    public class CreateTreeNode
    {
        public string TreeName { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
