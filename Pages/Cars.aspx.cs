using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace asp.Pages {
    public partial class Cars : database_helper{
        static DataSet dataset_store = new DataSet();
        static DateTime pick_date_time = new DateTime();
        static DateTime drop_date_time = new DateTime();

        void make_unselectable(DayRenderEventArgs e){
            // Makes dates on calendar uselectable
            e.Day.IsSelectable = false;
            e.Cell.BackColor = System.Drawing.Color.DarkGray;
        }

        void bind_repeater(){
            // Bind to repeater
            database_helper db_helper = new database_helper();
            // New dates both must be before booked pick date or after booked drop date
            db_helper.bind_new_select_command($"SELECT * FROM car_types WHERE EXISTS ( SELECT * FROM cars WHERE car_types.id = cars.type_id AND NOT EXISTS ( SELECT * FROM bookings WHERE cars.id = bookings.car_id AND NOT ( ( bookings.days_to_pick >= {Session["days_to_drop"]} AND days_to_pick >= {Session["days_to_pick"]} ) OR ( bookings.days_to_drop <= {Session["days_to_drop"]} AND days_to_drop <= {Session["days_to_pick"]} ) ) ) )");
            db_helper.fill_new_dataset("car_types");

            // Store data table
            if (dataset_store.Tables["car_types"] != null)
                dataset_store.Tables.Remove("car_types");
            dataset_store.Tables.Add(db_helper.get_dataset().Tables["car_types"].Copy());

            db_helper.repeater_bind("car_repeater", "car_types", Master.FindControl("MainContent"));
            db_helper.dispose_all_and_close();
        }

        // ***************************************
        // ********** Asp.net functions **********
        // ***************************************

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                // If dates selected, assign
                if (Convert.ToInt32(Session["days_to_pick"]) != -1 && Convert.ToInt32(Session["days_to_drop"]) != -1) {
                    pick_date_time = DateTime.Today.AddDays(Convert.ToInt32(Session["days_to_pick"]));
                    drop_date_time = DateTime.Today.AddDays(Convert.ToInt32(Session["days_to_drop"]));
                } else {
                    pick_date_time = DateTime.MinValue;
                    drop_date_time = DateTime.MinValue;
                }

                // Show pick dates
                calendar_pick.SelectedDate = pick_date_time;
                calendar_drop.SelectedDate = drop_date_time;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e) {
            bind_repeater();

            // Disable book button if dates are not selected
            if (Convert.ToInt32(Session["days_to_pick"]) == -1 || Convert.ToInt32(Session["days_to_drop"]) == -1) {
                foreach(RepeaterItem item in car_repeater.Items) {
                    Button reserve_button = (Button) item.FindControl("reserve_button");
                    reserve_button.Enabled = false;
                    reserve_button.Text = " Please Search Before Booking !!! ";
                }
            }

            // Set img urls
            int i = 0;
            foreach(RepeaterItem item in car_repeater.Items) {
                Image car_img = (Image) item.FindControl("car_img");
                // Row[] --> row. Row[][] --> colum data
                car_img.ImageUrl = $"/jpgs/{dataset_store.Tables["car_types"].Rows[i++][7].ToString().Trim()}.jpg";
            }
        }

        protected void reserve_clicked(object sender, CommandEventArgs e) {
            // Item index starts from 1 so use index - 1
            int id = (Int32)dataset_store.Tables["car_types"].Rows[Int32.Parse(e.CommandArgument.ToString()) - 1][0];
            database_helper db_helper = new database_helper();
            db_helper.bind_new_select_command($"SELECT id, type_id FROM cars WHERE type_id = {id} AND NOT EXISTS (SELECT * FROM bookings WHERE cars.id = car_id AND NOT ( ( bookings.days_to_pick >= {Session["days_to_drop"]} AND days_to_pick >= {Session["days_to_pick"]} ) OR ( bookings.days_to_drop <= {Session["days_to_drop"]} AND days_to_drop <= {Session["days_to_pick"]} ) ))");
            db_helper.fill_new_dataset("cars");

            // Store data table
            if (dataset_store.Tables["cars"] != null)
                dataset_store.Tables.Remove("cars");
            dataset_store.Tables.Add(db_helper.get_dataset().Tables["cars"].Copy());

            db_helper.execute_new_insert_command($"INSERT INTO bookings (car_id, days_to_pick, days_to_drop) VALUES ({dataset_store.Tables["cars"].Rows[0][0]}, {Session["days_to_pick"]}, {Session["days_to_drop"]})");
            db_helper.dispose_all_and_close();

            // Rebind
            bind_repeater();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Booking successful !!!')", true);
        }

        protected void pick_render(object sender, DayRenderEventArgs e) {
            // If before today or after drop date, make dates unselectable
            if (e.Day.Date < (System.DateTime.Today) ||
                (!drop_date_time.Equals(DateTime.MinValue) && e.Day.Date > drop_date_time)){
                
                make_unselectable(e);
            }
        }

        protected void drop_render(object sender, DayRenderEventArgs e) {
            // If before today or before pick date, make dates unselectable
            if (e.Day.Date < (System.DateTime.Today) ||
                (!pick_date_time.Equals(DateTime.MinValue) && e.Day.Date < pick_date_time)){
                
                make_unselectable(e);
            }
        }

        protected void selection_changed(object sender, EventArgs e) {
            // Assign new selected dates
            pick_date_time = calendar_pick.SelectedDate;
            drop_date_time = calendar_drop.SelectedDate;

            // If dates are selected assign then reload
            if (!pick_date_time.Equals(DateTime.MinValue) && !drop_date_time.Equals(DateTime.MinValue)) {
                Session["days_to_pick"] = (pick_date_time - DateTime.Today).Days;
                Session["days_to_drop"] = (drop_date_time - DateTime.Today).Days;

                Response.Redirect("Cars.aspx");
            }
        }

        protected void item_command(object sender, RepeaterCommandEventArgs e) {
            System.Diagnostics.Debug.WriteLine("aaa");
        }
    }
}