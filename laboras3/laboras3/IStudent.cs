using System.Collections.Generic;

namespace laboras3
{
    interface IStudent
    {
        double ExamRes { get; set; }
        double FinalPtsAvg { get; set; }
        double FinalPtsMed { get; set; }
        List<double> HomeWork { get; set; }
        string Name { get; set; }
        string SurName { get; set; }
    }
}