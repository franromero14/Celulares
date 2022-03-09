﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Telefonos.Models;

namespace Celulares.Migrations
{
    [DbContext(typeof(CelularesContext))]
    [Migration("20220308221441_MigracionInicial")]
    partial class MigracionInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity("Celulares.Models.App", b =>
                {
                    b.Property<int>("AppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AppId");

                    b.ToTable("App");
                });

            modelBuilder.Entity("Celulares.Models.Instalacion", b =>
                {
                    b.Property<int>("InstalacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<bool>("Exitosa")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Fecha")
                        .HasColumnType("text");

                    b.Property<int>("OperarioId")
                        .HasColumnType("int");

                    b.Property<int>("TelefonoId")
                        .HasColumnType("int");

                    b.HasKey("InstalacionId");

                    b.HasIndex("AppId");

                    b.HasIndex("OperarioId");

                    b.HasIndex("TelefonoId");

                    b.ToTable("Instalacion");
                });

            modelBuilder.Entity("Celulares.Models.Operario", b =>
                {
                    b.Property<int>("OperarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("OperarioId");

                    b.ToTable("Operario");
                });

            modelBuilder.Entity("Celulares.Models.Sensor", b =>
                {
                    b.Property<int>("SensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("SensorId");

                    b.ToTable("Sensor");
                });

            modelBuilder.Entity("Celulares.Models.Telefono", b =>
                {
                    b.Property<int>("TelefonoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .HasColumnType("text");

                    b.Property<string>("Modelo")
                        .HasColumnType("text");

                    b.Property<float>("Precio")
                        .HasColumnType("float");

                    b.HasKey("TelefonoId");

                    b.ToTable("Telefono");
                });

            modelBuilder.Entity("SensorTelefono", b =>
                {
                    b.Property<int>("SensoresSensorId")
                        .HasColumnType("int");

                    b.Property<int>("TelefonosTelefonoId")
                        .HasColumnType("int");

                    b.HasKey("SensoresSensorId", "TelefonosTelefonoId");

                    b.HasIndex("TelefonosTelefonoId");

                    b.ToTable("SensorTelefono");
                });

            modelBuilder.Entity("Celulares.Models.Instalacion", b =>
                {
                    b.HasOne("Celulares.Models.App", "App")
                        .WithMany("Instalaciones")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Celulares.Models.Operario", "Operario")
                        .WithMany("Instalaciones")
                        .HasForeignKey("OperarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Celulares.Models.Telefono", "Telefono")
                        .WithMany("Instalaciones")
                        .HasForeignKey("TelefonoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("Operario");

                    b.Navigation("Telefono");
                });

            modelBuilder.Entity("SensorTelefono", b =>
                {
                    b.HasOne("Celulares.Models.Sensor", null)
                        .WithMany()
                        .HasForeignKey("SensoresSensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Celulares.Models.Telefono", null)
                        .WithMany()
                        .HasForeignKey("TelefonosTelefonoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Celulares.Models.App", b =>
                {
                    b.Navigation("Instalaciones");
                });

            modelBuilder.Entity("Celulares.Models.Operario", b =>
                {
                    b.Navigation("Instalaciones");
                });

            modelBuilder.Entity("Celulares.Models.Telefono", b =>
                {
                    b.Navigation("Instalaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
