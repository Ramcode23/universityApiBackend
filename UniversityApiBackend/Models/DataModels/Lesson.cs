using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace UniversityApiBackend.Models.DataModels
{
    public class Lesson
    {
        [Key]
     public int Id { get; set; }
    public string? Tittle { get; set; }=string.Empty;
        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}