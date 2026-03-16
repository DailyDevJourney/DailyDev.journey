import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { TaskComponent } from './task/task.component';
import { TaskFormComponent } from './task-form/task-form.component';

export const routes: Routes = [

  { path: '', component: LoginComponent },
  { path: 'Task', component: TaskComponent },
  { path: 'Task/Create', component: TaskFormComponent },
  { path: 'Task/Edit/:id', component: TaskFormComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
