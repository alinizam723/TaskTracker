using System.Text.RegularExpressions;
using TaskTracker.Services;

namespace TaskTracker
{
    internal class Program
    {
        static TaskService taskService = new TaskService();

        static async Task Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("task cli ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    var command = ParseInput(Console.ReadLine());
                    await HandleCommand(command);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        private static async Task HandleCommand(List<string> command)
        {
            switch (command[0].ToLower())
            {
                case "add":
                    await AddTask(command);
                    break;
                case "update":
                    await UpdateTask(command);
                    break;
                case "delete":
                    await DeleteTask(command);
                    break;
                case "mark-in-progress":
                    await MarkInProgress(command);
                    break;
                case "mark-done":
                    await MarkDone(command);
                    break;
                case "list":
                    await ListTasks(command);
                    break;
                case "exit":
                    Exit();
                    break;
                default:
                    InvalidCommand();
                    break;
            }
        }

        private static async Task AddTask(List<string> command)
        {
            if (command.Count < 2)
            {
                Console.WriteLine("Usage: add <description>");
                return;
            }
            var result = await taskService.AddNewTask(command[1]);
            Console.WriteLine($"Task added successfully (ID: {result})");
        }

        private static async Task UpdateTask(List<string> command)
        {
            if (command.Count < 3)
            {
                Console.WriteLine("Usage: update <id> <description>");
                return;
            }
            var result = await Task.Run(() => taskService.UpdateTask(int.Parse(command[1]), command[2]));
            Console.WriteLine(result ? "Task updated successfully" : "Task not found");
        }

        private static async Task DeleteTask(List<string> command)
        {
            if (command.Count < 2)
            {
                Console.WriteLine("Usage: delete <id>");
                return;
            }
            var result = await taskService.DeleteTask(int.Parse(command[1]));
            Console.WriteLine(result ? "Task deleted successfully" : "Task not found");
        }

        private static async Task MarkInProgress(List<string> command)
        {
            if (command.Count < 2)
            {
                Console.WriteLine("Usage: mark-in-progress <id>");
                return;
            }
            var result = await taskService.SetStatus(Status.InProgress, int.Parse(command[1]));
            Console.WriteLine(result ? "Task status updated successfully" : "Task not found");
        }

        private static async Task MarkDone(List<string> command)
        {
            if (command.Count < 2)
            {
                Console.WriteLine("Usage: mark-done <id>");
                return;
            }
            var result = await taskService.SetStatus(Status.Done, int.Parse(command[1]));
            Console.WriteLine(result ? "Task status updated successfully" : "Task not found");
        }

        private static async Task ListTasks(List<string> command)
        {
            if (command.Count == 1)
            {
                var tasks = await taskService.GetAllTasks();
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Description: {task.Description}, Status: {task.TaskStatus}, Created At: {task.CreatedAt}, Updated At: {task.UpdatedAt}");
                }
            }
            else
            {
                var status = (Status)Enum.Parse(typeof(Status), command[1]);
                var tasks = await taskService.GetTaskByStatus(status);
                foreach (var task in tasks)
                {
                    Console.WriteLine($"ID: {task.Id}, Description: {task.Description}, Status: {task.TaskStatus}, Created At: {task.CreatedAt}, Updated At: {task.UpdatedAt}");
                }
            }
        }

        private static void Exit()
        {
            Environment.Exit(0);
            Console.ResetColor();
        }

        private static void InvalidCommand()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid command");
            Console.ResetColor();
        }

        private static List<string> ParseInput(string input)
        {
            var commandArgs = new List<string>();

            // Regex to match arguments, including those inside quotes
            var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                // Remove surrounding quotes if any
                string value = match.Value.Trim('"');
                commandArgs.Add(value);
            }

            return commandArgs;
        }
    }
}
