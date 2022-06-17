import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import {  RegisterStudent } from 'src/app/services/api/models';
import { StudentDetailDTO } from 'src/app/services/api/models/studentDetailDTO';
import { StudentsService } from 'src/app/services/students/students.service';


@Component({
  selector: 'app-student-detail-page',
  templateUrl: './student-detail-page.component.html',
  styleUrls: ['./student-detail-page.component.css']
})
export class StudentDetailPageComponent implements OnInit {

  student!: RegisterStudent;
  student$: Observable<StudentDetailDTO> | undefined;
  constructor(
    private activeRouter: ActivatedRoute,
    private studentsService: StudentsService,

  ) { }

  ngOnInit(): void {
    this.student = this.activeRouter.snapshot.params as RegisterStudent;
    this.getData();
  }

  getData(){
    if (this.student.id) {
      this.student$ = this.studentsService.getStudentById(this.student.id)

    }

  }

}
