﻿using Bg.EduSocial.Domain.Cores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("exam")]
    public class ExamEntity: BaseEntity
    {
        [Key]
        public Guid exam_id { get; set; }
        public Guid user_id { get; set; }
        public Guid test_id { get; set; }
        public decimal point { get; set; }

    }
}
