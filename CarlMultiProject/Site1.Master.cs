using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                //label_Username.Text = Session["Username"].ToString()+ " " + Session["UserId"].ToString();
                LinkButton_NewLogEntry.Visible = true;
                LinkButton_ViewLog.Visible = true;
                LinkButton_Logout.Visible = true;
                LinkButton_UserLogin.Visible = false;
                LinkButton_SignUp.Visible = false;
                LinkButton_Emergency.Visible = true;
                LinkButton_Settings.Visible = true;
            }
        }

        protected void Linkbutton_AdminLogin_Click(object sender, EventArgs e)
        {
            //Response.Redirect("homepage.aspx");

            Response.Write("<script>alert('Not implemented.');</script>");
        }

        protected void Linkbutton_AuthorManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void Linkbutton_PublisherManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void Linkbutton_BookInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void Linkbutton_BookIssuing_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void Linkbutton_MemberManagement_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagement.aspx");
        }


        protected void Linkbutton_UserLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");
        }

        protected void Linkbutton_SignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void Linkbutton_HelloUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("#");
        }

        protected void LinkButton_NewLogEntry_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewLogEntry.aspx");
        }

        protected void LinkButton_ViewLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewLog.aspx");
        }

        protected void LinkButton_Emergency_Click(object sender, EventArgs e)
        {
            Response.Redirect("Emergency.aspx");
        }
        protected void LinkButton_UserSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSettings.aspx");
        }
        protected void  Linkbutton_Logout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["UserId"] = null;
            LinkButton_Logout.Visible = false;
            LinkButton_UserLogin.Visible = true;
            LinkButton_SignUp.Visible = true;
            LinkButton_Emergency.Visible = false;
            LinkButton_Settings.Visible = false;
            Response.Redirect("homepage.aspx");
        }
    }
}