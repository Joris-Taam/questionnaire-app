namespace Questionnaire_Back_End.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Questionnaire_Back_End.Data.Factories;

    public class QuestionnaireDbContext : DbContext
    {
        public DbSet<Questionnaires> Questionnaires { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<AnswerOptions> AnswerOptions { get; set; }
        public DbSet<TargetGroups> TargetGroups { get; set; }
        public DbSet<ParticipantAnswers> ParticipantAnswers { get; set; }
        public DbSet<TargetGroupQuestionnaire> TargetGroupQuestionnaire { get; set; }
        public DbSet<Participants> Participants { get; set; }
        public DbSet<ParticipantGroup> ParticipantGroup { get; set; }
        public DbSet<Publications> Publications { get; set; }


        public QuestionnaireDbContext(DbContextOptions<QuestionnaireDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publications>()
                .HasKey(p => p.id);

            modelBuilder.Entity<Publications>()
                .HasOne(p => p.questionnaire)
                .WithMany(q => q.publication_dates)
                .HasForeignKey(p => p.questionnaire_id);

            modelBuilder.Entity<Questions>()
                .HasOne(q => q.questionnaire)
                .WithMany(qn => qn.questions)
                .HasForeignKey(q => q.questionnaire_id);

            modelBuilder.Entity<AnswerOptions>()
                .HasOne(a => a.question)
                .WithMany(q => q.answer_options)
                .HasForeignKey(a => a.question_id);

            modelBuilder.Entity<TargetGroupQuestionnaire>()
                .HasKey(tgq => new { tgq.target_group_id, tgq.questionnaire_id });

            modelBuilder.Entity<TargetGroupQuestionnaire>()
                .HasOne(tgq => tgq.target_group)
                .WithMany(tg => tg.target_group_questionnaire)
                .HasForeignKey(tgq => tgq.target_group_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TargetGroupQuestionnaire>()
                .HasOne(tgq => tgq.questionnaire)
                .WithMany(q => q.target_group_questionnaire)
                .HasForeignKey(tgq => tgq.questionnaire_id)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ParticipantGroup>()
                .HasKey(pg => new { pg.participant_id, pg.target_group_id });

            modelBuilder.Entity<ParticipantGroup>()
                .HasOne(pg => pg.participant)
                .WithMany(p => p.user_target_group)
                .HasForeignKey(pg => pg.participant_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParticipantGroup>()
                .HasOne(pg => pg.target_group)
                .WithMany(tg => tg.user_target_groups)
                .HasForeignKey(pg => pg.target_group_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParticipantAnswers>()
                .HasOne(pa => pa.participant)
                .WithMany(p => p.user_answers)
                .HasForeignKey(pa => pa.participant_id)
                .HasPrincipalKey(p => p.public_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParticipantAnswers>()
                .HasOne(pa => pa.questionnaire)
                .WithMany(q => q.participants_answers)
                .HasForeignKey(pa => pa.questionnaire_id)
                .HasPrincipalKey(q => q.public_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AnswerOptions>()
                .HasIndex(a => a.option_text);

            var questionnaires = QuestionnaireFactory.GetSeedData();
            modelBuilder.Entity<Questionnaires>().HasData(questionnaires);

            var questions = QuestionFactory.GetSeedQuestions(questionnaires);
            modelBuilder.Entity<Questions>().HasData(questions);

            var answerOptions = AnswerOptionFactory.GetSeedAnswerOptions(questions);
            modelBuilder.Entity<AnswerOptions>().HasData(answerOptions);

            var participants = ParticipantFactory.GetSeedData();
            modelBuilder.Entity<Participants>().HasData(participants);

            var publications = PublicationsFactory.GetSeedPublications(questionnaires);
            modelBuilder.Entity<Publications>().HasData(publications);

            var targetGroups = TargetGroupFactory.GetSeedTargetGroups();
            modelBuilder.Entity<TargetGroups>().HasData(targetGroups);

            var targetGroupQuestionnaires = TargetGroupQuestionnaireFactory.GetSeedData(questionnaires);
            modelBuilder.Entity<TargetGroupQuestionnaire>().HasData(targetGroupQuestionnaires);

            var participantGroups = ParticipantGroupFactory.GetSeedData();
            modelBuilder.Entity<ParticipantGroup>().HasData(participantGroups);

            var participantAnswers = ParticipantAnswersFactory.GetSeedData(questionnaires, questions, answerOptions, participants);
            modelBuilder.Entity<ParticipantAnswers>().HasData(participantAnswers);
        }
    }

    public class Publications
    {
        public int id { get; set; }
        public int questionnaire_id { get; set; }
        public Questionnaires questionnaire { get; set; }

        [Required]
        public DateTime start_date { get; set; }
        [Required]
        public DateTime end_date { get; set; }
    }

    public class Questionnaires
    {
        public int id { get; set; }
        public string public_id { get; set; }
        [Required]
        [MaxLength(50)]
        public string questionnaire_name { get; set; }

        [MaxLength(300)]
        public string description { get; set; }
        public ICollection<TargetGroupQuestionnaire> target_group_questionnaire { get; set; }
        public ICollection<Questions> questions { get; set; }
        public ICollection<Publications> publication_dates { get; set; }
        public ICollection<ParticipantAnswers> participants_answers { get; set; }

        public DateTime? deleted_on { get; set; }

        [Timestamp]
        public byte[] row_version { get; set; }

    }

    public class Questions
    {
        public int id { get; set; }
        public string public_id { get; set; }

        [Required]
        [MaxLength(150)]
        public string question_text { get; set; }

        public int question_number { get; set; }

        public bool required { get; set; }

        [Required]
        public int questionnaire_id { get; set; }
        public Questionnaires questionnaire { get; set; }
        [Required]
        public ICollection<AnswerOptions> answer_options { get; set; }
        //public ICollection<ParticipantAnswers> ParticipantsAnswers { get; set; }
        public DateTime? deleted_on { get; set; }

        [Timestamp]
        public byte[] row_version { get; set; }

    }

    public class AnswerOptions
    {
        public int id { get; set; }
        [Required]
        [MaxLength(150)]
        public string option_text { get; set; }
        [Required]
        public int question_id { get; set; }
        public Questions question { get; set; }
        //public ICollection<ParticipantAnswers> ParticipantsAnswers { get; set; }
        public DateTime? deleted_on { get; set; }

        [Timestamp]
        public byte[] row_version { get; set; }

    }

    public class TargetGroups
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string target_group_name { get; set; }
        public ICollection<ParticipantGroup> user_target_groups { get; set; }
        public ICollection<TargetGroupQuestionnaire> target_group_questionnaire { get; set; }
    }

    public class TargetGroupQuestionnaire
    {
        [Required]
        public int target_group_id { get; set; }
        public TargetGroups target_group { get; set; }

        [Required]
        public int questionnaire_id { get; set; }
        public Questionnaires questionnaire { get; set; }

    }


    public class ParticipantGroup
    {
        [Required]
        public int participant_id { get; set; }
        public Participants participant { get; set; }
        public int target_group_id { get; set; }
        public TargetGroups target_group { get; set; }
        //public ICollection<Participants> Participants { get; set; }
    }

    public class Participants
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string public_id { get; set; }
        [Required]
        [MaxLength(50)]
        public string participant_name { get; set; }
        [Required]
        [MaxLength(50)]
        public string participant_email { get; set; }
        public ICollection<ParticipantAnswers> user_answers { get; set; }
        public ICollection<ParticipantGroup> user_target_group { get; set; }
        [Timestamp]

        public byte[] row_version { get; set; }
    }

    public class ParticipantAnswers
    {
        public int id { get; set; }
        public string participant_id { get; set; }
        public Participants participant { get; set; }
        public string questionnaire_id { get; set; }
        public Questionnaires questionnaire { get; set; }
        public string question_text { get; set; }
        public string option_text { get; set; }
    }
}
