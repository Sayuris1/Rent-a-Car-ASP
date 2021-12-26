using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace asp.Pages
{
    public partial class Admin : System.Web.UI.Page
    {
        static bool is_logged_in = false;
        static string last_clicked;
        static string active_tab = "home";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (is_logged_in){
                admin_login.Visible = false;

                Panel cars_panel = (Panel) Master.FindControl("MainContent").FindControl(active_tab);
                cars_panel.Visible = true;
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

            Panel active = (Panel) Master.FindControl("MainContent").FindControl(active_tab);
            active.Visible = false;
            
            last_clicked = clicked.ID;
            active_tab = active.ID;
        }
        protected void cars_tab_clicked(object sender, EventArgs e){
            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            DataSet cars_dataset = new DataSet();
            SqlCommand select_cars = new SqlCommand("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id", db_connection);
            SqlDataAdapter adapter_cars = new SqlDataAdapter(select_cars);

            adapter_cars.Fill(cars_dataset, "cars");
              
            DataList datalist_cars = (DataList)Master.FindControl("MainContent").FindControl("car_list");
            datalist_cars.DataSource = cars_dataset.Tables["cars"];
            datalist_cars.DataBind();

            select_cars.Dispose();
            db_connection.Close();

            Panel cars_panel = (Panel) Master.FindControl("MainContent").FindControl("cars_panel");
            cars_panel.Visible = true;

            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }

            Panel active = (Panel) Master.FindControl("MainContent").FindControl(active_tab);
            active.Visible = false;
            
            last_clicked = clicked.ID;
            active_tab = active.ID;
        }
        protected void bookings_tab_clicked(object sender, EventArgs e){
            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }

            Panel active = (Panel) Master.FindControl("MainContent").FindControl(active_tab);
            active.Visible = false;
            
            last_clicked = clicked.ID;
            active_tab = active.ID;
        }
        protected void edit_command(Object sender, DataListCommandEventArgs e) {
            car_list.EditItemIndex = e.Item.ItemIndex;

            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            DataSet cars_dataset = new DataSet();
            SqlCommand select_cars = new SqlCommand("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id", db_connection);
            SqlDataAdapter adapter_cars = new SqlDataAdapter(select_cars);

            adapter_cars.Fill(cars_dataset, "cars");
              
            DataList datalist_cars = (DataList)Master.FindControl("MainContent").FindControl("car_list");
            datalist_cars.DataSource = cars_dataset.Tables["cars"];
            datalist_cars.DataBind();

            select_cars.Dispose();
            db_connection.Close();
        }

        protected void cancel_command(Object sender, DataListCommandEventArgs e) {
            car_list.EditItemIndex = -1;

            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            DataSet cars_dataset = new DataSet();
            SqlCommand select_cars = new SqlCommand("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id", db_connection);
            SqlDataAdapter adapter_cars = new SqlDataAdapter(select_cars);

            adapter_cars.Fill(cars_dataset, "cars");
              
            DataList datalist_cars = (DataList)Master.FindControl("MainContent").FindControl("car_list");
            datalist_cars.DataSource = cars_dataset.Tables["cars"];
            datalist_cars.DataBind();

            select_cars.Dispose();
            db_connection.Close();
        }

        protected void delete_command(Object sender, DataListCommandEventArgs e) {
            car_list.EditItemIndex = -1;

            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            DataSet delete_dataset = new DataSet();
            string delete_id = ((Label)e.Item.FindControl("edit_car_id")).Text;

            SqlCommand delete_command = new SqlCommand("DELETE FROM cars WHERE id=" + delete_id, db_connection);
            SqlDataAdapter adapter_delete = new SqlDataAdapter(delete_command);

            adapter_delete.DeleteCommand = delete_command;
            adapter_delete.DeleteCommand.ExecuteNonQuery();

            DataSet cars_dataset = new DataSet();
            SqlCommand select_cars = new SqlCommand("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id", db_connection);
            SqlDataAdapter adapter_cars = new SqlDataAdapter(select_cars);

            adapter_cars.Fill(cars_dataset, "cars");
              
            DataList datalist_cars = (DataList)Master.FindControl("MainContent").FindControl("car_list");
            datalist_cars.DataSource = cars_dataset.Tables["cars"];
            datalist_cars.DataBind();

            select_cars.Dispose();
            db_connection.Close();
        }

        protected void update_command(Object sender, DataListCommandEventArgs e) {
            car_list.EditItemIndex = -1;

            SqlConnection db_connection =
                new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);

            db_connection.Open();

            DataSet update_dataset = new DataSet();
            string update_id = ((Label)e.Item.FindControl("edit_car_id")).Text;
            string update_type_id = ((TextBox)e.Item.FindControl("edit_car_type_id")).Text;

            SqlCommand update_command = new SqlCommand("Update cars SET type_id=" + update_type_id + "WHERE id=" + update_id, db_connection);
            SqlDataAdapter adapter_update = new SqlDataAdapter(update_command);

            adapter_update.UpdateCommand = update_command;
            adapter_update.UpdateCommand.ExecuteNonQuery();

            DataSet cars_dataset = new DataSet();
            SqlCommand select_cars = new SqlCommand("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id", db_connection);
            SqlDataAdapter adapter_cars = new SqlDataAdapter(select_cars);

            adapter_cars.Fill(cars_dataset, "cars");
              
            DataList datalist_cars = (DataList)Master.FindControl("MainContent").FindControl("car_list");
            datalist_cars.DataSource = cars_dataset.Tables["cars"];
            datalist_cars.DataBind();

            select_cars.Dispose();
            db_connection.Close();
        }
    }
}