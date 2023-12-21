using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace es_Dnevnik
{
    internal class Konekcija
    {
        static public SqlConnection Connect()
        {
            string CS = @"Data Source = (localdb)\baza; Initial Catalog = esdnevnik; Integrated Security = True;";
            SqlConnection veza = new SqlConnection(CS);
            return veza;
        }
    }
}
