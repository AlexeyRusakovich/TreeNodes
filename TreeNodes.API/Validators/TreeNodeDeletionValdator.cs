using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TreeNodes.API.Models.TreeNodeActions;
using TreeNodes.Data.Context;

namespace TreeNodes.API.Validators
{
    public class TreeNodeDeletionValdator : AbstractValidator<DeleteTreeNode>
    {
        public TreeNodeDeletionValdator(TreeNodesContext context)
        {
            RuleFor(x => x.NodeId)
                .GreaterThan(0)
                    .WithMessage("Invalid NodeId")
                .MustAsync(async (x, ct) => await context.TreeNodes.AnyAsync(n => n.Id == x))
                    .WithMessage("Node with such nodeId doesn't exist")
                .MustAsync(async (x, ct) => !await context.TreeNodes.AnyAsync(n => n.ParentId == x))
                    .WithMessage("You need to delete all childs nodes, before deleting selected node");
        }
    }
}
