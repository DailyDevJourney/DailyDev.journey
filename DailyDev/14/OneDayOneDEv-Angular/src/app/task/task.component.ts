import { Component ,OnInit,OnDestroy} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { TaskItem } from '../../models/taskitem';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task.service';
import { TaskPriority } from "../../models/TaskPriority"

@Component({
  selector: 'Task',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent implements OnInit,OnDestroy{
    searchTerm: string = "";
    selectedStatus: string = "";
    selectedPriority: string = "";
  TaskPriority = TaskPriority;
    loading!: boolean;
  errorMessage!: string;

  tasks: TaskItem[] = [];
 

  constructor(private authService: AuthService,
              private router: Router,
              private taskService: TaskService)
            { }


  remainingTime: string = "";
  expirationTimer: any = null;
  ngOnDestroy(): void {
    if (this.expirationTimer) {
      clearInterval(this.expirationTimer);
      this.expirationTimer = null;
    }
  }

  logout(): void {
    this.authService.logout();
    return;
  }

  startExpirationTimer() {

    const expiration = localStorage.getItem('tokenExpiration');

    if (!expiration) {
      this.remainingTime = 'Non connecté';
      return;
    }

    const expirationDate = new Date(expiration);

    this.expirationTimer =  setInterval(() => {

      const now = new Date();

      const diff = expirationDate.getTime() - now.getTime();

      if (diff <= 0) {
        this.remainingTime = 'Expiré';

        clearInterval(this.expirationTimer);
        this.expirationTimer = null;

        this.authService.logout();
        return;
      }

      const minutes = Math.floor(diff / 60000);
      const seconds = Math.floor((diff % 60000) / 1000);

      this.remainingTime = minutes + 'm ' + seconds + 's';

    }, 1000);

  }

  ngOnInit(): void {

    this.startExpirationTimer()

    this.loadTasks();
  }

  loadTasks(): void {
    this.loading = true;
    this.errorMessage = '';

    this.taskService.getTasks().subscribe({
      next: (data) => {
        console.log('PageResult reçu :', data);
        this.tasks = data.itemsLists;
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Erreur récupération tâches :', err);
        this.errorMessage = 'Impossible de charger les tâches.';
        this.loading = false;
      }
    });
  }

  filteredTasks(): TaskItem[] {

    const search = (this.searchTerm || '').toLowerCase();

    return this.tasks.filter(task => {

      const matchesSearch =
        (task.title || '').toLowerCase().includes(search);

      const matchesStatus =
        this.selectedStatus === ''
        || (this.selectedStatus === 'Done' && task.iscompleted)
        || (this.selectedStatus === 'Todo' && !task.iscompleted);

      const matchesPriority =
        this.selectedPriority === '' ||
        task.priority === this.selectedPriority;

      return matchesSearch && matchesStatus && matchesPriority;

    });

  }

  toggleTaskStatus(task: TaskItem): void {
    task.iscompleted = !task.iscompleted;
    this.taskService.UpdateTask(task).subscribe({
      next: () => {
        console.log("Statut mis à jour");
      },
      error: (err) => {
        console.error("Erreur update", err);
      }
    });
  }

  isOverdue(task: TaskItem): boolean {

    if (!task.dueDate || task.iscompleted) {
      return false;
    }

    const due = new Date(task.dueDate);
    const today = new Date();

    return due < today;
  }
  addTask(): void {
    this.router.navigate(['/Task/Create']);
  }

  editTask(task: TaskItem): void {
    this.router.navigate(['/Task/Edit', task.id]);
  }

  deleteTask(id: number): void {
    this.taskService.DeleteTask(id);
  }
}
