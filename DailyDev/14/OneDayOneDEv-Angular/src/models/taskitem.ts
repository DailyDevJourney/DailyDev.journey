import { TaskPriority } from "./TaskPriority";

export interface TaskItem
{
  
  id: number;
  title: string;
  iscompleted: boolean;
  createdAt: string;
  updateAt: string;
  dueDate: string;
  overDate: string,
  priority: TaskPriority;
}


