import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryCreateDto, CategoryDto } from '../api/models';


@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  apiUrl = environment.apiUrl+'Categories';
  constructor(private http: HttpClient) { }


  getAllCategories(
    params?: {
      pageNumber?: number;
      resultsPage?: number;
    }

  ) {
    return this.http.get<CategoryDto[]>(this.apiUrl , { params });
  }

  getCategoryList() {
    return this.http.get<CategoryDto[]>(this.apiUrl + '/List');
  }

   searchCategory
    (params?: {
      Name?: string;
      rangeCourse?: Array<number>;
    }) {
    return this.http.get<CategoryDto[]>(this.apiUrl + '/Search', { params });
  }


  getCategoryById(id: number) {
    return this.http.get<CategoryDto>(this.apiUrl + '/' + id);
  }

  createCategory(category: CategoryCreateDto) {
    return this.http.post<CategoryDto>(this.apiUrl + '/', category);
  }

  updateCategory(category: CategoryCreateDto) {
    return this.http.put(this.apiUrl + '/' + category.id, category);
  }

  deleteCategory(id: number) {
    return this.http.delete(this.apiUrl + '/' + id);
  }
}
