import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RegisterStudent, StudentEnrollDto, StundentListDto } from '../api/models';


@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  apiUrl = environment.apiUrl + 'Students';
  constructor(private http: HttpClient) { }

  getAllStudents(params?: {
    pageNumber?: number;
    resultsPage?: number;
  }) {

    return this.http.get<StundentListDto[]>(this.apiUrl, { params });

  }


  searchStudents(params?: {
    FirstName?: string;
    LastName?: string;
    CourseName?: string;
    Street?: string;
    City?: string;
    State?: string;
    ZipCode?: string;
    Country?: string;
    Comunity?: string;
    RangeAge?: Array<number>;
    CourseCategory?: string;
  }): Observable<StundentListDto[]> {

    return this.http.get<StundentListDto[]>(this.apiUrl + '/Search', { params });
  }

  getStudentById(id: number) {
    return this.http.get(this.apiUrl + '/' + id);

  }
  apiStudentsEnrollPost(studentEnroll:StudentEnrollDto) {
    return this.http.post(this.apiUrl+'/Enroll', studentEnroll);
  }

  deleteStudent(id: number) {
    return this.http.delete(this.apiUrl + '/' + id);
  }

  updateStudent(student: RegisterStudent) {
    return this.http.put(this.apiUrl + '/' + student.id, student);
  }
}


///http://localhost:7108/api/Students?pageNumber=1&resultsPage=10
///https://localhost:7108/api/Students?pageNumber=1&resultsPage=10
