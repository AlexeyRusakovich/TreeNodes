using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TreeNodes.API.Models.TreeNodeActions;
using TreeNodes.Data.Context;

namespace TreeNodes.API.Validators
{
    public class TreeNodeRenamingValidator : AbstractValidator<RenameTreeNode>
    {
        public TreeNodeRenamingValidator(TreeNodesContext context)
        {
            RuleFor(treeNode => treeNode.NodeId)
                .GreaterThan(0)
                .WithMessage("Invalid nodeId");

            RuleFor(treeNode => treeNode.NewNodeName)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithMessage("New node name is required");

            RuleFor(treeNode => treeNode)
                .MustAsync(async (x, ct) => await context.TreeNodes.AnyAsync(n => n.Id == x.NodeId))
                .WithMessage("Node with such nodeId doesn't exist");
        }
    }
}
