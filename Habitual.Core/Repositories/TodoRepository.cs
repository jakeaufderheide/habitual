using System.Threading.Tasks;
using Habitual.Core.Entities;

namespace Habitual.Core.Repositories
{
    public interface TodoRepository : Repository<Todo>
    {
        Task MarkDone(Todo todo);
    }
}
