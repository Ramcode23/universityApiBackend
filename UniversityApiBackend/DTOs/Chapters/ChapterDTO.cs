using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApiBackend.DTOs.Chapters
{
    public class ChapterDTO
    {
           public int CourseId { get; set; }

        [Required]
        public string List { get; set; }=string.Empty;
    }
}