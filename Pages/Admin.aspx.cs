using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace asp.Pages
{
    public partial class Admin : System.Web.UI.Page
    {
        static bool is_logged_in = false;
        static string last_clicked;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (is_logged_in){
                admin_login.Visible = false;
            }
            else {
                logged_in_panel.Visible = false;
            }
        }

        protected void on_authenticate(object sender, AuthenticateEventArgs e)
        {
            if (admin_login.UserName == "admin" && admin_login.Password == "123")
                e.Authenticated = true;
            else
                e.Authenticated = false;
        }

        protected void on_logged_in(object sender, EventArgs e){
            is_logged_in = true;
        }
        
        protected void car_types_tab_clicked(object sender, EventArgs e){
            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }
            

            last_clicked = clicked.ID;
        }
        protected void cars_tab_clicked(object sender, EventArgs e){
            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }
            

            last_clicked = clicked.ID;
        }
        protected void bookings_tab_clicked(object sender, EventArgs e){
            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }
            

            last_clicked = clicked.ID;
        }
    }
}