import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CourseDto, RegisterStudent } from 'src/app/services/api/models';
import { AuthService } from 'src/app/services/auth.service';
import { StudentsService } from 'src/app/services/students/students.service';


@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent implements OnInit {
  @Input() student: RegisterStudent | undefined;
  studentForm = this.fb.group({});
  panelOpenState = false;
  isEdit: boolean = false;
  courses: Array<any> = [
    { id: 1, name: 'Course 1' },
    { id: 2, name: 'Course 2' },
    { id: 3, name: 'Course 3' },
    { id: 4, name: 'Course 4' },
    { id: 5, name: 'Course 5' },
    { id: 6, name: 'Course 6' },
    { id: 7, name: 'Course 7' },
    { id: 8, name: 'Course 8' },
    { id: 9, name: 'Course 9' },
  ];

  errors: any[] = [];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private StudentsService: StudentsService,

  ) {


  }

  ngOnInit(): void {
    this.studentForm = this.fb.group({
      firstName: [this.student?.firstName || '', Validators.required],
      lastName: [this.student?.lastName || '', Validators.required],
      email: [this.student?.email || '', [Validators.required, Validators.email]],
      password: ['Pass1234@', Validators.required],
      dob: [this.student?.dob || '', Validators.required],
      city: [this.student?.city || '', Validators.required],
      street: [this.student?.state || '', Validators.required],
      zipCode: [this.student?.zipCode || '', Validators.required],
      country: [this.student?.country || '', Validators.required],
      comunity: [this.student?.comunity || '', Validators.required],

    });
    this.getState();

  }

  enableForm(): void {
    this.studentForm.enable();

  }

  onSubmit(): void {
    if (this.studentForm.valid) {
      if (this.isEdit) {
        this.createStudent();
      } else {
        this.updateStudent();
      }
    }
  }

  disableForm(): void {
    this.studentForm.disable();
    this.studentForm.reset();
    this.errors = [];
  }
  navigateBack() {
    this.router.navigate(['/main/students']);
  }
  createStudent(): void {

    this.authService.authRegisterUser(this.studentForm.value).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      },
      (error) => {
        console.log(error);
        // chekc if error is array or string
        if (error.error instanceof Array) {
          this.errors = error.error;
        }
        else {
          this.errors = [error.error];
        }

      }
    );

  }

  updateStudent(): void {
    this.StudentsService.updateStudent(this.studentForm.value).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      }
    );
  }

  getState() {
    if (this.student?.lastName != undefined && this.student?.lastName != null) {
      this.isEdit = true;
      return;
    }
    this.isEdit = false;
  }

}

