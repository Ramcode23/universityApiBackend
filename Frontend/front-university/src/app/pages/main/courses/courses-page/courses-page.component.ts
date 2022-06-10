import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CourseDto } from 'src/app/services/api/models';
import { CoursesService } from '../../../../services/courses/courses.service';

@Component({
  selector: 'app-courses-page',
  templateUrl: './courses-page.component.html',
  styleUrls: ['./courses-page.component.css']
})
export class CoursesPageComponent implements OnInit {
  courses$!: Observable<CourseDto[]>;
  constructor(private courseService:CoursesService) {
this.courses$ = this.courseService.getAllCourses({ pageNumber: 1, resultsPage: 10});
  }

  ngOnInit(): void {
  }

}
