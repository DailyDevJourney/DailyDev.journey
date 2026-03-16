import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { Injectable } from '@angular/core';
import { authresponse } from '../models/auth-reponse';
import { Router } from '@angular/router';
import { PageResult } from '../models/Result/PageResult';
import { TaskPriority } from '../models/TaskPriority';
import { TaskItem } from '../models/taskitem';
import { Result } from '../models/Result/Result';
import { AppSettings } from "../../public/AppSettings";


@Injectable({
  providedIn: 'root'
})
export class TaskService {
 
 
  constructor(private http: HttpClient, private AppSettings: AppSettings) {
    AppSettings.loadConfig();
  }

  get api_url(): string {
    return `${this.AppSettings.apiUrl}/Tasks`;
  }
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem("token");


    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getTasks(): Observable<PageResult>
  {
    return this.http.get<PageResult>(this.api_url + "/GetAllTask", { headers: this.getHeaders() } );
  }

  

  getTaskById(id: number)
  {
    
    const params = { identifiant: id };

    return this.http.get<TaskItem>(
      `${this.api_url}/GetTaskById`,
      {
        headers: this.getHeaders(),
        params
      }
    );
  }

  getTaskByPriority(priority: TaskPriority) {

    const params = { priority: priority };

    return this.http.get<TaskItem[]>(
      `${this.api_url}/GetTaskByPriorities`,
      {
        headers: this.getHeaders(),
        params
      }
    );
  }

  DeleteTask(identifiant : number): Observable<PageResult> {
    

    return this.http.delete<PageResult>(this.api_url + "/DeleteATask/${identifiant}", { headers: this.getHeaders() });

  }

  UpdateTask(task: TaskItem): Observable<Result<TaskItem>> {

    const request = {
      identifiant: task.id,
      NewTitle: task.title,
      NewDueDate: task.dueDate ? new Date(task.dueDate).toISOString() : null,
      NewIscompleted: !!task.iscompleted,
      priority: task.priority
    };

    return this.http.put<Result<TaskItem>>(
      `${this.api_url}/UpdateTask`,
      request,
      { headers: this.getHeaders() }
    );

  }
  CreateTask(AddTitle: string, Addduedate: string, Addpriority: TaskPriority): Observable<Result<TaskItem>> {

    const request = {
      Title: AddTitle,
      DueDate: Addduedate,
      Priority: Addpriority.toString()
    };

    console.log("AddTitle =", request.Title);
    console.log("Addduedate =", request.DueDate);
    console.log("Addpriority =", request.Priority);

    return this.http.post<Result<TaskItem>>(
      `${this.api_url}/CreateATask`,
      request,
      { headers: this.getHeaders() }
    );

  }

}
