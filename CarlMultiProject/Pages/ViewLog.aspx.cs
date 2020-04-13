using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarlMultiProject.Pages
{
    public partial class ViewLog : System.Web.UI.Page
    {
        string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                Populate_Boat_Dropdown();
                FillGridView();
            }

        }
        private void Populate_Boat_Dropdown()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspGetBoats @UserId ", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    DropDown_Boat.Items.Add(new ListItem("New Boat"));
                    Response.Write("<script>alert('No boats found');</script>");

                }
                else
                {
                    DropDown_Boat.DataSource = dt;
                    DropDown_Boat.DataValueField = "BoatId";
                    DropDown_Boat.DataTextField = "BoatName";
                }

                DropDown_Boat.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
        }
        protected void DropDown_Boat_IndexChange(object sender, EventArgs e)
        {
            //if (Populate_Ongoing())
            //{

            //}
            FillGridView();
        }

        protected void Linkbutton_EditLogEntry_Click(object sender, EventArgs e)
        {
            int logEntryId = Convert.ToInt32((sender as LinkButton).CommandArgument);

            Response.Write("<script>alert('logEntryId: " + logEntryId + "');</script>");

        }
        private void FillGridView()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC logbook.ViewLogEntries @BoatId", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@BoatId", DropDown_Boat.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // felsök
                string s = "";
                foreach (DataRow dataRow in dt.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        s+=item.ToString();
                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }
    }
}