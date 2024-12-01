﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Cores
{
    public class FilterCondition
    {
        /// <summary>
        /// Tên trường cần lọc.
        /// </summary>
        public string Field { get; set; } = string.Empty;

        /// <summary>
        /// Toán tử lọc (EQ, NE, GT, LT, LIKE, IN, ...).
        /// </summary>
        public FilterOperator Operator { get; set; } = FilterOperator.Equal;

        /// <summary>
        /// Giá trị cần so sánh.
        /// </summary>
        public object? Value { get; set; }
    }
}