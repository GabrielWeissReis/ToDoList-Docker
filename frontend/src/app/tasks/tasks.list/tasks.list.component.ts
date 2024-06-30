import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface ToDoTask {
    id: number;
    title: string;
    description: string;
    isCompleted: boolean;
    createdAt: Date;
    editedAt: Date;
}

@Component({
  selector: 'app-tasks.list',
  templateUrl: './tasks.list.component.html',
  styleUrl: './tasks.list.component.css'
})

export class TasksListComponent {

    public tasks: ToDoTask[] = [];

    constructor(private http: HttpClient, private router: Router) { }

    ngOnInit() {
        this.getTasks();
    }

    getTasks() {
        this.http.get<ToDoTask[]>('http://localhost:8080/api/ToDoTask/GetTasks')
            .subscribe({
                next: (result) => {
                    this.tasks = result.map(task => {
                        task.createdAt = new Date(task.createdAt);
                        return task;
                    }).sort((a, b) => b.createdAt.getTime() - a.createdAt.getTime());
                },
                error: (error) => {
                    console.error(error);
                }
            });
    }

    navigateToTaskAdd() {
        this.router.navigate(['/task/add']);
    }

    editTask(id: number) {
        this.router.navigate(['/task/edit/' + id]);
    }

    deleteTask(id: number) {
        if (confirm('Are you sure you want to delete this task?')) {
            this.http.delete(`http://localhost:8080/api/ToDoTask/DeleteTask/${id}`).subscribe({
                next: () => {
                    this.getTasks();
                },
                error: (error) => {
                    console.error(error);
                }
            });
        }
    }
}
