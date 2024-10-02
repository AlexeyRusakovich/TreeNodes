using TreeNodes.Data.Models;

namespace TreeNodes.Data.Interfaces
{
    public interface IJournalRepository
    {
        Task<IEnumerable<Journal>> GetAll();
        Task Create(Journal journal);
    }
}

