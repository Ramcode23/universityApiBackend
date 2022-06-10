import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { StundentListDto } from 'src/app/services/api/models';
import { StudentsService } from '../../../../services/students/students.service';

@Component({
  selector: 'app-students-page',
  templateUrl: './students-page.component.html',
  styleUrls: ['./students-page.component.css']
})
export class StudentsPageComponent implements OnInit {

  //observable of students;
  students!: Observable<StundentListDto[]>;
  constructor(private studentService: StudentsService) {

    this.students = this.studentService.getAllStudents({ pageNumber: 1, resultsPage: 10 });

  }

  ngOnInit(): void {



  }

}
