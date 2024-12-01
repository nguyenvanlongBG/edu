﻿using Bg.EduSocial.Constract.Cores;
using Bg.EduSocial.Domain;
using Bg.EduSocial.Domain.Shared.ModelState;

namespace Bg.EduSocial.Constract
{
    public class OptionEditDto : OptionEntity, IRecordState
    {
        public List<object> object_content { get; set; }
        public ModelState State { get; set; } = ModelState.View;
    }
}