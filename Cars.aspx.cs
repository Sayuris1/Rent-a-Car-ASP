using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.OleDb;

namespace asp
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OleDbConnection db_connection =
                new OleDbConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();
        }
    }
}