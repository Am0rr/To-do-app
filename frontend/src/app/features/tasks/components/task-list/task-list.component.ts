import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { TaskResponse, TaskPagedResponse } from '../../../../core/models/task.model';
import { TaskCardComponent } from '../task-card/task-card.component';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [TaskCardComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './task-list.component.html',
  host: {
    class: 'flex-1 flex flex-col overflow-hidden',
  },
})
export class TaskListComponent {
  @Input({ required: true }) pagedResult!: TaskPagedResponse;

  @Output() edit = new EventEmitter<TaskResponse>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleStatus = new EventEmitter<TaskResponse>();
  @Output() pageChange = new EventEmitter<number>();

  get pageNumbers(): number[] {
    const { totalPages, pageNumber } = this.pagedResult;
    const delta = 2;
    const range: number[] = [];
    for (
      let i = Math.max(1, pageNumber - delta);
      i <= Math.min(totalPages, pageNumber + delta);
      i++
    ) {
      range.push(i);
    }
    return range;
  }
}
