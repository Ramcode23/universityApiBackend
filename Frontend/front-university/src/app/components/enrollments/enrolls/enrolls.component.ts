import { Component, Input } from '@angular/core';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { CourseDto } from 'src/app/services/api/models';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-enrolls',
  templateUrl: './enrolls.component.html',
  styleUrls: ['./enrolls.component.css']
})
export class EnrollsComponent {
  courses: CourseDto[] = [];
  @Input() courses$!: Observable<CourseDto[]>;
  /** Based on the screen size, switch from standard to one column per row */


  constructor(
    private breakpointObserver: BreakpointObserver,
    private router: Router,
  ) {

    this.courses$;

  }


  getDetail(course: any) {
    this.router.navigate(['/main/enrollments/', course.id, course]);
    console.log(course.id,);
  }
}
