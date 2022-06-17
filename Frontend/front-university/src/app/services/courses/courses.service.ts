import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CourseCreateDto, CourseDetailDto, CourseDto } from '../api/models';
import { CourseListDTO } from '../api/models/courseListDTO';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  apiUrl = environment.apiUrl+'courses';
  constructor(private http: HttpClient) { }

  getAllCourses(
    params?: {
      pageNumber?: number;
      resultsPage?: number;
    }) {
    return this.http.get<CourseDto[]>(this.apiUrl + '/', { params });
  }
  getCoursesList(){
    return this.http.get<CourseListDTO[]>(this.apiUrl + '/CoursesList');
  }

  searchCourse
    (params?: {
      Name?: string;
      CategoryName?: string;
      RangeStudents?: Array<number>;
    }) {
      debugger;
    return this.http.get<CourseDto[]>(this.apiUrl + '/Search', { params });
  }


  getCourseById(id?: number) {
    return this.http.get<CourseDetailDto>(this.apiUrl + '/' + id);
  }

  createCourse(Course: CourseCreateDto) {
    return this.http.post<CourseDto>(this.apiUrl + '/', Course);
  }

  updateCourse(Course: CourseCreateDto) {
    return this.http.put(this.apiUrl + '/' + Course.id, Course);
  }

  deleteCourse(id: number) {
    return this.http.delete(this.apiUrl + '/' + id);
  }

}
