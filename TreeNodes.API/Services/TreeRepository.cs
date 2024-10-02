using Microsoft.EntityFrameworkCore;
using TreeNodes.API.Extensions;
using TreeNodes.API.Models.Exceptions;
using TreeNodes.Data.Context;
using TreeNodes.Data.Interfaces;
using TreeNodes.Data.Models;

namespace TreeNodes.API.Services
{
    public class TreeRepository : ITreeRepository
    {
        private TreeNodesContext _dbContext;

        public TreeRepository(TreeNodesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateNode(TreeNode node)
        {
            await _dbContext.TreeNodes.AddAsync(node);
            await _dbContext.SaveChangesAsync();

            return node.Id;
        }

        public async Task DeleteNode(int nodeId)
        {
            var nodeToDelete = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Id == nodeId);
            if (nodeToDelete != null)
            {
                _dbContext.TreeNodes.Remove(nodeToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TreeNode> GetTree(string treeName)
        {
            var result = await _dbContext.TreeNodes
                .Where(x => x.TreeName == treeName)
                .ToListAsync();

            if (result == null)
                throw new SecureException("Tree with such name doesn't exist");

            var treeNode = result.ToTree();

            return treeNode;
        }

        public async Task RenameNode(int nodeId, string newNodeName)
        {
            var node = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Id == nodeId);
            if (node != null)
            {
                node.Name = newNodeName;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
