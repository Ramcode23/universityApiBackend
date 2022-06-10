import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoriesService } from '../../../../services/categories/categories.service';
import { CategoryDto } from '../../../../services/api/models';

@Component({
  selector: 'app-categories-page',
  templateUrl: './categories-page.component.html',
  styleUrls: ['./categories-page.component.css']
})
export class CategoriesPageComponent implements OnInit {
categories$!: Observable<CategoryDto[]>;
  constructor(private categoriesService:CategoriesService) {

    this.categories$ = this.categoriesService.getAllCategories({ pageNumber: 1, resultsPage: 10 });
  }

  ngOnInit(): void {
  }

}
