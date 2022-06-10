import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Category, CategoryCourseDto, CategoryDto, ChapterDto, CourseCreateDto, CourseDetailDto, Lesson, LessonDto, Level } from 'src/app/services/api/models';
import { CoursesService } from 'src/app/services/courses/courses.service';

import { CategoriesService } from '../../../services/categories/categories.service';

@Component({
  selector: 'app-course-form',
  templateUrl: './course-form.component.html',
  styleUrls: ['./course-form.component.css']
})
export class CourseFormComponent implements OnInit {
  @Input() course$?: Observable<CourseDetailDto>;
  course?: CourseDetailDto;
  categories: Category[] = [];
  levels: any[] = ['Basic', 'Medium', 'Advanced', 'Expert'];
  panelOpenState = false;
  courseForm = this.fb.group({});


  isEdit: boolean = false;
  errors: any[] = [];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private categoriesService: CategoriesService,
    private courseService: CoursesService

  ) {

  }

  get lessons() {
    return this.courseForm.controls['lessons'] as FormArray;
  }



  ngOnInit(): void {

    this.categoriesService.getCategoryList().subscribe(
      (data: Category[]) => {
        this.categories = data;
      });


    this.courseForm = this.fb.group({
      name: ['', Validators.required],
      shortDescription: ['', Validators.required],
      description: ['', Validators.required],
      publicGoal: ['', Validators.required],
      levels: ['', Validators.required],
      categories: ['', Validators.required],
      lessons: this.fb.array([]),

    });

    this.course$?.subscribe(course => {
      this.course = course;
      console.log(this.course);
      this.loadData(course);
      this.getState();
    })


  }


  enableForm(): void {
    this.courseForm.enable();

  }

  onSubmit(): void {
    if (this.courseForm.valid) {
      if (this.isEdit) {
        this.updateCourse();
      } else {
        this.createCourse();
      }
    }
  }


  disableForm(): void {
    this.courseForm.disable();
    this.courseForm.reset();
    this.errors = [];
  }

  navigateBack() {
    this.router.navigate(['/main/courses']);
  }

  createCourse(): void {
    const newCourse = this.formtoCourse();
    this.courseService.createCourse(newCourse).subscribe(
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

  updateCourse(): void {
    const editCourse = this.formtoCourse();
    this.courseService.updateCourse(editCourse).subscribe(
      (response) => {
        console.log(response);
        this.navigateBack();
      }
    );
  }

  getState() {
    if (this.course?.name != undefined && this.course?.name != null) {
      this.isEdit = true;
      return;
    }
    this.isEdit = false;
  }


  addLesson(event: any) {
    event.preventDefault();
    /*  const lessonForm = this.fb.control({
       title: ['', Validators.required],
     }); */
    this.lessons.push(this.fb.control('', Validators.required),);
  }


  deleteLesson(lessonIndex: number) {
    this.lessons.removeAt(lessonIndex);
  }

  converToArrayCategoryDto(categories: any[] | CategoryCourseDto[]): CategoryCourseDto[] {
    return categories.map((category) => {
      return { id: category };
    });
  }

  loadData(course?: CourseDetailDto) {

    this.courseForm.patchValue({
      name: course?.name,
      shortDescription: course?.shortDescription,
      description: course?.description,
      publicGoal: course?.publicGoal,
      levels: course?.level,
      categories: [...course!.categories!.map(category => category.id) || ''],
      lessons: []

    });


    if (course?.chapter?.lessons) {
      course.chapter.lessons.forEach(lesson => {
        this.lessons.push(this.fb.control(lesson.tittle));
      });
    }


  }

  formtoCourse(): CourseCreateDto {

    const {
      id,
      name
      , shortDescription
      , description
      , publicGoal
      , levels
      , categories
      , lessons } = this.courseForm.value;

    return {
      id: this.course?.id || 0,
      name,
      shortDescription,
      description,
      publicGoal,
      levels,
      categories: this.converToArrayCategoryDto(categories),
      chapter: {
        id: 0,
        lessons: lessons.map((lesson: string) => ({ id: 0, tittle: lesson })) as LessonDto[]
      } as ChapterDto
    };



  }



}
