import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  ChangeDetectionStrategy,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { TaskResponse, TaskItemStatus } from '../../../../core/models/task.model';
import { CategoryResponse } from '../../../../core/models/category.model';

@Component({
  selector: 'app-task-modal',
  standalone: true,
  imports: [FormsModule, AsyncPipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './task-modal.component.html',
})
export class TaskModalComponent implements OnChanges {
  @Input() task?: TaskResponse;
  @Input({ required: true }) categories$!: Observable<CategoryResponse[]>;

  @Output() save = new EventEmitter<any>();
  @Output() cancel = new EventEmitter<void>();

  get isEditMode() {
    return !!this.task;
  }

  form = {
    title: '',
    description: '',
    categoryId: '',
    status: 'Todo' as TaskItemStatus,
  };

  ngOnChanges() {
    this.form = {
      title: this.task?.title ?? '',
      description: this.task?.description ?? '',
      categoryId: this.task?.categoryId ?? '',
      status: this.task?.status ?? 'Todo',
    };
  }

  onSave() {
    this.save.emit({
      title: this.form.title,
      description: this.form.description || undefined,
      categoryId: this.form.categoryId || undefined,
      status: this.form.status,
    });
  }
}
