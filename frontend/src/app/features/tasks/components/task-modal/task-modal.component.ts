import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AsyncPipe } from '@angular/common';
import { Observable } from 'rxjs';
import { TaskResponse, TaskItemStatus } from '../../../../core/models/task.model';
import { CategoryResponse } from '../../../../core/models/category.model';
import { TaskService } from '../../../../core/services/task.service';

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

  @Output() saved = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  constructor(
    private taskService: TaskService,
    private cdr: ChangeDetectorRef,
  ) {}

  get isEditMode() {
    return !!this.task;
  }

  form = {
    title: '',
    description: '',
    categoryId: '',
    status: 'Todo' as TaskItemStatus,
  };

  error = '';

  ngOnChanges() {
    this.form = {
      title: this.task?.title ?? '',
      description: this.task?.description ?? '',
      categoryId: this.task?.categoryId ?? '',
      status: this.task?.status ?? 'Todo',
    };
    this.error = '';
  }

  onSaveTask() {
    this.error = '';
    const request = {
      title: this.form.title,
      description: this.form.description || undefined,
      categoryId: this.form.categoryId || undefined,
      status: this.form.status,
    };

    const action$: Observable<unknown> = this.isEditMode
      ? this.taskService.update(this.task!.id, request)
      : this.taskService.create(request);

    action$.subscribe({
      next: () => this.saved.emit(),
      error: (err) => {
        this.error = err.error?.errors?.[0] ?? err.error?.message ?? 'Failed to save task.';
        this.cdr.markForCheck();
      },
    });
  }
}
