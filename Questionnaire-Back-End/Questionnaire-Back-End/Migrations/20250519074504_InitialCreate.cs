using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Questionnaire_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    participant_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    participant_email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.id);
                    table.UniqueConstraint("AK_Participants_public_id", x => x.public_id);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    questionnaire_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.id);
                    table.UniqueConstraint("AK_Questionnaires_public_id", x => x.public_id);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    target_group_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantAnswers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    participant_id = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    questionnaire_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    option_text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantAnswers", x => x.id);
                    table.ForeignKey(
                        name: "FK_ParticipantAnswers_Participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "Participants",
                        principalColumn: "public_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantAnswers_Questionnaires_questionnaire_id",
                        column: x => x.questionnaire_id,
                        principalTable: "Questionnaires",
                        principalColumn: "public_id");
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionnaire_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Publications_Questionnaires_questionnaire_id",
                        column: x => x.questionnaire_id,
                        principalTable: "Questionnaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    public_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    question_text = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    question_number = table.Column<int>(type: "int", nullable: false),
                    required = table.Column<bool>(type: "bit", nullable: false),
                    questionnaire_id = table.Column<int>(type: "int", nullable: false),
                    deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_questionnaire_id",
                        column: x => x.questionnaire_id,
                        principalTable: "Questionnaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantGroup",
                columns: table => new
                {
                    participant_id = table.Column<int>(type: "int", nullable: false),
                    target_group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantGroup", x => new { x.participant_id, x.target_group_id });
                    table.ForeignKey(
                        name: "FK_ParticipantGroup_Participants_participant_id",
                        column: x => x.participant_id,
                        principalTable: "Participants",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ParticipantGroup_TargetGroups_target_group_id",
                        column: x => x.target_group_id,
                        principalTable: "TargetGroups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TargetGroupQuestionnaire",
                columns: table => new
                {
                    target_group_id = table.Column<int>(type: "int", nullable: false),
                    questionnaire_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroupQuestionnaire", x => new { x.target_group_id, x.questionnaire_id });
                    table.ForeignKey(
                        name: "FK_TargetGroupQuestionnaire_Questionnaires_questionnaire_id",
                        column: x => x.questionnaire_id,
                        principalTable: "Questionnaires",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TargetGroupQuestionnaire_TargetGroups_target_group_id",
                        column: x => x.target_group_id,
                        principalTable: "TargetGroups",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    option_text = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    deleted_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "id", "participant_email", "participant_name", "public_id" },
                values: new object[,]
                {
                    { 1, "Klaas@test.nl", "Klaas Jan", "4d64155b-9e64-4253-8dbe-47e75ed4a63a" },
                    { 2, "Henk@test.nl", "Henk de Vries", "4d64155b-9e64-4253-8dbe-47e75edasd3a" },
                    { 3, "sophie.jansen@test.nl", "Sophie Hansen", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678" },
                    { 4, "jan.willem@test.nl", "Jan Willem", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12" },
                    { 5, "lotte.bakker@test.nl", "Lotte Bakker", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678" },
                    { 6, "pieter.vandijk@test.nl", "Pieter van Dijk", "d3b941af-7654-4da3-b2f3-9876abcde321" },
                    { 7, "emma.deboer@test.nl", "Emma de Boer", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901" },
                    { 8, "bram.visser@test.nl", "Bram Visser", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12" },
                    { 9, "noa.meijer@test.nl", "Noa Meijer", "a2b3c4d5-e6f7-4890-8123-abcdef123456" },
                    { 10, "daan.kuipers@test.nl", "Daan Kuipers", "f3e2d1c0-b4a9-4e87-9345-fedcba654321" }
                });

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "id", "deleted_on", "description", "public_id", "questionnaire_name" },
                values: new object[,]
                {
                    { 1, null, "Een vragenlijst voor klanten om hun tevredenheid te meten over onze service.", "afe19d4a-82b0-46b5-8431-0409428dbdf8", "Klanttevredenheidsonderzoek" },
                    { 2, null, "Een vragenlijst om feedback te verzamelen over ons nieuwste product.", "afe19d4a-82b0-34b5-8431-0409428dbdf8", "Productfeedback" },
                    { 3, null, "Een interne survey om het welzijn van medewerkers te meten.", "d1aa8f7a-1bd2-4b65-aad9-882c31456789", "Personeelsbeleving" },
                    { 4, null, "Wat vond je van het laatste bedrijfsevenement? Laat het ons weten!", "f45a02a3-9131-4d3b-9831-c3247f8ddc10", "Evenement Evaluatie" }
                });

            migrationBuilder.InsertData(
                table: "TargetGroups",
                columns: new[] { "id", "target_group_name" },
                values: new object[,]
                {
                    { 1, "Groep 1" },
                    { 2, "Groep 2" },
                    { 3, "Groep 3" },
                    { 4, "Groep 4" }
                });

            migrationBuilder.InsertData(
                table: "ParticipantAnswers",
                columns: new[] { "id", "option_text", "participant_id", "question_text", "questionnaire_id" },
                values: new object[,]
                {
                    { 1, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 2, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 3, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 4, "De reactietijd op e-mails en telefoontjes mag sneller.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 5, "Goede communicatie, ik werd netjes en op tijd op de hoogte gehouden.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 6, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 7, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 8, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 9, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 10, "Ik zou graag een stillere werking willen, vooral bij intensief gebruik", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 11, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 12, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 13, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 14, "Een rustigere plek om ongestoord te kunnen werken.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 15, "Het leukste vind ik de samenwerking met collega’s.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 16, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 17, "Nee", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 18, "Ja", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 19, "Het was goed georganiseerd en de sfeer was prettig.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 20, "Het evenement had meer interactie en praktische workshops mogen bevatten.", "4d64155b-9e64-4253-8dbe-47e75ed4a63a", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 21, "Ja", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 22, "Ja", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 23, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 24, "De wachttijd aan de telefoon mag korter", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 25, "Te gehaast en weinig uitleg, waardoor ik met vragen bleef zitten.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 26, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 27, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 28, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 29, "De vormgeving is mooi en modern, maar het product voelt wat fragiel aan.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 30, "Een langere garantietermijn zou meer vertrouwen geven.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 31, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 32, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 33, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 34, "Meer natuurlijk licht op kantoor zou fijn zijn.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 35, "Het leukste is het oplossen van complexe problemen.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 36, "Ja", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 37, "Nee", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 38, "Ja", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 39, "Leuke locatie en interessante sprekers, ik heb ervan genoten.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 40, "Het evenement had meer interactie en praktische workshops mogen bevatten.", "4d64155b-9e64-4253-8dbe-47e75edasd3a", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 41, "Nee", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 42, "Nee", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 43, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 44, "De wachttijd aan de telefoon mag korter", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 45, "Te gehaast en weinig uitleg, waardoor ik met vragen bleef zitten.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 46, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 47, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 48, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 49, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 50, "De bedieningsknoppen zouden iets intuïtiever mogen zijn.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 51, "Nee", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 52, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 53, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 54, "Een flexibeler werkrooster zou het werk makkelijker maken.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 55, "Het moeilijkste is het balanceren van meerdere projecten tegelijk.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 56, "Nee", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 57, "Ja", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 58, "Nee", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 59, "Het was goed georganiseerd en de sfeer was prettig.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 60, "De inschrijfprocedure was wat onduidelijk en rommelig.", "a1c8f1b7-2d34-4a3e-bc5e-2a1b12345678", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 61, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 62, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 63, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 64, "De wachttijd aan de telefoon mag korter", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 65, "Zeer vriendelijk en professioneel, ik voelde me serieus genomen.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 66, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 67, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 68, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 69, "De installatie ging vlot, maar het werkte niet zoals ik had verwacht.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 70, "Een handleiding in meerdere talen zou handig zijn.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 71, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 72, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 73, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 74, "Meer natuurlijk licht op kantoor zou fijn zijn.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 75, "Het leukste vind ik de samenwerking met collega’s.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 76, "Ja", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 77, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 78, "Nee", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 79, "Het was goed georganiseerd en de sfeer was prettig.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 80, "De communicatie vooraf had duidelijker kunnen zijn.", "b9d1234a-4cde-4d2f-bb44-0f5678abcd12", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 81, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 82, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 83, "Nee", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 84, "De wachttijd aan de telefoon mag korter", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 85, "Soms wat onduidelijk, vooral bij technische vragen", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 86, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 87, "Nee", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 88, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 89, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 90, "Ik zou graag een stillere werking willen, vooral bij intensief gebruik", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 91, "Nee", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 92, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 93, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 94, "Een rustigere plek om ongestoord te kunnen werken.", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 95, "Het moeilijkste is het omgaan met strakke deadlines", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 96, "Nee", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 97, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 98, "Ja", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 99, "Het was goed georganiseerd en de sfeer was prettig.", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 100, "De communicatie vooraf had duidelijker kunnen zijn.", "c7f81da2-1a2b-4fae-a9f3-1234abcd5678", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 101, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 102, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 103, "Nee", "d3b941af-7654-4da3-b2f3-9876abcde321", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 104, "De wachttijd aan de telefoon mag korter", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 105, "Goede communicatie, ik werd netjes en op tijd op de hoogte gehouden.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 106, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 107, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 108, "Nee", "d3b941af-7654-4da3-b2f3-9876abcde321", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 109, "De vormgeving is mooi en modern, maar het product voelt wat fragiel aan.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 110, "Ik zou graag een stillere werking willen, vooral bij intensief gebruik", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 111, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 112, "Nee", "d3b941af-7654-4da3-b2f3-9876abcde321", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 113, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 114, "Een rustigere plek om ongestoord te kunnen werken.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 115, "Het leukste is het oplossen van complexe problemen.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 116, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 117, "Ja", "d3b941af-7654-4da3-b2f3-9876abcde321", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 118, "Nee", "d3b941af-7654-4da3-b2f3-9876abcde321", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 119, "Het evenement was goed georganiseerd, maar sommige sessies waren te kort.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 120, "De communicatie vooraf had duidelijker kunnen zijn.", "d3b941af-7654-4da3-b2f3-9876abcde321", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 121, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 122, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 123, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 124, "De wachttijd aan de telefoon mag korter", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 125, "Zeer vriendelijk en professioneel, ik voelde me serieus genomen.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 126, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 127, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 128, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 129, "De installatie ging vlot, maar het werkte niet zoals ik had verwacht.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 130, "Een langere garantietermijn zou meer vertrouwen geven.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 131, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 132, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 133, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 134, "Een flexibeler werkrooster zou het werk makkelijker maken.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 135, "Het moeilijkste is het omgaan met strakke deadlines", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 136, "Ja", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 137, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 138, "Nee", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 139, "Leuke locatie en interessante sprekers, ik heb ervan genoten.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 140, "De inschrijfprocedure was wat onduidelijk en rommelig.", "e1f23dcb-8a2c-4b0d-b9fe-4567cdef8901", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 141, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 142, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 143, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 144, "De reactietijd op e-mails en telefoontjes mag sneller.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 145, "Zeer vriendelijk en professioneel, ik voelde me serieus genomen.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 146, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 147, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 148, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 149, "De vormgeving is mooi en modern, maar het product voelt wat fragiel aan.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 150, "De bedieningsknoppen zouden iets intuïtiever mogen zijn.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 151, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 152, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 153, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 154, "Een flexibeler werkrooster zou het werk makkelijker maken.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 155, "Het moeilijkste is het omgaan met strakke deadlines", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 156, "Nee", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 157, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 158, "Ja", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 159, "Het was goed georganiseerd en de sfeer was prettig.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 160, "Het evenement had meer interactie en praktische workshops mogen bevatten.", "f2e13a77-0b1e-4a8e-a1c2-7890abcdef12", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 161, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 162, "Nee", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 163, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 164, "Meer kennis bij medewerkers over specifieke producten zou helpen.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 165, "Goede communicatie, ik werd netjes en op tijd op de hoogte gehouden.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 166, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 167, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 168, "Nee", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 169, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 170, "De bedieningsknoppen zouden iets intuïtiever mogen zijn.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 171, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 172, "Nee", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 173, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 174, "Een flexibeler werkrooster zou het werk makkelijker maken.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 175, "Het moeilijkste is het omgaan met strakke deadlines", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 176, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 177, "Nee", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 178, "Ja", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 179, "Het was goed georganiseerd en de sfeer was prettig.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 180, "De communicatie vooraf had duidelijker kunnen zijn.", "a2b3c4d5-e6f7-4890-8123-abcdef123456", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 181, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Ben je tevreden over onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 182, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Werd je snel geholpen?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 183, "Nee", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Was het personeel vriendelijk tegen je?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 184, "Meer kennis bij medewerkers over specifieke producten zou helpen.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat kunnen we verbeteren aan onze klantenservice?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 185, "Te gehaast en weinig uitleg, waardoor ik met vragen bleef zitten.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat vond je van de communicatie met onze medewerkers?", "afe19d4a-82b0-46b5-8431-0409428dbdf8" },
                    { 186, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Ben je tevreden over de kwaliteit van het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 187, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Was de productbeschrijving accuraat?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 188, "Nee", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Werd het product op tijd geleverd?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 189, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat vond je het beste of slechtste aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 190, "De bedieningsknoppen zouden iets intuïtiever mogen zijn.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat zou je veranderen aan het product?", "afe19d4a-82b0-34b5-8431-0409428dbdf8" },
                    { 191, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Ben je tevreden met je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 192, "Nee", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Voel je je gewaardeerd binnen het team?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 193, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Ben je tevreden met je werk-privébalans?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 194, "Een flexibeler werkrooster zou het werk makkelijker maken.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat zou je willen veranderen aan je werkomgeving?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 195, "Het moeilijkste is het balanceren van meerdere projecten tegelijk.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat vind je het leukste of moeilijkste aan je werk?", "d1aa8f7a-1bd2-4b65-aad9-882c31456789" },
                    { 196, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Was je tevreden over de locatie van het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 197, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Was de communicatie vooraf duidelijk?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 198, "Ja", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Was de organisatie van het evenement goed?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 199, "Het was goed georganiseerd en de sfeer was prettig.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat vond je van het evenement in het algemeen?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" },
                    { 200, "Het evenement had meer interactie en praktische workshops mogen bevatten.", "f3e2d1c0-b4a9-4e87-9345-fedcba654321", "Wat had er beter gekund aan het evenement?", "f45a02a3-9131-4d3b-9831-c3247f8ddc10" }
                });

            migrationBuilder.InsertData(
                table: "ParticipantGroup",
                columns: new[] { "participant_id", "target_group_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "id", "end_date", "questionnaire_id", "start_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 9, 23, 59, 0, 0, DateTimeKind.Local), 1, new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, new DateTime(2025, 6, 9, 23, 59, 0, 0, DateTimeKind.Local), 2, new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, new DateTime(2025, 6, 9, 23, 59, 0, 0, DateTimeKind.Local), 3, new DateTime(2025, 5, 19, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, new DateTime(2025, 5, 18, 23, 59, 0, 0, DateTimeKind.Local), 4, new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "id", "deleted_on", "public_id", "question_number", "question_text", "questionnaire_id", "required" },
                values: new object[,]
                {
                    { 1, null, "8286d046-9740-a3e4-95cf-ff46699c73c4", 1, "Ben je tevreden over onze klantenservice?", 1, true },
                    { 2, null, "95c69371-b924-6fe3-7c38-98b7dd200bc1", 2, "Werd je snel geholpen?", 1, true },
                    { 3, null, "a905569d-db07-3ae3-63a0-322750a4a3bd", 3, "Was het personeel vriendelijk tegen je?", 1, true },
                    { 4, null, "bc4519c8-fdeb-06e2-4a08-cc98c4273aba", 4, "Wat kunnen we verbeteren aan onze klantenservice?", 1, true },
                    { 5, null, "cf85ddf4-1ece-d1e2-3171-650938abd2b7", 5, "Wat vond je van de communicatie met onze medewerkers?", 1, true },
                    { 6, null, "e2c4a01f-40b1-9de1-18d9-ff7aab2e6ab3", 1, "Ben je tevreden over de kwaliteit van het product?", 2, true },
                    { 7, null, "f604634b-6295-68e1-ff41-99ea1fb201b0", 2, "Was de productbeschrijving accuraat?", 2, true },
                    { 8, null, "09442776-8478-34e0-e6aa-335b933599ad", 3, "Werd het product op tijd geleverd?", 2, true },
                    { 9, null, "1c84eaa2-a65c-ffdf-ce12-cccc06b931a9", 4, "Wat vond je het beste of slechtste aan het product?", 2, true },
                    { 10, null, "2fc3adcd-c83f-cbdf-b57a-663d7a3cc8a6", 5, "Wat zou je veranderen aan het product?", 2, true },
                    { 11, null, "420371f9-ea23-96de-9ce3-00aeeec060a2", 1, "Ben je tevreden met je werk?", 3, true },
                    { 12, null, "56433424-0c06-62de-834b-9a1e6143f89f", 2, "Voel je je gewaardeerd binnen het team?", 3, true },
                    { 13, null, "6982f750-2dea-2ddd-6ab4-338fd5c7909c", 3, "Ben je tevreden met je werk-privébalans?", 3, true },
                    { 14, null, "7cc2ba7c-4fcd-f9dd-511c-cd00494a2798", 4, "Wat zou je willen veranderen aan je werkomgeving?", 3, true },
                    { 15, null, "8f027ea7-71b0-c4dc-3884-6771bccebf95", 5, "Wat vind je het leukste of moeilijkste aan je werk?", 3, true },
                    { 16, null, "a24141d3-9394-90dc-1fed-01e130515792", 1, "Was je tevreden over de locatie van het evenement?", 4, true },
                    { 17, null, "b68104fe-b577-5bdb-0755-9a52a4d5ee8e", 2, "Was de communicatie vooraf duidelijk?", 4, true },
                    { 18, null, "c9c1c82a-d75b-27da-eebd-34c31858868b", 3, "Was de organisatie van het evenement goed?", 4, true },
                    { 19, null, "dc018b55-f93e-f2da-d526-ce348bdc1e87", 4, "Wat vond je van het evenement in het algemeen?", 4, true },
                    { 20, null, "ef404e81-1b22-bed9-bc8e-68a5ff5fb584", 5, "Wat had er beter gekund aan het evenement?", 4, true }
                });

            migrationBuilder.InsertData(
                table: "TargetGroupQuestionnaire",
                columns: new[] { "questionnaire_id", "target_group_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "id", "deleted_on", "option_text", "question_id" },
                values: new object[,]
                {
                    { 1, null, "Ja", 1 },
                    { 2, null, "Nee", 1 },
                    { 3, null, "Ja", 2 },
                    { 4, null, "Nee", 2 },
                    { 5, null, "Ja", 3 },
                    { 6, null, "Nee", 3 },
                    { 7, null, "De wachttijd aan de telefoon mag korter", 4 },
                    { 8, null, "Meer kennis bij medewerkers over specifieke producten zou helpen.", 4 },
                    { 9, null, "De reactietijd op e-mails en telefoontjes mag sneller.", 4 },
                    { 10, null, "Zeer vriendelijk en professioneel, ik voelde me serieus genomen.", 5 },
                    { 11, null, "Soms wat onduidelijk, vooral bij technische vragen", 5 },
                    { 12, null, "Goede communicatie, ik werd netjes en op tijd op de hoogte gehouden.", 5 },
                    { 13, null, "Te gehaast en weinig uitleg, waardoor ik met vragen bleef zitten.", 5 },
                    { 14, null, "Ja", 6 },
                    { 15, null, "Nee", 6 },
                    { 16, null, "Ja", 7 },
                    { 17, null, "Nee", 7 },
                    { 18, null, "Ja", 8 },
                    { 19, null, "Nee", 8 },
                    { 20, null, "Het beste was de gebruiksvriendelijkheid, maar de batterijduur viel tegen.", 9 },
                    { 21, null, "Ik vond de kwaliteit van het materiaal uitstekend, maar het product was moeilijk te monteren.", 9 },
                    { 22, null, "De vormgeving is mooi en modern, maar het product voelt wat fragiel aan.", 9 },
                    { 23, null, "De installatie ging vlot, maar het werkte niet zoals ik had verwacht.", 9 },
                    { 24, null, "Een langere garantietermijn zou meer vertrouwen geven.", 10 },
                    { 25, null, "De bedieningsknoppen zouden iets intuïtiever mogen zijn.", 10 },
                    { 26, null, "Ik zou graag een stillere werking willen, vooral bij intensief gebruik", 10 },
                    { 27, null, "Een handleiding in meerdere talen zou handig zijn.", 10 },
                    { 28, null, "Ja", 11 },
                    { 29, null, "Nee", 11 },
                    { 30, null, "Ja", 12 },
                    { 31, null, "Nee", 12 },
                    { 32, null, "Ja", 13 },
                    { 33, null, "Nee", 13 },
                    { 34, null, "Meer natuurlijk licht op kantoor zou fijn zijn.", 14 },
                    { 35, null, "Een rustigere plek om ongestoord te kunnen werken.", 14 },
                    { 36, null, "Een flexibeler werkrooster zou het werk makkelijker maken.", 14 },
                    { 37, null, "Meer groen in de werkruimte zou de sfeer verbeteren.", 14 },
                    { 38, null, "Het leukste vind ik de samenwerking met collega’s.", 15 },
                    { 39, null, "Het moeilijkste is het omgaan met strakke deadlines", 15 },
                    { 40, null, "Het leukste is het oplossen van complexe problemen.", 15 },
                    { 41, null, "Het moeilijkste is het balanceren van meerdere projecten tegelijk.", 15 },
                    { 42, null, "Ja", 16 },
                    { 43, null, "Nee", 16 },
                    { 44, null, "Ja", 17 },
                    { 45, null, "Nee", 17 },
                    { 46, null, "Ja", 18 },
                    { 47, null, "Nee", 18 },
                    { 48, null, "Het was goed georganiseerd en de sfeer was prettig.", 19 },
                    { 49, null, "Leuke locatie en interessante sprekers, ik heb ervan genoten.", 19 },
                    { 50, null, "Het evenement was goed georganiseerd, maar sommige sessies waren te kort.", 19 },
                    { 51, null, "Ik vond de presentaties inspirerend, maar de locatie was lastig te bereiken.", 19 },
                    { 52, null, "De inschrijfprocedure was wat onduidelijk en rommelig.", 20 },
                    { 53, null, "Er hadden meer pauzes mogen zijn tussen de sessies", 20 },
                    { 54, null, "De communicatie vooraf had duidelijker kunnen zijn.", 20 },
                    { 55, null, "Het evenement had meer interactie en praktische workshops mogen bevatten.", 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_option_text",
                table: "AnswerOptions",
                column: "option_text");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_question_id",
                table: "AnswerOptions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantAnswers_participant_id",
                table: "ParticipantAnswers",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantAnswers_questionnaire_id",
                table: "ParticipantAnswers",
                column: "questionnaire_id");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantGroup_target_group_id",
                table: "ParticipantGroup",
                column: "target_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_questionnaire_id",
                table: "Publications",
                column: "questionnaire_id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_questionnaire_id",
                table: "Questions",
                column: "questionnaire_id");

            migrationBuilder.CreateIndex(
                name: "IX_TargetGroupQuestionnaire_questionnaire_id",
                table: "TargetGroupQuestionnaire",
                column: "questionnaire_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "ParticipantAnswers");

            migrationBuilder.DropTable(
                name: "ParticipantGroup");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "TargetGroupQuestionnaire");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "TargetGroups");

            migrationBuilder.DropTable(
                name: "Questionnaires");
        }
    }
}
