/* tslint:disable */
/* eslint-disable */
import { Course } from './course';
import { Lesson } from './lesson';
import { User } from './user';
export interface Chapter {
  course?: Course;
  courseId?: number;
  createdAt?: string;
  createdBy?: User;
  deletedBy?: User;
  deleteteAt?: null | string;
  id: number;
  isDeleted?: boolean;
  lessons: Array<Lesson>;
  updatedAt?: null | string;
  updatedBy?: User;
}
