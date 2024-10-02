using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TreeNodes.API.Extensions;
using TreeNodes.API.Models.DTOs;
using TreeNodes.API.Models.TreeNodeActions;
using TreeNodes.Data.Interfaces;

namespace TreeNodes.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreeNodeController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITreeRepository _treeRepository;
        private readonly IMapper _mapper;

        public TreeNodeController(
            IServiceProvider serviceProvider,
            ITreeRepository treeRepository,
            IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _treeRepository = treeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTree(string treeName)
        {
            var result = await _treeRepository.GetTree(treeName);
            var mapped = _mapper.Map<TreeNode>(result);
            return Ok(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTreeNode node)
        {
            var validator = _serviceProvider.GetValidator<CreateTreeNode>();
            await node.ValidateAsync(validator);

            var mapped = _mapper.Map<Data.Models.TreeNode>(node);
            var nodeId = await _treeRepository.CreateNode(mapped);

            return Ok(nodeId);
        }

        [HttpPut]
        public async Task<IActionResult> Rename([FromBody] RenameTreeNode node)
        {
            var validator = _serviceProvider.GetValidator<RenameTreeNode>();
            await node.ValidateAsync(validator);

            await _treeRepository.RenameNode(node.NodeId, node.NewNodeName);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTreeNode node)
        {
            var validator = _serviceProvider.GetValidator<DeleteTreeNode>();
            await node.ValidateAsync(validator);

            await _treeRepository.DeleteNode(node.NodeId);

            return Ok();
        }
    }
}
