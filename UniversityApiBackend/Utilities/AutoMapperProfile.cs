using AutoMapper;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.DTOs.Students;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Utilities
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
           

            CreateMap< CategoryDTO, Category>();
            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap< CourseDTO, Course>();
            CreateMap<CourseDTO, Course>().ReverseMap();

            CreateMap<StudentDTO, Student>();
            CreateMap<StudentDTO, Student>().ReverseMap();











        }
    }
}
