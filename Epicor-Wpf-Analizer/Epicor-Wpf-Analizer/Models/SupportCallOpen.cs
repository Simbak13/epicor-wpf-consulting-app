

using System;

namespace Epicor_Wpf_Analizer.Models
{
    public class SupportCallOpen
    {
        public string SupportCallID { get; set; }
        public string AssignTo { get; set; }
        public int Number { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Status { get; set; }
        public string Types { get; set; }
        public string OpenBy { get; set; }
        public DateTime OpenDate { get; set; }
        public int Days { get; set; }
        public string Summary { get; set; }
        public string Organization { get; set; }
        public string Groups { get; set; }

        public SupportCallOpen()
        {

        }
        public class SupportCallOpenBuilder
        {
            private SupportCallOpen supportCall;

            public SupportCallOpenBuilder()
            {
                supportCall = new SupportCallOpen();
            }

            public SupportCallOpenBuilder WithSupportCallID(string supportCallID)
            {
                supportCall.SupportCallID = supportCallID;
                return this;
            }

            public SupportCallOpenBuilder WithAssignTo(string assignTo)
            {
                supportCall.AssignTo = assignTo;
                return this;
            }

            public SupportCallOpenBuilder WithNumber(int number)
            {
                supportCall.Number = number;
                return this;
            }

            public SupportCallOpenBuilder WithImpact(string impact)
            {
                supportCall.Impact = impact;
                return this;
            }

            public SupportCallOpenBuilder WithUrgency(string urgency)
            {
                supportCall.Urgency = urgency;
                return this;
            }

            public SupportCallOpenBuilder WithStatus(string status)
            {
                supportCall.Status = status;
                return this;
            }

            public SupportCallOpenBuilder WithTypes(string types)
            {
                supportCall.Types = types;
                return this;
            }

            public SupportCallOpenBuilder WithOpenBy(string openBy)
            {
                supportCall.OpenBy = openBy;
                return this;
            }

            public SupportCallOpenBuilder WithOpenDate(DateTime openDate)
            {
                supportCall.OpenDate = openDate;
                return this;
            }

            public SupportCallOpenBuilder WithDays(int days)
            {
                supportCall.Days = days;
                return this;
            }

            public SupportCallOpenBuilder WithSummary(string summary)
            {
                supportCall.Summary = summary;
                return this;
            }

            public SupportCallOpenBuilder WithOrganization(string organization)
            {
                supportCall.Organization = organization;
                return this;
            }

            public SupportCallOpenBuilder WithGroups(string groups)
            {
                supportCall.Groups = groups;
                return this;
            }

            public SupportCallOpen Build()
            {
                return supportCall;
            }
        }
    }
}
