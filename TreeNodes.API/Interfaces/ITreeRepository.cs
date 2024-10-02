using TreeNodes.Data.Models;

namespace TreeNodes.Data.Interfaces
{
    public interface ITreeRepository
    {
        Task<TreeNode> GetTree(string name);
        Task<int> CreateNode(TreeNode node);
        Task DeleteNode(int nodeId);
        Task RenameNode(int nodeId, string newNodeName);
    }
}
