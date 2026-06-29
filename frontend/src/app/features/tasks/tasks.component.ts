import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TaskFilterModel, TaskPagedResponse, TaskResponse } from "../../core/models/task.model";
import { CategoryResponse } from "../../core/models/category.model";
import { TaskService } from "../../core/services/task.service";
import { CategoryService } from "../../core/services/category.service";
import { AsyncPipe } from "@angular/common";
import { Observable } from "rxjs";

@Component({
    selector: 'app-tasks',
    standalone: true,
    imports: [FormsModule, AsyncPipe],
    templateUrl: './tasks.component.html'
})

export class TasksComponent implements OnInit {
    tasks$!: Observable<TaskPagedResponse>;
    categories$!: Observable<CategoryResponse[]>;

    filter: TaskFilterModel = {
        pageNumber: 1,
        pageSize: 10,
    }

    constructor(
        private taskService: TaskService,
        private categoryService: CategoryService,
    ) {}

    ngOnInit() {
        this.loadTasks();
        this.loadCategories();
    }

    loadTasks() {
        this.tasks$ = this.taskService.getPaged(this.filter);
    }

    loadCategories() {
        this.categories$ = this.categoryService.getAll();
    }
}