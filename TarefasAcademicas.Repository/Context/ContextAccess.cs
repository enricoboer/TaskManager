using System.Data.OleDb;

namespace TarefasAcademicas.DataAccess.Context
{
    public abstract class ContextAccess
    {
        private static OleDbConnection conn;

        public static OleDbConnection connection = GetConnection();

        public static OleDbConnection GetConnection()
        {
            if (conn == null)
            {
                conn = new OleDbConnection(
                    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\enricoboer\source\repos\TarefasAcademicas\TarefasAcademicas.UI\App_Data\TarefasAcademicas.accdb");

                return conn;
            }
            else
                return conn;
            
        }
    }
}