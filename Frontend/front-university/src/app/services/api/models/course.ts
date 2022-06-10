/* tslint:disable */
/* eslint-disable */
import { Category } from './category';
import { Chapter } from './chapter';
import { Level } from './level';
import { Student } from './student';
import { User } from './user';
export interface Course {
  categories: Array<Category>;
  chapter: Chapter;
  createdAt?: string;
  createdBy?: User;
  deletedBy?: User;
  deleteteAt?: null | string;
  description: string;
  id: number;
  isDeleted?: boolean;
  level?: Level;
  name: string;
  shortDescription: string;
  students: Array<Student>;
  updatedAt?: null | string;
  updatedBy?: User;
}
