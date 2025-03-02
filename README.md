# Task Tracker Console Application
Project Task URL : https://roadmap.sh/projects/task-tracker

.NET 8 Console app solution for the task-tracker [challenge](https://roadmap.sh/projects/task-tracker) from [roadmap.sh](https://roadmap.sh/).

Task Tracker is a simple console-based application designed to help you manage and track tasks. This application allows you to add, update, delete, and list tasks with various statuses such as "To-Do," "In-Progress," and "Done."

## Features

- **Add a New Task**: Create new tasks with a simple command.
- **Update Task**: Modify the description of an existing task.
- **Delete Task**: Remove tasks by their ID.
- **List Tasks**: Display all tasks or filter tasks by status.
- **Change Task Status**: Mark tasks as "In-Progress," or "Done."

## Installation

To run this application, follow these steps:

1. Clone this repository:
    ```bash
    git clone https://github.com/alinizam723/TaskTracker.git
    ```

2. Navigate to the project directory:
    ```bash
    cd TaskTracker
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Build the project:
    ```bash
    dotnet build
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

### Available Commands

- **add [description]**: Adds a new task with the provided description.
- **update [id] [new description]**: Updates the task with the given ID.
- **delete [id]**: Deletes the task with the given ID.
- **list**: Lists all tasks.
- **list [status]**: Lists tasks filtered by status ("todo", "in-progress", "done").
- **mark-in-progress [id]**: Marks the task with the given ID as "In-Progress".
- **mark-done [id]**: Marks the task with the given ID as "Done".
- **exit**: Exits the application.

### Example Usage

```bash
task cli add "Finish the report"
Task added successfully (ID:1)

task cli list
ID: 1, Description: Buy Groceries, Status: InProgress, Created At: 02-03-2025 14:55:23, Updated At: 02-03-2025 15:33:45

task cli mark-in-progress 1
Task status updated successfully

task cli exit
