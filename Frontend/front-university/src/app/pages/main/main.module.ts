import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';

import { StudentsPageComponent } from './students/students-page/students-page.component';
import { CoursesPageComponent } from './courses/courses-page/courses-page.component';
import { CategoriesPageComponent } from './categories/categories-page/categories-page.component';
import { ComponentsModule } from '../../components/components.module';
import { MaterialModule } from '../../modules/material/material.module';
import { StudentDetailPageComponent } from './students/student-detail-page/student-detail-page.component';
import { CourseDetailPageComponent } from './courses/course-detail-page/course-detail-page.component';
import { CategoryDetailPageComponent } from './categories/category-detail-page/category-detail-page.component';
import { EnrollmentsPageComponent } from './enrolls/enrollments-page/enrollments-page.component';
import { EnrollmentDetailComponent } from './enrolls/enrollment-detail/enrollment-detail.component';


@NgModule({
  declarations: [
    MainComponent,
    CategoriesPageComponent,
    StudentsPageComponent,
    CoursesPageComponent,
    StudentDetailPageComponent,
    CourseDetailPageComponent,
    CategoryDetailPageComponent,
    EnrollmentsPageComponent,
    EnrollmentDetailComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    ComponentsModule,
    MaterialModule
  ]
})
export class MainModule { }
