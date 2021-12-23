using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
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

            SqlCommand select_all_car_ids = new SqlCommand("SELECT DISTINCT car_types.id FROM car_types WHERE EXISTS (SELECT * FROM cars WHERE car_types.id = type_id AND NOT EXISTS (SELECT * FROM bookings WHERE cars.id = car_id AND days_to_drop >= " + Session["days_to_drop"] + " AND days_to_pick <= " + Session["days_to_pick"] + "))", db_connection);
            SqlDataAdapter adapter_all_car_ids = new SqlDataAdapter(select_all_car_ids);
            DataSet dataset_all_car_ids = new DataSet();

            adapter_all_car_ids.Fill(dataset_all_car_ids, "car_types");

            Repeater car_repeater = (Repeater)Master.FindControl("MainContent").FindControl("car_repeater");
            car_repeater.DataSource = dataset_all_car_ids.Tables["car_types"];
            car_repeater.DataBind();

            db_connection.Close();

            System.Diagnostics.Debug.WriteLine(Session["days_to_pick"]);
            System.Diagnostics.Debug.WriteLine(Session["days_to_drop"]);
        }

        protected void reserve_clicked(object sender, EventArgs e)
        {

        }
    }
}