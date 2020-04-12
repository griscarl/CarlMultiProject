using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarlLaptopProject.Pages
{
    public partial class UserSettings : System.Web.UI.Page
    {

        string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Populate_Boat_Dropdown();
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
                    DropDown_Boat.DataValueField = "BoatName";
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

        protected void btn_BoatDropdownClick(object sender, EventArgs e)
        {
            //Shows the appropriate fields for adding a new boat to the user's boat list.
            ListItem newItem = new ListItem("New Boat", "New Boat", true);
            DropDown_Boat.Items.Add(newItem);
            DropDown_Boat.SelectedValue = "New Boat";
            BoatDetails.Visible = true;
        }

        protected void DropDown_Boat_IndexChange(object sender, EventArgs e)
        {
            if (DropDown_Boat.SelectedValue != "New Boat")
            { 
                DropDown_Boat.Items.Remove("New Boat");
                BoatDetails.Visible = false;
            } 
        }

        protected void Button_SaveBoat_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspInsertBoat @UserId, @BoatName, @BoatModel, @InsuranceNumber, @RadioCallSign, @Length, @Width, @Weight, @MastHeight, @FuelCapacity, @FuelBurn, @ServiceInterval, @IsActive", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                cmd.Parameters.AddWithValue("@BoatName", TextBox_BoatName.Text.Trim());			
                cmd.Parameters.AddWithValue("@BoatModel", TextBox_BoatModel.Text.Trim());
                cmd.Parameters.AddWithValue("@InsuranceNumber", TextBox_InsuranceNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@RadioCallSign", TextBox_RadioCallSign.Text.Trim());
                cmd.Parameters.AddWithValue("@Length", TextBox_Length.Text.Trim());
                cmd.Parameters.AddWithValue("@Width", TextBox_Width.Text.Trim());
                cmd.Parameters.AddWithValue("@Weight", TextBox_Weight.Text.Trim());			
                cmd.Parameters.AddWithValue("@MastHeight", TextBox_MastHeight.Text.Trim());
                cmd.Parameters.AddWithValue("@FuelCapacity", TextBox_FuelCapacity.Text.Trim());
                cmd.Parameters.AddWithValue("@FuelBurn", TextBox_FuelBurn.Text.Trim());
                cmd.Parameters.AddWithValue("@ServiceInterval", TextBox_ServiceInterval.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", 1);
                cmd.ExecuteNonQuery();

                con.Close();
                BoatDetails.Visible = false;
                Response.Write("<script>alert('Boat Created');</script>");
                Response.Redirect("UserSettings.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
        }

        //TODO: Implement Button_SaveUserSettings_Click
        protected void Button_SaveUserSettings_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    SqlConnection con = new SqlConnection(strCon);
            //    if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
            //    {
            //        con.Open();
            //    }

            //    //Create the command (Is it possible to simply write a function name here?)
            //    SqlCommand cmd = new SqlCommand("EXEC uspUpdateUser @UserId, ", con);

            //    //Define the variables in the SqlCommand
            //    cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            //    cmd.ExecuteNonQuery();

            //    con.Close();
            //    Response.Write("<script>alert('UserUpdated');</script>");
            //    Response.Redirect("UserSettings.aspx");
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("<script>alert('" + ex.Message + "');</script>");
            //    throw;
            //}

            Response.Write("<script>alert('Not Implemented yet');</script>");
        }
    }
}