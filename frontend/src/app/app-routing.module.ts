import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TasksAddComponent } from './tasks/tasks.add/tasks.add.component';
import { TasksListComponent } from './tasks/tasks.list/tasks.list.component';
import { TasksEditComponent } from './tasks/tasks.edit/tasks.edit.component';

const routes: Routes = [
    { path: 'task/list', component: TasksListComponent },
    { path: 'task/add', component: TasksAddComponent },
    { path: 'task/edit/:id', component: TasksEditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
