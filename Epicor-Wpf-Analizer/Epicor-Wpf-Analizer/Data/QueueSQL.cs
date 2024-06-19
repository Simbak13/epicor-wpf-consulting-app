

using Epicor_Wpf_Analizer.Helpers;

namespace Epicor_Wpf_Analizer.Data
{
    public  static class QueueSQL
    {
        private static string query = string.Empty;


        public static string QueueQuery(FiltersParams filters = null, int rowsNumber = 50)
        {

            query = @"
           SELECT TOP " + rowsNumber + @"
            Sc.SupportCallID,
            --ISNULL( AssigTo.DisplayName,'EN CAS') AS[AssignToUser],
            --Que.Name AS Queue,
            CASE
            WHEN AssigTo.DisplayName IS NOT NULL THEN AssigTo.DisplayName 
            ELSE
            Que.Name 
            END AS[AssignTo],
            Sc.Number,
            ISNULL(RIGHT(Impact.Entry, LEN(Impact.Entry) - 3),'NO DEFINIDO') AS[Impact],
            ISNULL(RIGHT(Urgency.Entry, LEN(Urgency.Entry) - 3),'NO DEFINIDO') AS[Urgency],
            Status.Name AS [Status],
            CASE 
            WHEN Sc.SupportCallType ='I' THEN 'Incident'
            WHEN Sc.SupportCallType ='R' THEN 'Request Fo Change'
            WHEN Sc.SupportCallType ='S' THEN 'Service Request'
            ELSE
            'NO DEFINIDO'
            END AS [Types],
            OpenBy.DisplayName AS[OpenBy],
            DATEADD(HH,-6,Sc.OpenDate) AS OpenDate, --EN EPICOR SE MUESTRA 6 HORAS MENOS
            DATEDIFF(DAY,  Sc.OpenDate,GETDATE()) AS [Days],
            Sc.Summary,
            O.Name  AS Organization,
            G.FullName AS Groups
            FROM SupportCall  AS Sc WITH (NOLOCK)
            LEFT JOIN ApplicationUser AS AssigTo WITH (NOLOCK)  ON   AssigTo .ApplicationUserID =Sc.AssignToUserID
            LEFT JOIN Queue  AS Que WITH (NOLOCK)    ON Que.QueueID = Sc.AssignToQueueID
            LEFT JOIN ValueListEntry AS Impact  WITH (NOLOCK)    ON Impact.ValueListEntryID = Sc.ImpactID
            LEFT JOIN ValueListEntry AS Urgency WITH (NOLOCK)    ON Urgency.ValueListEntryID = Sc.UrgencyID
            LEFT JOIN SupportCallStatus AS Status WITH (NOLOCK)   ON Status.SupportCallStatusID= Sc.StatusID
            LEFT JOIN ApplicationUser AS OpenBy WITH (NOLOCK)    ON OpenBy.ApplicationUserId = Sc.OpenByUserID
            LEFT JOIN Party AS P WITH (NOLOCK)    ON P.PartyID = Sc.PartyID
            LEFT JOIN Organization AS O WITH (NOLOCK)   ON O.OrganizationID = P.OrganizationID
            LEFT JOIN Groups AS  G WITH (NOLOCK)    ON G.GroupID = P.GroupID
            WHERE Sc.Closed=0  ORDER BY Sc.OpenDate DESC
            ";

            return query;
        }
    }
}
