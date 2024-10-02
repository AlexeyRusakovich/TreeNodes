using AutoMapper;

namespace TreeNodes.API.Mapping.Profiles
{
    public class TreeNodeProfile : Profile
    {
        public TreeNodeProfile()
        {
            CreateMap<Data.Models.TreeNode, Models.DTOs.TreeNode>()
                .ReverseMap();

            CreateMap<Models.TreeNodeActions.CreateTreeNode, Data.Models.TreeNode> ();
        }
    }
}
