using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversityApiBackend.DTOs.Chapters;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DTOs.Courses
{
    public class CourseDetailDTO
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string PublicGoal { get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;

        public ChapterDetailDTO Chapter { get; set; }
    }
}