using MySql.Data.MySqlClient;
using System.Configuration;

namespace Lc_Cell_Sistema_de_Controle.br.com.projet.connection
{
    public class ConnectionFactory
    {
        public MySqlConnection GetConnection()
        {
            string conecxao = ConfigurationManager.ConnectionStrings["lccelldb"].ConnectionString;

            return new MySqlConnection(conecxao);
        }
    }
}
