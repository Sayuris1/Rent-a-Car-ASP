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
    }
}