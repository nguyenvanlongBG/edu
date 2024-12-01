﻿using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bg.EduSocial.Domain
{
    [Table("classroom")]
    public class ClassroomEntity : BaseEntity
    {
        [Key]
        public Guid classroom_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
     
    }
}