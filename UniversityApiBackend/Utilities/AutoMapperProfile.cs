﻿using AutoMapper;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.DTOs.Categories;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.DTOs.Courses;
using UniversityApiBackend.DTOs.Students;

using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Utilities
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {


            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryDTO, Category>().ReverseMap();

            CreateMap<CategoryListDTO, Category>();
            CreateMap<CategoryListDTO, Category>().ReverseMap();

            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryCreateDTO, Category>().ReverseMap();

            CreateMap<CategoryCourseDTO, Category>();

            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>();
            // Map CourseCreateDTO to Course
            CreateMap<CourseCreateDTO, Course>().PreserveReferences();

            CreateMap<CourseDetailDTO, Course>().ReverseMap();
            CreateMap<CourseDetailDTO, Course>();
            
            CreateMap<Student, StudentDTO>()

                .ForMember(
                    dest => dest.Dob,
                    opt => opt.MapFrom(src => $"{src.Dob}")
                )

                .ForMember(
                    dest => dest.Adress,
                    opt => opt.MapFrom(src => $"{src.Address}")
                ).ReverseMap();


            CreateMap<Student, StudentDTO>()

                .ForMember(
                    dest => dest.Dob,
                    opt => opt.MapFrom(src => $"{src.Dob}")
                )

                .ForMember(
                    dest => dest.Adress,
                    opt => opt.MapFrom(src => $"{src.Address}")
                );


            // Map CourseCreateDTO to Course

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Student, AddressDTO>();


            CreateMap<Chapter, ChapterDTO>().ReverseMap();
            CreateMap<Chapter, ChapterDTO>();
           
            CreateMap<Chapter, ChapterDetailDTO>().ReverseMap();
            CreateMap<Chapter, ChapterDetailDTO>();


            CreateMap<Lesson, LessonDTO>().ReverseMap();
            CreateMap<Lesson, LessonDTO>();





        }
    }
}
