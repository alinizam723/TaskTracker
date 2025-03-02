using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace TaskTracker.Services
{
    public class TaskService
    {
        private static readonly string FileName = "task_data.json";
        private List<ApplicationTask> _tasks;

        public TaskService()
        {
            _tasks = LoadTasks();
        }

        public async Task<int> AddNewTask(string description)
        {
            var newTask = new ApplicationTask
            {
                Id = GetTaskId(),
                Description = description,
                TaskStatus = Status.ToDo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _tasks.Add(newTask);
            await SaveChangesAsync();
            return await Task.FromResult(newTask.Id);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;
            _tasks.Remove(task);
            await SaveChangesAsync();
            return true;
        }

        public async Task<List<ApplicationTask>> GetAllTasks()
        {
            return await Task.FromResult(_tasks.ToList());
        }

        public async Task<List<ApplicationTask>> GetTaskByStatus(Status status)
        {
            return await Task.FromResult(_tasks.Where(t => t.TaskStatus == status).ToList());
        }

        public async Task<bool> SetStatus(Status status, int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return await Task.FromResult(false);
            task.TaskStatus = status;
            task.UpdatedAt = DateTime.Now;
            await SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateTask(int id, string description)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return await Task.FromResult(false);
            task.Description = description;
            task.UpdatedAt = DateTime.Now;
            await SaveChangesAsync();
            return await Task.FromResult(true);
        }

        private int GetTaskId()
        {
            return _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        private List<ApplicationTask> LoadTasks()
        {
            try
            {
                if (!File.Exists(FileName)) return new List<ApplicationTask>();
                var json = File.ReadAllText(FileName);
                return JsonSerializer.Deserialize<List<ApplicationTask>>(json) ?? new List<ApplicationTask>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}");
                return new List<ApplicationTask>();
            }
        }
    }
}
