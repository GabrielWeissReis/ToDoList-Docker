import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';

interface ToDoTask {
    id: number;
    title: string;
    description: string;
    isCompleted: boolean;
}

@Component({
    selector: 'app-tasks.edit',
    templateUrl: './tasks.edit.component.html',
    styleUrls: ['./tasks.edit.component.css']
})
export class TasksEditComponent implements OnInit {
    task: ToDoTask = { id: 0, title: '', description: '', isCompleted: false };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private http: HttpClient
    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe(params => {
            const taskId = Number(params.get('id'));
            if (taskId) {
                this.getTaskById(taskId);
            }
            else {
                console.error('Invalid task ID');
                this.router.navigate(['/task/list']);
            }
        });
    }

    getTaskById(taskId: number) {
        this.http.get<ToDoTask>(`http://localhost:8080/api/ToDoTask/GetTask/${taskId}`)
            .subscribe({
                next: (task) => {
                    this.task = task;
                },
                error: (error) => {
                    console.error('Error fetching task:', error);
                }
            });
    }

    updateTask(taskForm: NgForm) {
        if (taskForm.valid && this.task) {
            this.http.put(`http://localhost:8080/api/ToDoTask/UpdateTask/${this.task.id}`, this.task)
                .subscribe({
                    next: () => {
                        this.router.navigate(['/task/list']);
                    },
                    error: (error) => {
                        console.error('Error updating task:', error);
                    }
                });
        }
    }

    backToTaskList() {
        this.router.navigate(['/task/list']);
    }
}

