﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProAgil.Repository;

namespace ProAgil.Repository.Migrations
{
    [DbContext(typeof(ProAgilDataContext))]
    partial class ProAgilDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("ProAgil.Domain.Evento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataEvento");

                    b.Property<string>("Email");

                    b.Property<int?>("EventoID");

                    b.Property<string>("ImagemURL");

                    b.Property<string>("Local");

                    b.Property<int>("QtdPessoas");

                    b.Property<string>("Telefone");

                    b.Property<string>("Tema");

                    b.HasKey("ID");

                    b.HasIndex("EventoID");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("ProAgil.Domain.Lote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime?>("DataInicio");

                    b.Property<int>("EventoID");

                    b.Property<string>("Nome");

                    b.Property<decimal>("Preco");

                    b.Property<int>("Quantidade");

                    b.HasKey("ID");

                    b.HasIndex("EventoID");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("ProAgil.Domain.Palestrante", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("ImagemURL");

                    b.Property<string>("Miniurriculo");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("ID");

                    b.ToTable("Palestrantes");
                });

            modelBuilder.Entity("ProAgil.Domain.PalestranteEvento", b =>
                {
                    b.Property<int>("PalestranteID");

                    b.Property<int>("EventoID");

                    b.HasKey("PalestranteID", "EventoID");

                    b.HasIndex("EventoID");

                    b.ToTable("PalestranteEventos");
                });

            modelBuilder.Entity("ProAgil.Domain.RedeSocial", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EventoID");

                    b.Property<string>("Nome");

                    b.Property<int?>("PalestranteID");

                    b.Property<string>("URL");

                    b.HasKey("ID");

                    b.HasIndex("EventoID");

                    b.HasIndex("PalestranteID");

                    b.ToTable("RedesSociais");
                });

            modelBuilder.Entity("ProAgil.Domain.Evento", b =>
                {
                    b.HasOne("ProAgil.Domain.Evento")
                        .WithMany("Eventos")
                        .HasForeignKey("EventoID");
                });

            modelBuilder.Entity("ProAgil.Domain.Lote", b =>
                {
                    b.HasOne("ProAgil.Domain.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("EventoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProAgil.Domain.PalestranteEvento", b =>
                {
                    b.HasOne("ProAgil.Domain.Evento", "Evento")
                        .WithMany("PalestrantesEventos")
                        .HasForeignKey("EventoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProAgil.Domain.Palestrante", "Palestrante")
                        .WithMany()
                        .HasForeignKey("PalestranteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProAgil.Domain.RedeSocial", b =>
                {
                    b.HasOne("ProAgil.Domain.Evento", "Evento")
                        .WithMany("RedeSociais")
                        .HasForeignKey("EventoID");

                    b.HasOne("ProAgil.Domain.Palestrante", "Palestrante")
                        .WithMany("RedeSociais")
                        .HasForeignKey("PalestranteID");
                });
#pragma warning restore 612, 618
        }
    }
}
