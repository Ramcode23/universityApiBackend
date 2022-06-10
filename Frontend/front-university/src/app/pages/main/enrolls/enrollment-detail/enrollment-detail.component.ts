import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CourseCreateDto, CourseDetailDto, CourseDto } from 'src/app/services/api/models';
import { CoursesService } from 'src/app/services/courses/courses.service';

@Component({
  selector: 'app-enrollment-detail',
  templateUrl: './enrollment-detail.component.html',
  styleUrls: ['./enrollment-detail.component.css']
})
export class EnrollmentDetailComponent implements OnInit {

  course$?: Observable<CourseDetailDto>
  id?: number;
  courseDTO?: CourseDto;
  constructor(private _router: Router,
    private activeRouter: ActivatedRoute,
    private courseService: CoursesService
  ) { }

  ngOnInit(): void {

    this.id = this.activeRouter.snapshot.params['id'];
    this.getData();
  }


  getData() {
    if (this.id) {
      this.course$ = this.courseService.getCourseById(this.id);

    }
  }
}
