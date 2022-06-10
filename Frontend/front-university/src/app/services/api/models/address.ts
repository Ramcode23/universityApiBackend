/* tslint:disable */
/* eslint-disable */
import { Student } from './student';
export interface Address {
  city?: null | string;
  comunity?: null | string;
  country?: null | string;
  id?: number;
  state?: null | string;
  street?: null | string;
  students?: null | Array<Student>;
  zipCode?: null | string;
}
