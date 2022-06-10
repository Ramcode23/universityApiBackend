import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryCreateDto } from '../../../services/api/models';
import { CategoriesService } from '../../../services/categories/categories.service';
@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {
  @Input() category: CategoryCreateDto | undefined;
  isEdit: boolean = false;
  errors: any[] = [];
  categoryForm = this.fb.group({});


  constructor(
    private fb: FormBuilder,
    private router: Router,
    private categoryService: CategoriesService
  ) {


  }
  ngOnInit(): void {
    this.getState();

    this.categoryForm = this.fb.group({
      name: [this.category?.name||'', Validators.required]

    });
  }

  enableForm(): void {
    this.categoryForm.enable();
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      if (this.isEdit) {
        this.updateCategory();
      }
      else {
        this.createCategory();
      }
    }
  }

  disableForm(): void {
    this.categoryForm.disable();
    this.categoryForm.reset();
  }

  navigateBack() {
    this.router.navigate(['/main/categories']);
  }
  createCategory(): void {
    this.categoryService.createCategory(this.categoryForm.value).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      }
    );

  }

  updateCategory(): void {
    this.categoryService.updateCategory({id:this.category?.id,name:this.categoryForm.value.name}).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      }
    );
  }


  getState() {
    if (this.category?.name != undefined && this.category?.name != null) {
      this.isEdit = true;
      return;
    }
    this.isEdit = false;
  }
}
