import { TaskItem } from "../taskitem"

export interface PageResult {


  ActualPage: number;
  PageSize: number;
  TotalItem: number;
  TotalPages: number;

  _Filter: string;

  itemsLists: TaskItem[];


 }
