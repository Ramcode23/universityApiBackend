import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CourseDto } from 'src/app/services/api/models';
import { CoursesService } from 'src/app/services/courses/courses.service';

@Component({
  selector: 'app-enrollments-page',
  templateUrl: './enrollments-page.component.html',
  styleUrls: ['./enrollments-page.component.css']
})
export class EnrollmentsPageComponent implements OnInit {
  courses$!: Observable<CourseDto[]>;
  constructor(private courseService: CoursesService,
    private router: Router,

  ) { }

  ngOnInit(): void {
    this.courses$ = this.courseService.getAllCourses({ pageNumber: 1, resultsPage: 10 });
  }

  getDetail(course: any) {
    this.router.navigate(['/main/enrollments/', course.id, course]);
    console.log(course.id,);
  }

}
