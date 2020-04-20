using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //User Defined Functions
        protected void ImageButton_LogbookClick(object sender, EventArgs e)
        {
            if(Session["UserId"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                Response.Redirect("NewLogEntry.aspx");
            }
        }
    }
}