import { AddressDto } from "./address-dto";
import { Course } from "./course";
import { CourseListDTO } from "./courseListDTO";
import { User } from "./user";

export interface StudentDetailDTO {
  id?:      number;
  dob?:     Date;
  user?:    User;
  address?:  AddressDto;
  courses?: CourseListDTO[];
}


