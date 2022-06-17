import { AddressDto } from "./address-dto";
import { StudentCourseDTO } from "./student-dto";
import { User } from "./user";

export interface StudentCreateDTO {
  id?:      number;
  dob?:     Date;
  user?:    User;
  courses?: StudentCourseDTO[];
  address?: AddressDto;
}



