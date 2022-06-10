/* tslint:disable */
/* eslint-disable */
import { Address } from './address';
import { Course } from './course';
import { User } from './user';
export interface Student {
  address?: Address;
  courses?: null | Array<Course>;
  createdAt?: string;
  createdBy?: User;
  deletedBy?: User;
  deleteteAt?: null | string;
  dob: string;
  id: number;
  isDeleted?: boolean;
  updatedAt?: null | string;
  updatedBy?: User;
  user?: User;
}
