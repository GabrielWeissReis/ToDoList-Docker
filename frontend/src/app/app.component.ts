import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface ToDoTasks {
    id: number;
    title: string;
    description: string;
    isCompleted: boolean;
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    public tasks: ToDoTasks[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getTasks();
    }

    getTasks() {
        this.http.get<ToDoTasks[]>('http://localhost:8080/api/ToDoTask/GetTasks').subscribe(
            (result) => {
                this.tasks = result;
            },
            (error) => {
                console.error(error);
            }
        );
    }

    title = 'frontend';
}
