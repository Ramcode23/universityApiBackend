using AutoMapper;
using UniversityApiBackend.DTOs;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Utilities
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDTO>();

        }
    }
}
