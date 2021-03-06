﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Quiz : Enity
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public Guid CourseId { set; get; }
        public Course Course { set; get; }
    }

    public class Question : Enity
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public bool Required { set; get; }
        public Guid QuizId { set; get; }

        public Quiz Quiz { set; get; }
        public string Answers { set; get; }
    }
    public class Answer
    {
        public Guid Id { set; get; }
        public string Labsel { set; get; }
        public string Value { set; get; }
        public bool Choice { set; get; }
    }
}
