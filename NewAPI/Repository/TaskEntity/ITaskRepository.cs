using System.Linq;
using System.Threading.Tasks;

namespace NewAPI.Repository.TaskEntity
{
    public interface ITaskRepository
    {
        public Task<string> AddTaskAsync(Task entity);
        public Task<Task> GetTaskByIdAsync(string id);
        public Task<bool> DeleteTaskAsync(Task entity);
        public Task<bool> UpdateTaskAsync(Task entity);
        public Task<IQueryable<Task>> GetAll();
    }
}
