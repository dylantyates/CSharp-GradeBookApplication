﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        private string ExceptionMessage = "Ranked grading requires at least 5 students";

        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count() < 5)
            {
                throw new InvalidOperationException(ExceptionMessage);
            }

            // Get first 20% group from all students
            int threshold = (int)Math.Ceiling(Students.Count * 0.2);

            // Order grades for ranked grading from high to low
            var grades = Students.OrderByDescending(s => s.AverageGrade)
                                 .Select(s => s.AverageGrade)
                                 .ToList();

            return StudentsRankedByGrade(averageGrade, threshold, grades);
        }

        public override void CalculateStatistics()
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine(ExceptionMessage);
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine(ExceptionMessage);
                return;
            }

            base.CalculateStudentStatistics(name);
        }

        private char StudentsRankedByGrade(double averageGrade, int threshold, List<double> grades)
        {
            // Highest 20% of students get A, next 20% get B, so on...
            if (averageGrade >= grades[threshold - 1])
            {
                return 'A';
            }
            if (averageGrade >= grades[(threshold * 2) - 1])
            {
                return 'B';
            }
            if (averageGrade >= grades[(threshold * 3) - 1])
            {
                return 'C';
            }
            if (averageGrade >= grades[(threshold * 4) - 1])
            {
                return 'D';
            }

            return 'F';
        }
    }
}