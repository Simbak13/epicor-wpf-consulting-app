using Epicor_Wpf_Analizer.Helpers;
using Epicor_Wpf_Analizer.Interfaces;
using Epicor_Wpf_Analizer.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Epicor_Wpf_Analizer.Data;

namespace Epicor_Wpf_Analizer.Services
{
    public class QueueServices : IQueueServices
    {

        private string connstring = ConfigurationManager.ConnectionStrings["EpicoConnectionString"].ConnectionString;
        private string query = string.Empty;
        private SqlConnection con;

        public async  Task<List<SupportCallOpen>> ListOpenQueuesAsync(FiltersParams filters = null, int rowsNumber = 50)
        {
            List<SupportCallOpen> list = new List<SupportCallOpen>();
            try
            {
                query = QueueSQL.QueueQuery(null, rowsNumber);
                using (con = new SqlConnection(connstring)) 
                {
                    await con.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new SupportCallOpen.SupportCallOpenBuilder()
                                          .WithSupportCallID(reader["SupportCallID"].ToString())
                                          .WithAssignTo(reader["AssignTo"].ToString())
                                          .WithNumber(Convert.ToInt32(reader["Number"]))
                                          .WithImpact(reader["Impact"].ToString())
                                          .WithUrgency(reader["Urgency"].ToString())
                                          .WithStatus(reader["Status"].ToString())
                                          .WithTypes(reader["Types"].ToString())
                                          .WithOpenBy(reader["OpenBy"].ToString())
                                          .WithOpenDate(Convert.ToDateTime(reader["OpenDate"]))
                                          .WithDays(Convert.ToInt32(reader["Days"]))
                                          .WithSummary(reader["Summary"].ToString())
                                          .WithOrganization(reader["Organization"].ToString())
                                          .WithGroups(reader["Groups"].ToString())
                                          .Build());
                            }
                        }
                    }

                }

                return list;
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine($"SqlExceptio {sqlEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
