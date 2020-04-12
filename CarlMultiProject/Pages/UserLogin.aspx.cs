using System;
using System.Collections.Generic;
using System.Configuration;
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

            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("EXEC uspLoginUser @Username, @Password", con);
                cmd.Parameters.AddWithValue("@Username", TextBox_Username.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", TextBox_Password.Text.Trim());

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
                        Response.Redirect("UserSettings.aspx");

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