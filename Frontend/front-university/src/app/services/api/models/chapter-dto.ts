/* tslint:disable */
/* eslint-disable */
import { LessonDto } from './lesson-dto';
export interface ChapterDto {
  courseId: number;
  id?: number;
  lessons: Array<LessonDto>;
}
