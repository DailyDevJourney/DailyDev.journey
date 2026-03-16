import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../models/taskitem';
import { Observable } from 'rxjs';
import { TaskPriority } from '../../models/TaskPriority';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  TaskPriority = TaskPriority;
  isEditMode: boolean = false;
  taskId: number = 0;

  task: TaskItem = {
      id: 0,
    title: "",
    dueDate: "",
      createdAt: "",
      iscompleted: false,
      priority: TaskPriority.MEDIUM,
      updateAt: "",
      overDate: ""
  };


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEditMode = true;
      this.taskId = Number(idParam);
      this.loadTask(this.taskId);
    }
  }

  loadTask(id: number): void {
    this.taskService.getTaskById(id).subscribe({
      next: (data) => {
        this.task = data;
      },
      error: (err) => {
        console.error('Erreur chargement tâche', err);
      }
    });
  }

  save(): void {
    if (this.isEditMode) {
      this.taskService.UpdateTask(this.task).subscribe({
        next: () => {
          this.router.navigate(['/Task']);
        },
        error: (err) => {
          console.error('Erreur update', err);
        }
      });
    } else {
      this.taskService.CreateTask(this.task.title,
        this.task.dueDate.toString(),
        this.task.priority)
        .subscribe({
        next: () => {
          this.router.navigate(['/Task']);
        },
        error: (err) => {
          console.error('Erreur create', err);
          alert(err.error)
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/Task']);
  }
}
