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

namespace ELibraryManagement
{
    public partial class UserLogin : System.Web.UI.Page
    {

        string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Signup_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Not implemented, contact admin');</script>");
        }
        protected void Button_ForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Not implemented, contact admin');</script>");
        }
        protected void Button_Login_Click(object sender, EventArgs e)
        {
            if (ValidPassword())
            {
                somefunction();
            } 
            else
            {
                Response.Write("<script>alert('Invalid Credentials');</script>");
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
                SqlCommand cmd = new SqlCommand("EXEC uspGetUser @Username = @un ", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@un", TextBox_Username.Text);
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

            if (passwordEncrypted == Cryptography.Encrypt(TextBox_Password.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void somefunction()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("EXEC uspGetUser @Username = @un", con);
                cmd.Parameters.AddWithValue("@un", TextBox_Username.Text.Trim());

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {

                        //Set session variables.
                        Session["UserId"] = dr["UserId"];
                        Session["Username"] = dr["Username"];
                        Session["IsAdmin"] = dr["IsAdmin"];
                        Response.Write("<script>alert('Welcome " + dr["Username"] + " ID = " + dr["UserId"] + "IsAdmin = " + dr["IsAdmin"] + "');</script>");
                        Response.Redirect("homepage.aspx");

                    }

                }
                else
                {
                    Response.Write("<script>alert('Invalid Credentials');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
        }
    }
}