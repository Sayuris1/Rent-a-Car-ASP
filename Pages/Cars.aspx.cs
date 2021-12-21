using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace asp
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();
        }
    }
}