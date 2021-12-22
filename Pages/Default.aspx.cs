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
        DateTime pick_date_time = new DateTime();
        DateTime drop_date_time = new DateTime();
        int dates_to_pick = 0;
        int dates_to_drop = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnDayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < (System.DateTime.Now.AddDays(-1)))

            {

                e.Day.IsSelectable = false;

                e.Cell.BackColor = System.Drawing.Color.DarkGray;

            }
        }
        protected void pick_selection_changed(object sender, EventArgs e)
        {
            pick_date_time = calendar_pick.SelectedDate;
        }

        protected void drop_selection_changed(object sender, EventArgs e)
        {
            drop_date_time = calendar_drop.SelectedDate;
        }

        protected void search_clicked(object sender, EventArgs e)
        {
            dates_to_pick = (DateTime.Now - pick_date_time).Days;
            dates_to_drop = (DateTime.Now - drop_date_time).Days;
        }
    }
}