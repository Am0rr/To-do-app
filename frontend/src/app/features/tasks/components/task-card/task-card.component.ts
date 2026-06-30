import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { TaskResponse } from '../../../../core/models/task.model';

@Component({
  selector: 'app-task-card',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './task-card.component.html',
})
export class TaskCardComponent {
  @Input({ required: true }) task!: TaskResponse;

  @Output() edit = new EventEmitter<TaskResponse>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleStatus = new EventEmitter<TaskResponse>();
}
