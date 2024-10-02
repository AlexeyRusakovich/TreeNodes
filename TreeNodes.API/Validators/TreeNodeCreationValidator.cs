using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TreeNodes.API.Models.TreeNodeActions;
using TreeNodes.Data.Context;

namespace TreeNodes.API.Validators
{
    public class TreeNodeCreationValidator : AbstractValidator<CreateTreeNode>
    {
        public TreeNodeCreationValidator(TreeNodesContext context)
        {
            RuleFor(treeNode => treeNode.TreeName)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithMessage("Tree name is Required.");

            RuleFor(treeNode => treeNode.Name)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithMessage("Node name is required");

            RuleFor(treeNode => treeNode.ParentId)
                .GreaterThan(0).When(x => x.ParentId != null)
                .WithMessage("Invalid ParentId");

            RuleFor(treeNode => treeNode.ParentId)
                .MustAsync(async (x, ct) => await context.TreeNodes.AnyAsync(n => n.Id == x))
                .When(x => x.ParentId != null)
                .WithMessage("Node with such parentId doesn't exist");

            RuleFor(treeNode => treeNode)
                .MustAsync(async (x, ct) => !await context.TreeNodes.AnyAsync(n => n.ParentId == null && n.TreeName == x.TreeName))
                .When(x => x.ParentId == null)
                .WithMessage("The tree already have root");
        }
    } 
}
