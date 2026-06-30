import { Component, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { TaskItemStatus } from '../../../../core/models/task.model';

@Component({
  selector: 'app-task-filter-bar',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './task-filter-bar.component.html',
})
export class TaskFilterBarComponent {
  @Output() searchChange = new EventEmitter<string | undefined>();
  @Output() statusChange = new EventEmitter<TaskItemStatus | undefined>();

  onSearch(event: Event) {
    const value = (event.target as HTMLInputElement).value || undefined;
    this.searchChange.emit(value);
  }

  onStatusChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.statusChange.emit(value ? (value as TaskItemStatus) : undefined);
  }
}
