import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PermissionGuard } from 'src/app/guards/permission.guard';
import { CategoriesPageComponent } from './categories/categories-page/categories-page.component';
import { CategoryDetailPageComponent } from './categories/category-detail-page/category-detail-page.component';
import { CourseDetailPageComponent } from './courses/course-detail-page/course-detail-page.component';

import { CoursesPageComponent } from './courses/courses-page/courses-page.component';
import { EnrollmentDetailComponent } from './enrolls/enrollment-detail/enrollment-detail.component';
import { EnrollmentsPageComponent } from './enrolls/enrollments-page/enrollments-page.component';
import { MainComponent } from './main.component';
import { StudentDetailPageComponent } from './students/student-detail-page/student-detail-page.component';
import { StudentsPageComponent } from './students/students-page/students-page.component';

const routes: Routes = [

  {
    path: '', component: MainComponent, children: [
      { path: 'categories', component: CategoriesPageComponent,canActivate:[PermissionGuard] },
      {path:'categories/:id',component:CategoryDetailPageComponent,canActivate:[PermissionGuard]},
      {path:'categories/new',component:CategoryDetailPageComponent,canActivate:[PermissionGuard]},
      { path: 'courses', component: CoursesPageComponent },
      {path:'enrollments',component:EnrollmentsPageComponent},
      {path:'enrollments/:id',component:EnrollmentDetailComponent},
      { path: 'courses/:id', component: CourseDetailPageComponent ,canActivate:[PermissionGuard]},
      { path: 'courses/new', component: CourseDetailPageComponent,canActivate:[PermissionGuard] },
      { path: 'students', component: StudentsPageComponent,canActivate:[PermissionGuard]},
      { path: 'students/:id', component: StudentDetailPageComponent,canActivate:[PermissionGuard]},
      { path: 'student', component: StudentDetailPageComponent,canActivate:[PermissionGuard]},

      { path: '**', redirectTo: '' }
    ]
  }

];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
