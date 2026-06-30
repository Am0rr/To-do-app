import {
  Component,
  Input,
  Output,
  EventEmitter,
  ChangeDetectionStrategy,
  ElementRef,
  ViewChild,
  AfterViewInit,
  OnDestroy,
  NgZone,
} from '@angular/core';
import { TaskResponse, TaskPagedResponse } from '../../../../core/models/task.model';
import { TaskCardComponent } from '../task-card/task-card.component';

const ROW_HEIGHT = 71;
const CONTAINER_PADDING = 48;
const SAFETY_MARGIN_PX = 2;

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
export class TaskListComponent implements AfterViewInit, OnDestroy {
  @Input({ required: true }) pagedResult!: TaskPagedResponse;
  @Output() edit = new EventEmitter<TaskResponse>();
  @Output() delete = new EventEmitter<string>();
  @Output() toggleStatus = new EventEmitter<TaskResponse>();
  @Output() pageChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  @ViewChild('listContainer') listContainer!: ElementRef<HTMLDivElement>;

  private resizeObserver?: ResizeObserver;
  private lastPageSize = 0;

  constructor(private zone: NgZone) {}

  ngAfterViewInit(): void {
    this.resizeObserver = new ResizeObserver((entries) => {
      this.zone.run(() => {
        const height = entries[0].contentRect.height;
        const availableHeight = height - CONTAINER_PADDING - SAFETY_MARGIN_PX;
        const itemsPerPage = Math.max(1, Math.floor(availableHeight / ROW_HEIGHT));

        if (itemsPerPage !== this.lastPageSize) {
          this.lastPageSize = itemsPerPage;
          this.pageSizeChange.emit(itemsPerPage);
        }
      });
    });

    this.resizeObserver.observe(this.listContainer.nativeElement);
  }

  ngOnDestroy(): void {
    this.resizeObserver?.disconnect();
  }

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
