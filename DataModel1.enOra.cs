﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025. 04. 01. 12:52:16
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
    public partial class enOra {

        public enOra()
        {
            OnCreated();
        }

        public int id { get; set; }

        public int tantargy_id { get; set; }

        public int tanar_id { get; set; }

        public int osztaly_id { get; set; }

        public int tanev_id { get; set; }

        public TimeSpan kezdet { get; set; }

        public TimeSpan vege { get; set; }

        public byte nap { get; set; }

        public DateTime erv_kezdet { get; set; }

        public DateTime erv_vege { get; set; }

        public virtual enTantargy enTantargy { get; set; }

        public virtual enFelhasznalo enFelhasznalo { get; set; }

        public virtual enOsztaly enOsztaly { get; set; }

        public virtual enTanev enTanev { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
