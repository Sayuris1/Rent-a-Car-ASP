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

namespace asp
{
    public class database_helper : Page{
        SqlConnection db_connection;
        SqlCommand sql_command;
        SqlDataAdapter sql_adapter;

        DataSet data_set;

        public database_helper(){
            // Init
            db_connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["db"].ConnectionString);
            sql_command = new SqlCommand("", db_connection);
            sql_adapter = new SqlDataAdapter(this.sql_command);

            // Open
            db_connection.Open();
        }

        public void bind_new_select_command(string command){
            // If not null, dispose before new bind
            if (sql_command != null)
                sql_command.Dispose();
            
            // New bind
            sql_command = new SqlCommand(command, db_connection);
            sql_adapter.SelectCommand = sql_command;
        }

        public void execute_new_delete_command(string command){
            // If not null, dispose before new bind
            if (sql_command != null)
                sql_command.Dispose();
            
            // New bind
            sql_command = new SqlCommand(command, db_connection);
            sql_adapter.DeleteCommand = sql_command;
            sql_adapter.DeleteCommand.ExecuteNonQuery();
        }

        public void execute_new_update_command(string command){
            // If not null, dispose before new bind
            if (sql_command != null)
                sql_command.Dispose();
            
            // New bind
            sql_command = new SqlCommand(command, db_connection);
            sql_adapter.UpdateCommand = sql_command;
            sql_adapter.UpdateCommand.ExecuteNonQuery();
        }

        public void execute_new_insert_command(string command){
            // If not null, dispose before new bind
            if (sql_command != null)
                sql_command.Dispose();
            
            // New bind
            sql_command = new SqlCommand(command, db_connection);
            sql_adapter.InsertCommand = sql_command;
            sql_adapter.InsertCommand.ExecuteNonQuery();
        }

        public void fill_new_dataset(string table_name){
            data_set = new DataSet();
            sql_adapter.Fill(data_set, table_name);
        }

        public void data_list_bind(string data_list_id, string table_name, Control main){
            DataList data_list = (DataList)main.FindControl(data_list_id);
            data_list.DataSource = data_set.Tables[table_name];
            data_list.DataBind();
        }

        public void repeater_bind(string repeater_id, string table_name, Control main){
            Repeater repeater = (Repeater)main.FindControl(repeater_id);
            repeater.DataSource = data_set.Tables[table_name];
            repeater.DataBind();
        }

        public void dispose_all_and_close(){
            if (sql_command != null)
                sql_command.Dispose();
            if (sql_adapter != null)
                sql_adapter.Dispose();
            if (data_set != null)
                data_set.Dispose();

            db_connection.Close();
            db_connection.Dispose();
        }

        public DataSet get_dataset(){
            return data_set;
        }
    }
}