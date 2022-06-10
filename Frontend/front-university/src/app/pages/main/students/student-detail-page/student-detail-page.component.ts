import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterStudent } from 'src/app/services/api/models';


@Component({
  selector: 'app-student-detail-page',
  templateUrl: './student-detail-page.component.html',
  styleUrls: ['./student-detail-page.component.css']
})
export class StudentDetailPageComponent implements OnInit {

  student!: RegisterStudent;
  constructor(private _router: Router, private activeRouter: ActivatedRoute) { }

  ngOnInit(): void {
    this.student= this.activeRouter.snapshot.params as RegisterStudent

  }

}
