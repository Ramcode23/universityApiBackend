using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApiBackend.DTOs.Chapters
{
    public class ChapterDetailDTO
    {
        
        public int Id { get; set; }
        
        public List<LessonDTO> Lessons { get; set; } = new List<LessonDTO>();
    }
}