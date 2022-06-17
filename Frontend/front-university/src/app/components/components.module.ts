import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { MaterialModule } from '../modules/material/material.module';
import { MenuItemComponent } from './menu-item/menu-item.component';
import { LoginFormComponent } from './auth/login-form/login-form.component';
import { RegisterFormComponent } from './auth/register-form/register-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainRoutingModule } from '../pages/main/main-routing.module';
import { StudentsTableComponent } from './students/students-table/students-table.component';
import { CoursesTableComponent } from './courses/courses-table/courses-table.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { CategoriesTableComponent } from './categories/categories-table/categories-table.component';
import { StudentFormComponent } from './students/student-form/student-form.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { DialogComponent } from './dialog/dialog.component';
import {MatDialogModule} from '@angular/material/dialog';
import { CategoryFormComponent } from './categories/category-form/category-form.component';
import { CourseFormComponent } from './courses/course-form/course-form.component';
import { EnrollsComponent } from './enrollments/enrolls/enrolls.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { LayoutModule } from '@angular/cdk/layout';
import { EnrollCardComponent } from './enrollments/enroll-card/enroll-card.component';

@NgModule({
  declarations: [
    LoginFormComponent,
    RegisterFormComponent,
    NavComponent,
    MenuItemComponent,
    StudentsTableComponent,
    CoursesTableComponent,
    CategoriesTableComponent,
    StudentFormComponent,
    DialogComponent,
    CategoryFormComponent,
    CourseFormComponent,
    EnrollsComponent,
    EnrollCardComponent,


  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    MainRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatDatepickerModule,
    MatDialogModule,
    MatGridListModule,
    MatMenuModule,
    MatIconModule,
    LayoutModule,
    FormsModule
  ],
  exports: [
    LoginFormComponent,
    RegisterFormComponent,
    MenuItemComponent,
    StudentsTableComponent,
    StudentFormComponent,
    MatDatepickerModule,
    DialogComponent,
    CategoriesTableComponent,
    CoursesTableComponent,
    CategoryFormComponent,
    MatSelectModule,
    CourseFormComponent,
    EnrollsComponent,
    EnrollCardComponent,
  ]
})
export class ComponentsModule { }
