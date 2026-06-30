import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CategoryResponse, CreateCategoryRequest } from '../../../../core/models/category.model';
import { LucideAngularModule, List, Folder } from 'lucide-angular';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [FormsModule, LucideAngularModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './sidebar.component.html',
  host: { class: 'contents' },
})
export class SidebarComponent {
  @Input({ required: true }) categories!: CategoryResponse[];
  @Input() selectedCategoryId: string | null = null;

  @Output() categorySelected = new EventEmitter<{ id: string | null; name: string }>();
  @Output() categoryAdded = new EventEmitter<CreateCategoryRequest>();
  @Output() categoryDeleted = new EventEmitter<string>();

  readonly List = List;
  readonly Folder = Folder;

  showAddCategoryModal = false;
  newCategory = { name: '', description: '' };

  onAddCategory() {
    this.categoryAdded.emit({
      name: this.newCategory.name,
      description: this.newCategory.description || undefined,
    });
    this.newCategory = { name: '', description: '' };
    this.showAddCategoryModal = false;
  }
}
