using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp
{
    public partial class _Default : Page {
        static DateTime pick_date_time = new DateTime();
        static DateTime drop_date_time = new DateTime();

        bool is_dates_selected(DateTime pick_date, DateTime drop_date){
            // Is both dates are selected
            if (!pick_date_time.Equals(DateTime.MinValue) && !drop_date_time.Equals(DateTime.MinValue))
                return true;
            else
                return false;
        }
        
        void make_unselectable(DayRenderEventArgs e){
            // Makes dates on calendar uselectable
            e.Day.IsSelectable = false;
            e.Cell.BackColor = System.Drawing.Color.DarkGray;
        }

        // ***************************************
        // ********** Asp.net functions **********
        // ***************************************

        protected void Page_Load(object sender, EventArgs e){
            // Reset selected days
            if (!IsPostBack){
                pick_date_time = DateTime.MinValue;
                drop_date_time = DateTime.MinValue;

                // How many days to pick and drop from today
                // -1 means not selected
                Session["days_to_pick"] = -1;
                Session["days_to_drop"] = -1;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e){
            // Disable search button if dates are not selected on post back
            if (!is_dates_selected(pick_date_time, drop_date_time)){
                Button search_button = (Button)Master.FindControl("MainContent").FindControl("search_button");
                search_button.Enabled = false;
                search_button.Text = " Plase Select Dates Before Searching !!! ";
            }
        }

        protected void pick_render(object sender, DayRenderEventArgs e)
        {
            // If before today or after drop date, make dates unselectable
            if (e.Day.Date < (DateTime.Today)
                || (!drop_date_time.Equals(DateTime.MinValue) && e.Day.Date > drop_date_time)){

                make_unselectable(e);
            }
        }

        protected void drop_render(object sender, DayRenderEventArgs e){
            // If before today or before pick date, make dates unselectable
            if (e.Day.Date < (DateTime.Today)
                || (!pick_date_time.Equals(DateTime.MinValue) && e.Day.Date < pick_date_time)){

                make_unselectable(e);
            }
        }

        protected void selection_changed(object sender, EventArgs e){
            // Assign selected dates
            pick_date_time = calendar_pick.SelectedDate;
            drop_date_time = calendar_drop.SelectedDate;

            // If dates are selected, enable search button
            if (is_dates_selected(pick_date_time, drop_date_time)){
                Button search_button = (Button)Master.FindControl("MainContent").FindControl("search_button");
                search_button.Enabled = true;
                search_button.Text = " Search a Car To Rent ";
            }
        }

        protected void search_clicked(object sender, EventArgs e){
            // If dates are selected, assign days to session and redirect
            if (is_dates_selected(pick_date_time, drop_date_time)){
                Session["days_to_pick"] = (pick_date_time - DateTime.Today).Days;
                Session["days_to_drop"] = (drop_date_time - DateTime.Today).Days;

                Response.Redirect("Cars.aspx");
            }
        }
    }
}