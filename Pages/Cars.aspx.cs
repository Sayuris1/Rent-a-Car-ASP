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
        static DataSet dataset_all_car_ids = new DataSet();
        static DateTime pick_date_time = new DateTime();
        static DateTime drop_date_time = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                if (Convert.ToInt32(Session["days_to_pick"]) != -1 && Convert.ToInt32(Session["days_to_drop"]) != -1){
                    pick_date_time = DateTime.Today.AddDays(Convert.ToInt32(Session["days_to_pick"]));
                    drop_date_time = DateTime.Today.AddDays(Convert.ToInt32(Session["days_to_drop"]));
                }
                else {
                    pick_date_time = DateTime.MinValue;
                    drop_date_time = DateTime.MinValue;
                }

                calendar_pick.SelectedDate = pick_date_time;
                calendar_drop.SelectedDate = drop_date_time;
            }

            dataset_all_car_ids = new DataSet();

            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            SqlCommand select_all_car_ids = new SqlCommand("SELECT DISTINCT car_types.id FROM car_types WHERE EXISTS (SELECT * FROM cars WHERE car_types.id = type_id AND NOT EXISTS (SELECT * FROM bookings WHERE cars.id = car_id AND days_to_drop >= " + Session["days_to_drop"] + " AND days_to_pick <= " + Session["days_to_pick"] + "))", db_connection);
            SqlDataAdapter adapter_all_car_ids = new SqlDataAdapter(select_all_car_ids);

            adapter_all_car_ids.Fill(dataset_all_car_ids, "car_types");

            Repeater car_repeater = (Repeater)Master.FindControl("MainContent").FindControl("car_repeater");
            car_repeater.DataSource = dataset_all_car_ids.Tables["car_types"];
            car_repeater.DataBind();

            select_all_car_ids.Dispose();
            db_connection.Close();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Session["days_to_pick"]);
            System.Diagnostics.Debug.WriteLine(Session["days_to_drop"]);
            if (Convert.ToInt32(Session["days_to_pick"]) == -1 || Convert.ToInt32(Session["days_to_drop"]) == -1){
                Repeater car_repeater = (Repeater)Master.FindControl("MainContent").FindControl("car_repeater");
                foreach (RepeaterItem item in car_repeater.Items){
                    Button reserve_button = (Button)item.FindControl("reserve_button");
                    reserve_button.Enabled = false;
                    reserve_button.Text = " Please Search Before Booking !!! ";
                }
            }
        }

        protected void reserve_clicked(object sender, CommandEventArgs e)
        {
            SqlConnection db_connection =
                    new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

                db_connection.Open();

            SqlCommand get_free_car_id = new SqlCommand("SELECT id, type_id FROM cars WHERE type_id = " + dataset_all_car_ids.Tables["car_types"].Rows[(Int32.Parse(e.CommandArgument.ToString()))][0]  + " AND NOT EXISTS (SELECT * FROM bookings WHERE cars.id = car_id AND days_to_drop >= " + Session["days_to_drop"] + " AND days_to_pick <= " + Session["days_to_pick"] + ")", db_connection);
            SqlDataAdapter adapter_free_car = new SqlDataAdapter(get_free_car_id);
            DataSet dataset_free_car = new DataSet();

            adapter_free_car.Fill(dataset_free_car, "car_ids");

            SqlCommand insert_new_booking = new SqlCommand("INSERT INTO bookings VALUES (" + dataset_free_car.Tables["car_ids"].Rows[0][0] + ", " + Session["days_to_pick"] + ", " + Session["days_to_drop"] + ")", db_connection);
            SqlDataAdapter adapter_booking = new SqlDataAdapter(insert_new_booking);

            adapter_booking.InsertCommand = insert_new_booking;
            adapter_booking.InsertCommand.ExecuteNonQuery();

            get_free_car_id.Dispose();
            db_connection.Close();

            Response.Redirect("Default.aspx");
        }

        protected void pick_render(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < (System.DateTime.Today)
                || (!drop_date_time.Equals(DateTime.MinValue) && e.Day.Date > drop_date_time))

            {

                e.Day.IsSelectable = false;

                e.Cell.BackColor = System.Drawing.Color.DarkGray;

            }
        }

        protected void drop_render(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < (System.DateTime.Today)
                || (!pick_date_time.Equals(DateTime.MinValue) && e.Day.Date < pick_date_time))

            {

                e.Day.IsSelectable = false;

                e.Cell.BackColor = System.Drawing.Color.DarkGray;

            }
        }

         protected void selection_changed(object sender, EventArgs e)
        {
            pick_date_time = calendar_pick.SelectedDate;
            drop_date_time = calendar_drop.SelectedDate;

            if (!pick_date_time.Equals(DateTime.MinValue) && !drop_date_time.Equals(DateTime.MinValue)){
                Session["days_to_pick"] = (pick_date_time - DateTime.Today).Days;
                Session["days_to_drop"] = (drop_date_time - DateTime.Today).Days;

                Response.Redirect("Cars.aspx");
            }
        }

        protected void item_command (object sender, RepeaterCommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("aaa");
        }
    }
}