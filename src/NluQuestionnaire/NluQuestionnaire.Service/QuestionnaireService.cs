using NluQuestionnaire.Data;
using NluQuestionnaire.Util;
using System;
using System.Data.SqlClient;

namespace NluQuestionnaire.Service
{
    public class QuestionnaireService
    {
        public bool Create(Questionnaire item)
        {
            Configuration con = new Configuration();
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@userName",item.UserName),
                new SqlParameter("@chance1",item.Chance1),
                new SqlParameter("@chance2",item.Chance2),
                new SqlParameter("@chance3",item.Chance3),
                new SqlParameter("@question1",item.Question1),
                new SqlParameter("@question2",item.Question2),
                new SqlParameter("@question3",item.Question3),
            };
            con.ExecuteNonQueryPro("InsertQuestion", par);
            return true;
        }
    }
}
