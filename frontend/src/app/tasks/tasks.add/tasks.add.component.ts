import { Component, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

interface ToDoTask {
    id: number;
    title: string;
    description: string;
    isCompleted: boolean;
}

@Component({
    selector: 'app-tasks-add',
    templateUrl: './tasks.add.component.html',
    styleUrls: ['./tasks.add.component.css']
})

export class TasksAddComponent {
    newTask: ToDoTask = { id: 0, title: '', description: '', isCompleted: false };

    constructor(private http: HttpClient, private router: Router) { }

    addTask(taskForm: NgForm) {
        if (taskForm.valid) {
            this.http.post<ToDoTask>('http://localhost:8080/api/ToDoTask/CreateTask', this.newTask)
                .subscribe({
                    next: (createdTask) => {
                        this.newTask = { id: 0, title: '', description: '', isCompleted: false };
                        taskForm.resetForm();
                        this.router.navigate(['/task/list']);
                    },
                    error: (error) => {
                        console.error('Error adding task:', error);
                    }
                });
        }
    }

    backToTaskList() {
        this.router.navigate(['/task/list']);
    }
}
