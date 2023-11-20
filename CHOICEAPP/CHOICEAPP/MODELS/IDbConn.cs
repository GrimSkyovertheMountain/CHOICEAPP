using System.Data.SqlClient;

namespace CHOICEAPP.MODELS
{
    public interface IDbConn
    {
        SqlConnection Connection { get; }
        string server_dbname { get; set; }
        string server_name { get; set; }
        string server_pass { get; set; }
        string server_user { get; set; }

        string GetConnectionString();
    }
}