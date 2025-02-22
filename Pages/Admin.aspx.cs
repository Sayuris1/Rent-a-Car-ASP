﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

namespace asp.Pages
{
    public partial class Admin : database_helper {
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

        void fill_bookings_datalist(){
            // Configure db operations
            database_helper db_helper = new database_helper();
            db_helper.bind_new_select_command("SELECT * FROM bookings");
            db_helper.fill_new_dataset("bookings");
            db_helper.data_list_bind("bookings_list", "bookings", Master.FindControl("MainContent"));
            db_helper.dispose_all_and_close();
        }

        bool is_correct_for_car(string type_id){
            int _;
            if (type_id == ""){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert empty data !!!')", true);
                return false;
            }
            else if (!Int32.TryParse(type_id, out _)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert string for int !!!')", true);
                return false;
            }
            else{
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
                else{
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Car type doesnt exists !!!')", true);
                    return false;
                }
            }
        }

        bool is_correct_for_car_type(string daily_cost, string door, string passenger, string transmission, string ac, string fuel, string name){
            int _;
            string[] transmission_targets = {"Auto", "Manuel"};
            string[] fuel_targets = {"Diesel", "Hybrid", "Gas", "LPG"};
            string[] ac_targets = {"True", "False"};
            if(daily_cost == "" || door == "" || passenger == "" || transmission == "" || fuel == "" || name == ""){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert empty data !!!')", true);
                return false;
            }
            else if (!Int32.TryParse(daily_cost, out _) || !Int32.TryParse(door, out _) || !Int32.TryParse(passenger, out _)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert string for int !!!')", true);
                return false;
            }
            else if (!transmission_targets.Contains(transmission)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert wrong transmission !!!')", true);
                return false;
            }
            else if(!fuel_targets.Contains(fuel)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert wrong fuel !!!')", true);
                return false;
            }
            else if (!ac_targets.Contains(ac)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert wrong ac !!!')", true);
                return false;
            }
            else
                return true;
        }

        bool is_correct_for_bookings(string car_id, string days_to_pick, string days_to_drop){
            int _;
            if (car_id == "" || days_to_pick == "" || days_to_drop == ""){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert empty data !!!')", true);
                return false;
            }
            else if (!Int32.TryParse(car_id, out _) || !Int32.TryParse(days_to_pick, out _) || !Int32.TryParse(days_to_drop, out _)){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert string for int !!!')", true);
                return false;
            }
            else if (Convert.ToInt32(days_to_drop) < Convert.ToInt32(days_to_pick) || Convert.ToInt32(days_to_drop) < 0){
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cant insert wrong dates !!!')", true);
                return false;
            }
            else{
                // Get all type ids
                database_helper check = new database_helper();
                check.bind_new_select_command("SELECT id FROM cars");
                check.fill_new_dataset("cars");
                DataSet type_ids = check.get_dataset();

                // Select type ids
                DataRow[] found_rows = type_ids.Tables["cars"].Select("id = " + car_id);

                check.dispose_all_and_close();

                if (found_rows.Length > 0)
                    return true;
                else{
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Car id doesnt exists')", true);
                    return false;
                }
            }
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
            fill_bookings_datalist();
            change_tab(sender, "bookings_panel");
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

            if (is_correct_for_car(update_type_id)){
                // If exist, update
                database_helper db_helper = new database_helper();
                db_helper.execute_new_update_command($"Update cars SET type_id={update_type_id}WHERE id={update_id}");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_car_datalist();
        }

        protected void add_car(object sender, EventArgs e){
            // Get strings
            string add_type_id = ((TextBox)Master.FindControl("MainContent").FindControl("add_car_type_id")).Text;

            if (is_correct_for_car(add_type_id)){
                // If exist, add
                database_helper db_helper = new database_helper();
                db_helper.execute_new_insert_command($"INSERT INTO cars (type_id) VALUES ({add_type_id})");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_car_datalist();
        }

        // ************************************************
        // ********** Car types table functions ***********
        // ************************************************

        protected void car_types_edit(object sender, DataListCommandEventArgs e){
            // Make edit view visible
            car_types_list.EditItemIndex = e.Item.ItemIndex;

            fill_car_types_datalist();
        }

        protected void car_types_cancel(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            car_types_list.EditItemIndex = -1;

            fill_car_types_datalist();
        }

        protected void car_types_delete(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            car_types_list.EditItemIndex = -1;

            // Delete
            string delete_id = ((Label)e.Item.FindControl("car_type_edit_id")).Text;

            database_helper db_helper = new database_helper();
            db_helper.execute_new_delete_command("DELETE FROM car_types WHERE id=" + delete_id);
            db_helper.dispose_all_and_close();

            // Refill
            fill_car_types_datalist();
        }

        protected void car_types_update(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            car_types_list.EditItemIndex = -1;

            // Get strings
            string update_id = ((Label)e.Item.FindControl("car_type_edit_id")).Text;
            string update_daily_cost = ((TextBox)e.Item.FindControl("edit_daily_cost")).Text;
            string update_door = ((TextBox)e.Item.FindControl("edit_door")).Text;
            string update_passenger = ((TextBox)e.Item.FindControl("edit_passenger")).Text;
            string update_transmission = ((TextBox)e.Item.FindControl("edit_transmission")).Text.Trim();
            string update_ac = ((TextBox)e.Item.FindControl("edit_ac")).Text;
            string update_fuel = ((TextBox)e.Item.FindControl("edit_fuel")).Text.Trim();
            string update_name = ((TextBox)e.Item.FindControl("edit_name")).Text.Trim();

            if(is_correct_for_car_type(update_daily_cost, update_door, update_passenger, update_transmission, update_ac, update_fuel, update_name)){
                // True is a string
                if(update_ac == "True")
                    update_ac = "1";
                else
                    update_ac = "0";

                // Update
                database_helper db_helper = new database_helper();
                db_helper.execute_new_update_command($"UPDATE car_types SET daily_cost={update_daily_cost}, door={update_door}, passenger={update_passenger}, transmission='{update_transmission}'");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_car_types_datalist();
        }

        protected void add_car_type(object sender, EventArgs e){
            // Get strings
            string update_daily_cost = ((TextBox)Master.FindControl("MainContent").FindControl("add_daily_cost")).Text;
            string update_door = ((TextBox)Master.FindControl("MainContent").FindControl("add_door")).Text;
            string update_passenger = ((TextBox)Master.FindControl("MainContent").FindControl("add_passenger")).Text;
            string update_transmission = ((TextBox)Master.FindControl("MainContent").FindControl("add_transmission")).Text.Trim();
            string update_ac = ((TextBox)Master.FindControl("MainContent").FindControl("add_ac")).Text;
            string update_fuel = ((TextBox)Master.FindControl("MainContent").FindControl("add_fuel")).Text.Trim();
            string update_name = ((TextBox)Master.FindControl("MainContent").FindControl("add_name")).Text.Trim();

            if(is_correct_for_car_type(update_daily_cost, update_door, update_passenger, update_transmission, update_ac, update_fuel, update_name)){
                // True is a string
                if(update_ac == "True")
                    update_ac = "1";
                else
                    update_ac = "0";

                // Add
                database_helper db_helper = new database_helper();
                db_helper.execute_new_insert_command($"INSERT INTO car_types (daily_cost, door, passenger, transmission, ac, fuel, name) VALUES ({update_daily_cost}, {update_door}, {update_passenger}, '{update_transmission}', {update_ac}, '{update_fuel}', '{update_name}')");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_car_types_datalist();
        }

        // ***********************************************
        // *********** Bookings table functions **********
        // ***********************************************


        protected void bookings_edit(object sender, DataListCommandEventArgs e){
            // Make edit view visible
            bookings_list.EditItemIndex = e.Item.ItemIndex;

            fill_bookings_datalist();
        }

        protected void bookings_cancel(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            bookings_list.EditItemIndex = -1;

            fill_bookings_datalist();
        }

        protected void bookings_delete(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            bookings_list.EditItemIndex = -1;

            // Delete
            string delete_id = ((Label)e.Item.FindControl("booking_id")).Text;

            database_helper db_helper = new database_helper();
            db_helper.execute_new_delete_command("DELETE FROM bookings WHERE id=" + delete_id);
            db_helper.dispose_all_and_close();

            // Refill
            fill_bookings_datalist();
        }

        protected void bookings_update(object sender, DataListCommandEventArgs e){
            // Make edit view not visible
            bookings_list.EditItemIndex = -1;

            // Get strings
            string update_id = ((Label)e.Item.FindControl("booking_id")).Text;
            string update_car_id = ((TextBox)e.Item.FindControl("edit_booking_car_id")).Text;
            string update_days_to_pick = ((TextBox)e.Item.FindControl("edit_days_to_pick")).Text;
            string update_days_to_drop = ((TextBox)e.Item.FindControl("edit_days_to_drop")).Text;

            if (is_correct_for_bookings(update_car_id, update_days_to_pick, update_days_to_drop)){
                // If exist, update
                database_helper db_helper = new database_helper();
                db_helper.execute_new_update_command($"Update bookings SET car_id={update_car_id}, days_to_pick={update_days_to_pick}, days_to_drop={update_days_to_drop} WHERE id={update_id}");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_bookings_datalist();
        }

        protected void add_booking(object sender, EventArgs e){
            // Get strings
            string update_car_id = ((TextBox)Master.FindControl("MainContent").FindControl("add_car_id")).Text;
            string update_days_to_pick = ((TextBox)Master.FindControl("MainContent").FindControl("add_days_to_pick")).Text;
            string update_days_to_drop = ((TextBox)Master.FindControl("MainContent").FindControl("add_days_to_drop")).Text;

            if (is_correct_for_bookings(update_car_id, update_days_to_pick, update_days_to_drop)){
                // Add
                database_helper db_helper = new database_helper();
                db_helper.execute_new_insert_command($"INSERT INTO bookings (car_id, days_to_pick, days_to_drop) VALUES ({update_car_id}, {update_days_to_pick}, {update_days_to_drop})");
                db_helper.dispose_all_and_close();
            }

            // Refill
            fill_bookings_datalist();

        }
    }
}