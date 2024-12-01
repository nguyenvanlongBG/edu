﻿using Bg.EduSocial.Domain.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Classes
{
    public interface IEnrollmentClassRepository: IWriteRepository<EnrollmentClassEntity>
    {
    }
}