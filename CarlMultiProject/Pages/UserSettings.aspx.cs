using EncryptionandDecryption;
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
            if (Session["UserId"] == null)
            {
                Response.Redirect("homepage.aspx");
            }
            if (!IsPostBack)
            {
                Populate_Boat_Dropdown();
                Populate_User_Settings();
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

        protected void Button_SaveUserSettings_Click(object sender, EventArgs e)
        {

            if(Valid_UserSettings())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strCon);
                    if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                    {
                        con.Open();
                    }

                    //Create the command (Is it possible to simply write a function name here?)
                    SqlCommand cmd = new SqlCommand("EXEC uspUpdateUser @UserId, @FirstName, @LastName, @Email ", con);

                    //Define the variables in the SqlCommand
                    cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    cmd.Parameters.AddWithValue("@FirstName", TextBox_FirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", TextBox_LastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", TextBox_Email.Text.Trim());
                    cmd.ExecuteNonQuery();

                    con.Close();
                    Response.Write("<script>alert('User Updated Successfully');</script>");
                    
                    Populate_User_Settings();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                    throw;
                }
            }
        }

        protected void Button_UpdatePassword_Click(object sender, EventArgs e)
        {
            if (ValidPassword())
            {
                Response.Write($"<script>alert('Valid password');</script>");
                UpdatePassword();
            } else
            { 
                Response.Write($"<script>alert('Invalid password');</script>");
                //string message = $"Old Password {TextBox_OldPassword.Text} New Password {Textbox_NewPassword.Text}";
                //string encryptedPassword = Cryptography.Encrypt(Textbox_NewPassword.Text);
                //Response.Write($"<script>alert('{message} encrypted password: {encryptedPassword}');</script>");
            }
        }

        private void UpdatePassword()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspUpdateUserPassword @UserId, @PasswordEncrypted", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                cmd.Parameters.AddWithValue("@PasswordEncrypted", Cryptography.Encrypt(Textbox_NewPassword.Text));
                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('Password Updated Successfully');</script>");

                Populate_User_Settings();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
        }

        private bool ValidPassword()
        {
            string passwordEncrypted = "";
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspGetUser @UserId ", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    Response.Write("<script>alert('No User found');</script>");

                }
                else
                {
                    passwordEncrypted = dt.Rows[0]["PasswordEncrypted"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
            if (passwordEncrypted == Cryptography.Encrypt(TextBox_OldPassword.Text))
            {
                return true;
            } 
            else
            {
                return false;
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
                cmd.Parameters.AddWithValue("@Length", Math.Round(Convert.ToDecimal(TextBox_Length.Text),2));
                cmd.Parameters.AddWithValue("@Width", Math.Round(Convert.ToDecimal(TextBox_Width.Text),2));
                cmd.Parameters.AddWithValue("@Weight", TextBox_Weight.Text.Trim());			
                cmd.Parameters.AddWithValue("@MastHeight", TextBox_MastHeight.Text.Trim());
                cmd.Parameters.AddWithValue("@FuelCapacity", TextBox_FuelCapacity.Text.Trim());
                cmd.Parameters.AddWithValue("@FuelBurn", TextBox_FuelBurn.Text.Trim());
                cmd.Parameters.AddWithValue("@ServiceInterval", TextBox_ServiceInterval.Text.Trim());
                cmd.Parameters.AddWithValue("@IsActive", 1);
                cmd.ExecuteNonQuery();

                //cmd.Parameters.AddWithValue("@Width", Math.Round(Convert.ToDecimal(TextBox_OilIntake.Text), 2));

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

        private void Populate_User_Settings()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspGetUser @UserId ", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    Response.Write("<script>alert('No User found');</script>");

                }
                else
                {
                    TextBox_FirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    TextBox_LastName.Text = dt.Rows[0]["LastName"].ToString();
                    TextBox_Email.Text = dt.Rows[0]["Email"].ToString();

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
        private bool Valid_UserSettings()
        {
            //See if the UserSetting-inputs are valid
            if (TextBox_FirstName.Text.Trim() != null && TextBox_LastName.Text.Trim() != null && TextBox_Email.Text.Trim() != null)
            {
                return true;
            } 
            else
            {
                Response.Write("<script>alert('Invalid User Settings. Empty values not allowed.');</script>");
                return false;
            }
        }
    }
}