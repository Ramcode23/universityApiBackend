import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CourseCreateDto, CourseDetailDto } from 'src/app/services/api/models';
import { CoursesService } from 'src/app/services/courses/courses.service';

@Component({
  selector: 'app-enroll-card',
  templateUrl: './enroll-card.component.html',
  styleUrls: ['./enroll-card.component.css']
})
export class EnrollCardComponent implements OnInit {
  course?: CourseDetailDto;
  @Input() course$?: Observable<CourseDetailDto>;
  constructor(
    private router: Router,

  ) { }

  ngOnInit(): void {

    this.course$?.subscribe(course => {
      this.course = course;
      console.log(this.course);

    })
  }
  navigateBack() {
    this.router.navigate(['/main/enrollments']);
  }
}
