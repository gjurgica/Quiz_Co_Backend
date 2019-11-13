using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quiz_co.Domain
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public int? QuizId { get; set; }

        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
        public virtual IEnumerable<Answer> Answers { get; set; }
    }
}
