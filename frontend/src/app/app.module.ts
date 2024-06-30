import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TasksAddComponent } from './tasks/tasks.add/tasks.add.component';
import { TasksListComponent } from './tasks/tasks.list/tasks.list.component';

import { FormsModule } from '@angular/forms';
import { TasksEditComponent } from './tasks/tasks.edit/tasks.edit.component';

@NgModule({
  declarations: [
    AppComponent,
    TasksAddComponent,
    TasksListComponent,
    TasksEditComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      AppRoutingModule,
      FormsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
