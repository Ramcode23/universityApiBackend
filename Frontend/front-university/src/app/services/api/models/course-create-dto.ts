/* tslint:disable */
/* eslint-disable */
import { CategoryCourseDto } from './category-course-dto';
import { ChapterDto } from './chapter-dto';
import { Level } from './level';
export interface CourseCreateDto {
  categories?: null | Array<CategoryCourseDto>;
  description: string;
  id?: number;
  levels?: Level;
  name: string;
  publicGoal: string;
  shortDescription: string;
  chapter:ChapterDto;

}
