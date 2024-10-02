using System.ComponentModel.DataAnnotations;

namespace TreeNodes.Data.Models
{
    public class TreeNode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string  TreeName { get; set; }

        public TreeNode Parent { get; set; }
        public IEnumerable<TreeNode> Childs { get; set; }
    }
}
