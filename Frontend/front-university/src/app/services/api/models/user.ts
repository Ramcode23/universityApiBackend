/* tslint:disable */
/* eslint-disable */
import { Category } from './category';
import { Chapter } from './chapter';
import { Course } from './course';
import { Student } from './student';
export interface User {
  accessFailedCount?: number;
  categoriesCreatedBy?: null | Array<Category>;
  categoriesDeletedBy?: null | Array<Category>;
  categoriesUpdatedBy?: null | Array<Category>;
  chaptersCreatedBy?: null | Array<Chapter>;
  chaptersDeletedBy?: null | Array<Chapter>;
  chaptersUpdatedBy?: null | Array<Chapter>;
  concurrencyStamp?: null | string;
  coursesCreatedBy?: null | Array<Course>;
  coursesDeletedBy?: null | Array<Course>;
  coursesUpdatedBy?: null | Array<Course>;
  email: string;
  emailConfirmed?: boolean;
  id?: null | string;
  lastName: string;
  lockoutEnabled?: boolean;
  lockoutEnd?: null | string;
  name: string;
  normalizedEmail?: null | string;
  normalizedUserName?: null | string;
  password: string;
  passwordHash?: null | string;
  phoneNumber?: null | string;
  phoneNumberConfirmed?: boolean;
  securityStamp?: null | string;
  studentsCreatedBy?: null | Array<Student>;
  studentsDeletedBy?: null | Array<Student>;
  studentsUpdatedBy?: null | Array<Student>;
  twoFactorEnabled?: boolean;
  userName?: null | string;
}
