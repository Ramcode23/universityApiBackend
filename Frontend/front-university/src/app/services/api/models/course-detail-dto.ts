/* tslint:disable */
/* eslint-disable */
import { Category } from './category';
import { ChapterDetailDto } from './chapter-detail-dto';
import { Level } from './level';
export interface CourseDetailDto {
  id:               number;
  name:             string;
  shortDescription: string;
  description:      string;
  publicGoal:       string;
  level:            Level;
  chapter:          ChapterDetailDto;
  categories:       Category[];
}




