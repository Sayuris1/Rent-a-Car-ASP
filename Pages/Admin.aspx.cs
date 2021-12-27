using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace asp.Pages
{
    public partial class Admin : database_helper
    {
        static bool is_logged_in = false;

        static string last_clicked;
        static string active_tab = "home";

        void change_tab(object sender, string current_tab){
            Panel last_panel = (Panel) Master.FindControl("MainContent").FindControl(active_tab);
            last_panel.Visible = false;

            Panel active_panel = (Panel) Master.FindControl("MainContent").FindControl(current_tab);
            active_panel.Visible = true;

            Button clicked = (Button) sender;
            clicked.CssClass = clicked.CssClass.Replace("w3-hover-red", "w3-red");

            if (last_clicked != null){
                Button last_button = (Button) Master.FindControl("MainContent").FindControl(last_clicked);
                last_button.CssClass = last_button.CssClass.Replace("w3-red", "w3-hover-red");
            }

            last_clicked = clicked.ID;
            active_tab = current_tab;
        }

        void fill_car_datalist(){
            // Configure db operations
            database_helper db_helper = new database_helper();
            db_helper.bind_new_select_command("SELECT cars.id, cars.type_id, car_types.name FROM cars JOIN car_types ON cars.type_id = car_types.id");
            db_helper.fill_new_dataset("cars");
            db_helper.data_list_bind("car_list", "cars", Master.FindControl("MainContent"));
            db_helper.dispose_all_and_close();
        }

        void fill_car_types_datalist(){
            // Configure db operations
            database_helper db_helper = new database_helper();
            db_helper.bind_new_select_command("SELECT * FROM car_types");
            db_helper.fill_new_dataset("car_types");
            db_helper.data_list_bind("car_types_list", "car_types", Master.FindControl("MainContent"));
            db_helper.dispose_all_and_close();
        }

        bool is_type_id_exist(string type_id){
            // Get all type ids
            database_helper check = new database_helper();
            check.bind_new_select_command("SELECT id FROM car_types");
            check.fill_new_dataset("car_types");
            DataSet type_ids = check.get_dataset();

            // Select type ids
            DataRow[] found_rows = type_ids.Tables["car_types"].Select("id = " + type_id);

            check.dispose_all_and_close();

            if (found_rows.Length > 0)
                return true;
            else
                return false;
        }

        // ***************************************
        // ********** Asp.net functions **********
        // ***************************************
        
        protected void Page_Load(object sender, EventArgs e){
            // If logged in
            if (is_logged_in){
                // Disable login panel
                admin_login.Visible = false;
            }
            else{
                // Disable logged in panel
                logged_in_panel.Visible = false;
            }
        }

        protected void on_authenticate(object sender, AuthenticateEventArgs e){
            // If username and password are true, call on_logged_in
            if (admin_login.UserName == "admin" && admin_login.Password == "123")
                e.Authenticated = true;
            else
                e.Authenticated = false;
        }

        protected void on_logged_in(object sender, EventArgs e){
            is_logged_in = true;
        }
        
        // ***********************************
        // ********** Tab functions **********
        // ***********************************

        protected void car_types_tab_clicked(object sender, EventArgs e){
            // Configure db operations
            fill_car_types_datalist();
            change_tab(sender, "car_types_panel");
        }

        protected void cars_tab_clicked(object sender, EventArgs e){
            // Configure db operations
            fill_car_datalist();
            change_tab(sender, "cars_panel");
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


        // *****************************************
        // ********** Car table functions **********
        // *****************************************

        protected void car_edit(Object sender, DataListCommandEventArgs e) {
            // Make edit view visible
            car_list.EditItemIndex = e.Item.ItemIndex;

            fill_car_datalist();
        }

        protected void car_cancel(Object sender, DataListCommandEventArgs e) {
            // Make edit view not visible
            car_list.EditItemIndex = -1;

            fill_car_datalist();
        }

        protected void car_delete(Object sender, DataListCommandEventArgs e) {
            // Make edit view not visible
            car_list.EditItemIndex = -1;

            // Delete
            string delete_id = ((Label)e.Item.FindControl("edit_car_id")).Text;

            database_helper db_helper = new database_helper();
            db_helper.execute_new_delete_command("DELETE FROM cars WHERE id=" + delete_id);
            db_helper.dispose_all_and_close();

            // Refill
            fill_car_datalist();
        }

        protected void car_update(Object sender, DataListCommandEventArgs e) {
            // Make edit view not visible
            car_list.EditItemIndex = -1;

            // Get strings
            string update_id = ((Label)e.Item.FindControl("edit_car_id")).Text;
            string update_type_id = ((TextBox)e.Item.FindControl("edit_car_type_id")).Text;

            if (is_type_id_exist(update_type_id)){
                // If exist, update
                database_helper db_helper = new database_helper();
                db_helper.execute_new_update_command("Update cars SET type_id=" + update_type_id + "WHERE id=" + update_id);
                db_helper.dispose_all_and_close();
            }
            else
                // Else, alert message
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Type Id Doesnt Exist !!')", true);

            // Refill
            fill_car_datalist();
        }

        protected void add_car(object sender, EventArgs e){
            // Get strings
            string add_type_id = ((TextBox)Master.FindControl("MainContent").FindControl("add_car_type_id")).Text;

            if (is_type_id_exist(add_type_id)){
                // If exist, add
                database_helper db_helper = new database_helper();
                db_helper.execute_new_insert_command("INSERT INTO cars (type_id) VALUES (" + add_type_id + ")");
                db_helper.dispose_all_and_close();
            }
            else
                // Else, alert message
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Type Id Doesnt Exist !!')", true);

            // Refill
            fill_car_datalist();
        }

        // ************************************************
        // ********** Car types table functions ***********
        // ************************************************

        protected void car_types_edit(object sender, EventArgs e){
        }

        protected void car_types_cancel(object sender, EventArgs e){
        }

        protected void car_types_delete(object sender, EventArgs e){
        }

        protected void car_types_update(object sender, EventArgs e){
        }

        protected void car_types_add(object sender, EventArgs e){
        }
    }
}