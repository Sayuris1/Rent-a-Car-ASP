using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp
{
    public partial class _Default : Page
    {
        static DateTime pick_date_time = new DateTime();
        static DateTime drop_date_time = new DateTime();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["days_to_pick"] = -1;
            Session["days_to_drop"] = -1;

            if (IsCrossPagePostBack){
                pick_date_time = DateTime.MinValue;
                drop_date_time = DateTime.MinValue;
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (pick_date_time.Equals(DateTime.MinValue) || drop_date_time.Equals(DateTime.MinValue)){
                Button search_button = (Button)Master.FindControl("MainContent").FindControl("search_button");
                search_button.Enabled = false;
                search_button.Text = " Plase Select Dates Before Searching !!! ";
            }
        }

        protected void OnDayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < (System.DateTime.Now.AddDays(-1)))

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
                Button search_button = (Button)Master.FindControl("MainContent").FindControl("search_button");
                search_button.Enabled = true;
                search_button.Text = " Search a Car To Rent ";
            }
        }

        protected void search_clicked(object sender, EventArgs e)
        {
            if (pick_date_time.Equals(DateTime.MinValue) || drop_date_time.Equals(DateTime.MinValue)) {

            }

            else {
                Session["days_to_pick"] = (pick_date_time.AddDays(1) - DateTime.Now).Days;
                Session["days_to_drop"] = (drop_date_time.AddDays(1) - DateTime.Now).Days;

                Response.Redirect("Cars.aspx");
            }
            
        }
    }
}