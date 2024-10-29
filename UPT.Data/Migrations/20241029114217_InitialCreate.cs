using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UPT.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gyms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    open_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    close_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gyms", x => x.id);
                    table.ForeignKey(
                        name: "fk_gyms_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email_address = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: true),
                    is_notification_enable = table.Column<bool>(type: "boolean", nullable: false),
                    is_email_notification_enable = table.Column<bool>(type: "boolean", nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    edit_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_news", x => x.id);
                    table.ForeignKey(
                        name: "fk_news_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_checked = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    trainer_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    volume_breast = table.Column<double>(type: "double precision", nullable: false),
                    volume_waist = table.Column<double>(type: "double precision", nullable: false),
                    volume_abdomen = table.Column<double>(type: "double precision", nullable: false),
                    volume_buttock = table.Column<double>(type: "double precision", nullable: false),
                    volume_hip = table.Column<double>(type: "double precision", nullable: false),
                    trainer_id = table.Column<int>(type: "integer", nullable: false),
                    training_program = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                    table.ForeignKey(
                        name: "fk_clients_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favorits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favorits", x => x.id);
                    table.ForeignKey(
                        name: "fk_favorits_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    medic_grade = table.Column<bool>(type: "boolean", nullable: false),
                    work_injuries = table.Column<bool>(type: "boolean", nullable: false),
                    work_sportsmens = table.Column<bool>(type: "boolean", nullable: false),
                    training_programs = table.Column<int[]>(type: "integer[]", nullable: false),
                    gym_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    favorite_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trainers", x => x.id);
                    table.ForeignKey(
                        name: "fk_trainers_favorits_favorite_id",
                        column: x => x.favorite_id,
                        principalTable: "favorits",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_trainers_gyms_gym_id",
                        column: x => x.gym_id,
                        principalTable: "gyms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trainers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    trainer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_feedbacks", x => x.id);
                    table.ForeignKey(
                        name: "fk_feedbacks_clients_creator_id",
                        column: x => x.creator_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_feedbacks_trainers_trainer_id",
                        column: x => x.trainer_id,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chats_client_id",
                table: "chats",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_chats_trainer_id",
                table: "chats",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_trainer_id",
                table: "clients",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_user_id",
                table: "clients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_favorits_client_id",
                table: "favorits",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedbacks_creator_id",
                table: "feedbacks",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_feedbacks_trainer_id",
                table: "feedbacks",
                column: "trainer_id");

            migrationBuilder.CreateIndex(
                name: "ix_gyms_city_id",
                table: "gyms",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_news_user_id",
                table: "news",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_user_id",
                table: "payments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_trainers_favorite_id",
                table: "trainers",
                column: "favorite_id");

            migrationBuilder.CreateIndex(
                name: "ix_trainers_gym_id",
                table: "trainers",
                column: "gym_id");

            migrationBuilder.CreateIndex(
                name: "ix_trainers_user_id",
                table: "trainers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_city_id",
                table: "users",
                column: "city_id");

            migrationBuilder.AddForeignKey(
                name: "fk_chats_clients_client_id",
                table: "chats",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_chats_trainers_trainer_id",
                table: "chats",
                column: "trainer_id",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_clients_trainers_trainer_id",
                table: "clients",
                column: "trainer_id",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_favorits_clients_client_id",
                table: "favorits");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "trainers");

            migrationBuilder.DropTable(
                name: "favorits");

            migrationBuilder.DropTable(
                name: "gyms");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
