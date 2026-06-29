import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TaskFilterModel, TaskResponse } from "../../core/models/task.model";
import { CategoryResponse } from "../../core/models/category.model";
import { TaskService } from "../../core/services/task.service";
import { CategoryService } from "../../core/services/category.service";

@Component({
    selector: 'app-tasks',
    standalone: true,
    imports: [FormsModule],
    templateUrl: './tasks.component.html'
})

export class TasksComponent implements OnInit {
    tasks: TaskResponse[] = [];
    categories: CategoryResponse[] = [];
    totalPages = 0;

    filter: TaskFilterModel = {
        pageNumber: 1,
        pageSize: 10,
    }

    constructor(
        private taskService: TaskService,
        private categoryService: CategoryService
    ) {}

    ngOnInit() {
        this.loadTasks();
        this.loadCategories();
    }

    loadTasks() {
        this.taskService.getPaged(this.filter).subscribe({
            next: (response) => {
                this.tasks = response.items;
                this.totalPages = response.totalPages;
            }
        });
    }

    loadCategories() {
        this.categoryService.getAll().subscribe({
            next: (categories) => this.categories = categories
        });
    }
}