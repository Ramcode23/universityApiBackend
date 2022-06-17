import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { StudentCourseDTO, StudentCreateDTO } from 'src/app/services/api/models';
import { CourseListDTO } from 'src/app/services/api/models/courseListDTO';
import { StudentDetailDTO } from 'src/app/services/api/models/studentDetailDTO';

import { CoursesService } from 'src/app/services/courses/courses.service';
import { StudentsService } from 'src/app/services/students/students.service';


@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent implements OnInit {
  @Input() studentId: number | undefined
  student: StudentCreateDTO | undefined;
  @Input() student$: Observable<StudentDetailDTO> | undefined;
  studentFromDetails: StudentDetailDTO | undefined
  courses: CourseListDTO[] = [];
  coursesList: StudentCourseDTO[] = [];
  courseStudent: CourseListDTO[] | undefined = [];
  studentForm = this.fb.group({});
  selectedValue: number | undefined;
  panelOpenState = false;
  isEdit: boolean = false;


  errors: any[] = [];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private StudentsService: StudentsService,
    private coursesService: CoursesService,

  ) { }

  ngOnInit(): void {
    this.studentForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['Pass1234@', this.isEdit ? null : Validators.required],
      dob: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
      zipCode: ['', Validators.required],
      country: ['', Validators.required],
      comunity: ['', Validators.required],
      state: ['', Validators.required],

    });
    this.getState();
    this.getCourses();
    if (this.isEdit) {
      this.student$?.subscribe(student => {
        this.loadData(student);
      }
      );
    }
  }

  getCourses() {
    this.coursesService.getCoursesList()
      .subscribe(courses => {
        console.log(courses);
        this.courses = courses
      });
  }


  enableForm(): void {
    this.studentForm.enable();

  }

  onSubmit(): void {
    if (this.studentForm.valid) {
      if (!this.isEdit) {
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
    const student = this.formtoStudent();
    this.StudentsService.createStudent(student).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      },
      (error) => {
        console.log(error);
        // chekc if error is array or string
        if (error.error instanceof Array) {
          this.errors = error.error;
          console.log(this.errors);
        }
        else {
          this.errors = [error.error];
        }

      }
    );

  }

  updateStudent(): void {
    const student = this.formtoStudent();
    this.StudentsService.updateStudent(student).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      }
    );
  }

  getState() {
    if (this.studentId != undefined) {
      this.isEdit = true;
      return;
    }
    this.isEdit = false;
  }

  loadData(student?: StudentDetailDTO) {

    this.studentForm.patchValue({
      firstName: student?.user?.name,
      lastName: student?.user?.lastName,
      email: student?.user?.userName,
      password: student?.user?.password,
      dob: student?.dob,
      city: student?.address?.city,
      street: student?.address?.street,
      zipCode: student?.address?.zipCode,
      country: student?.address?.country,
      comunity: student?.address?.comunity,
      state: student?.address?.state,
    });

    this.studentFromDetails = student;

    if (student!.courses!.length > 0) {
      this.courseStudent = student?.courses;
    } else {
      this.student!.courses = [];
    }


  }

  formtoStudent() {
    const {
      firstName,
      lastName,
      email,
      password,
      dob,
      city,
      street,
      zipCode,
      country,
      comunity,
      state } = this.studentForm.value;

    const student: StudentCreateDTO = {
      id: this.studentId||0,
      user!: {
        name: firstName,
        lastName,
        userName: email,
        password,
        email,
      },
      dob,
      address: {
        city,
        street,
        zipCode,
        country,
        comunity,
        state

      },
      courses: this.courseStudent?.map(({ id }) => ({ id })),
    };



    return student;
  }

  deleteCourse(index: number) {
    this.courseStudent = this.courseStudent!.filter((_, i) => i !== index);
  }

  addCourse() {
    debugger;
    if (this.selectedValue !== undefined) {
      if (this.courseStudent!.filter(course => course.id == this.selectedValue).length == 0) {
        const course = this.courses.filter(course => course.id == this.selectedValue)[0];
        this.courseStudent!.push({ id: course.id, name: course.name } as CourseListDTO);
      }
    }

  }

  private _filter(name: string): CourseListDTO[] {
    const filterValue = name.toLowerCase();
    return this.courses.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  displayFn(course: CourseListDTO): string {
    return course && course.name ? course.name : '';
  }

}

