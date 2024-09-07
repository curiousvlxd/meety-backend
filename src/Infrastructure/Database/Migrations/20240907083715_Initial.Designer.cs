﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240907083715_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Invitation.Invitation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("InviteeId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("invitee_id");

                    b.Property<string>("InviterId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("inviter_id");

                    b.Property<string>("MeetingId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("meeting_id");

                    b.Property<short>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_invitations");

                    b.HasIndex("InviteeId")
                        .HasDatabaseName("ix_invitations_invitee_id");

                    b.HasIndex("InviterId")
                        .HasDatabaseName("ix_invitations_inviter_id");

                    b.HasIndex("MeetingId")
                        .HasDatabaseName("ix_invitations_meeting_id");

                    b.ToTable("invitations", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Meeting.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("creator_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.Property<DateTime>("Scheduled")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("scheduled");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_meetings");

                    b.HasIndex("CreatorId")
                        .HasDatabaseName("ix_meetings_creator_id");

                    b.ToTable("meetings", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ChatId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("chat_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<short>("MessengerType")
                        .HasColumnType("smallint")
                        .HasColumnName("messenger_type");

                    b.Property<string>("MessengerUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("messenger_user_id");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Invitation.Invitation", b =>
                {
                    b.HasOne("Domain.Entities.User.User", "Invitee")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("InviteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invitations_users_invitee_id");

                    b.HasOne("Domain.Entities.User.User", "Inviter")
                        .WithMany("SentInvitations")
                        .HasForeignKey("InviterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invitations_users_inviter_id");

                    b.HasOne("Domain.Entities.Meeting.Meeting", "Meeting")
                        .WithMany("Invitations")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_invitations_meetings_meeting_id");

                    b.Navigation("Invitee");

                    b.Navigation("Inviter");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("Domain.Entities.Meeting.Meeting", b =>
                {
                    b.HasOne("Domain.Entities.User.User", "Creator")
                        .WithMany("Meetings")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_meetings_users_creator_id");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Domain.Entities.Meeting.Meeting", b =>
                {
                    b.Navigation("Invitations");
                });

            modelBuilder.Entity("Domain.Entities.User.User", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("ReceivedInvitations");

                    b.Navigation("SentInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
