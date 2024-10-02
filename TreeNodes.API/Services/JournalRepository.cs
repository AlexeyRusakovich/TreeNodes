using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreeNodes.Data.Context;
using TreeNodes.Data.Interfaces;
using TreeNodes.Data.Models;

namespace TreeNodes.API.Services
{
    public class JournalRepository : IJournalRepository
    {
        private readonly TreeNodesContext _context;

        public JournalRepository(TreeNodesContext context)
        {
            _context = context;
        }

        public async Task Create(Journal journal)
        {
            await _context.Journal.AddAsync(journal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Journal>> GetAll()
        {
            return await _context.Journal.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
    }
}
