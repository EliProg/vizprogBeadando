﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025. 04. 15. 13:18:54
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Models
{
    public partial class enSubject {

        public enSubject()
        {
            this.enTimetableLessons = new List<enTimetableLesson>();
            OnCreated();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IList<enTimetableLesson> enTimetableLessons { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
