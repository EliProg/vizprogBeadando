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
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace cnOrarend
{

    public partial class OrarendContext : DbContext
    {

        public OrarendContext() :
            base()
        {
            OnCreated();
        }

        public OrarendContext(DbContextOptions<OrarendContext> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<enFelhasznalo> enFelhasznalos
        {
            get;
            set;
        }

        public virtual DbSet<enOsztaly> enOsztalies
        {
            get;
            set;
        }

        public virtual DbSet<enTantargy> enTantargies
        {
            get;
            set;
        }

        public virtual DbSet<enOra> enOras
        {
            get;
            set;
        }

        public virtual DbSet<enTanev> enTanevs
        {
            get;
            set;
        }

        public virtual DbSet<enLog> enLogs
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.enFelhasznaloMapping(modelBuilder);
            this.CustomizeenFelhasznaloMapping(modelBuilder);

            this.enOsztalyMapping(modelBuilder);
            this.CustomizeenOsztalyMapping(modelBuilder);

            this.enTantargyMapping(modelBuilder);
            this.CustomizeenTantargyMapping(modelBuilder);

            this.enOraMapping(modelBuilder);
            this.CustomizeenOraMapping(modelBuilder);

            this.enTanevMapping(modelBuilder);
            this.CustomizeenTanevMapping(modelBuilder);

            this.enLogMapping(modelBuilder);
            this.CustomizeenLogMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region enFelhasznalo Mapping

        private void enFelhasznaloMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enFelhasznalo>().ToTable(@"Felhasznalo");
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.nev).HasColumnName(@"nev").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.felhasznalo_nev).HasColumnName(@"felhasznalo_nev").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.rang).HasColumnName(@"rang").IsRequired().ValueGeneratedNever().HasMaxLength(255);
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.jelszo).HasColumnName(@"jelszo").IsRequired().ValueGeneratedNever().HasMaxLength(255);
            modelBuilder.Entity<enFelhasznalo>().Property(x => x.oktatasi_azonosito).HasColumnName(@"oktatasi_azonosito").ValueGeneratedNever().HasMaxLength(12);
            modelBuilder.Entity<enFelhasznalo>().HasKey(@"id");
            modelBuilder.Entity<enFelhasznalo>().HasIndex(@"felhasznalo_nev").IsUnique(true);
            modelBuilder.Entity<enFelhasznalo>().HasIndex(@"oktatasi_azonosito").IsUnique(true);
        }

        partial void CustomizeenFelhasznaloMapping(ModelBuilder modelBuilder);

        #endregion

        #region enOsztaly Mapping

        private void enOsztalyMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enOsztaly>().ToTable(@"Osztaly");
            modelBuilder.Entity<enOsztaly>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enOsztaly>().Property(x => x.nev).HasColumnName(@"nev").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<enOsztaly>().HasKey(@"id");
        }

        partial void CustomizeenOsztalyMapping(ModelBuilder modelBuilder);

        #endregion

        #region enTantargy Mapping

        private void enTantargyMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enTantargy>().ToTable(@"Tantargy");
            modelBuilder.Entity<enTantargy>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enTantargy>().Property(x => x.nev).HasColumnName(@"nev").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<enTantargy>().HasKey(@"id");
        }

        partial void CustomizeenTantargyMapping(ModelBuilder modelBuilder);

        #endregion

        #region enOra Mapping

        private void enOraMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enOra>().ToTable(@"Ora");
            modelBuilder.Entity<enOra>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enOra>().Property(x => x.tantargy_id).HasColumnName(@"tantargy_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.tanar_id).HasColumnName(@"tanar_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.osztaly_id).HasColumnName(@"osztaly_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.tanev_id).HasColumnName(@"tanev_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.kezdet).HasColumnName(@"kezdet").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.vege).HasColumnName(@"vege").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.nap).HasColumnName(@"nap").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.erv_kezdet).HasColumnName(@"erv_kezdet").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().Property(x => x.erv_vege).HasColumnName(@"erv_vege").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enOra>().HasKey(@"id");
        }

        partial void CustomizeenOraMapping(ModelBuilder modelBuilder);

        #endregion

        #region enTanev Mapping

        private void enTanevMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enTanev>().ToTable(@"Tanev");
            modelBuilder.Entity<enTanev>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enTanev>().Property(x => x.kezdet).HasColumnName(@"kezdet").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enTanev>().Property(x => x.vege).HasColumnName(@"vege").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enTanev>().HasKey(@"id");
        }

        partial void CustomizeenTanevMapping(ModelBuilder modelBuilder);

        #endregion

        #region enLog Mapping

        private void enLogMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enLog>().ToTable(@"enLogs");
            modelBuilder.Entity<enLog>().Property(x => x.id).HasColumnName(@"id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<enLog>().Property(x => x.description).HasColumnName(@"description").IsRequired().ValueGeneratedNever().HasMaxLength(200);
            modelBuilder.Entity<enLog>().Property(x => x.idopont).HasColumnName(@"idopont").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<enLog>().HasKey(@"id");
        }

        partial void CustomizeenLogMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<enFelhasznalo>().HasMany(x => x.enOras).WithOne(op => op.enFelhasznalo).HasForeignKey(@"tanar_id").IsRequired(true);

            modelBuilder.Entity<enOra>().HasOne(x => x.enTantargy).WithMany().HasForeignKey(@"tantargy_id").IsRequired(true);
            modelBuilder.Entity<enOra>().HasOne(x => x.enFelhasznalo).WithMany(op => op.enOras).HasForeignKey(@"tanar_id").IsRequired(true);
            modelBuilder.Entity<enOra>().HasOne(x => x.enOsztaly).WithMany().HasForeignKey(@"osztaly_id").IsRequired(true);
            modelBuilder.Entity<enOra>().HasOne(x => x.enTanev).WithMany().HasForeignKey(@"tanev_id").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
