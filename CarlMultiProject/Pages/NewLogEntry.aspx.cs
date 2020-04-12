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
    public partial class NewLogEntry : System.Web.UI.Page
    {

        //Get the connectionstring from the Web.Config
        string strCon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack) //only first time page is loaded, not on button press.
            {
                Populate_Boat_Dropdown();
                if (Populate_Ongoing())
                {
                    Response.Write("<script>alert('Unconfirmed log entry exists!');</script>");
                } else
                {
                    Populate_Date_Time();
                }

            }

        }
        protected void DropDown_Boat_IndexChange(object sender, EventArgs e)
        {
            if (Populate_Ongoing())
            {

            }
        }

        private bool Populate_Ongoing()
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspGetOngoingLogEntry " +
                    "@BoatId = @bi ", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@bi", DropDown_Boat.SelectedValue);

                SqlDataReader dr =  cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    ViewState["LogEntryId"] = null;
                    return false;
                }
                while (dr.Read())
                {
                    ViewState["LogEntryId"] = dr.GetInt32(0);
                    TextBox_StartDate.Text = dr["DatetimeStart"].ToString().Substring(0, 10);
                    TextBox_StartTime.Text = dr["DatetimeStart"].ToString().Substring(11, 5);
                    TextBox_EndDate.Text = dr["DatetimeEnd"].ToString().Substring(0, 10); ;
                    TextBox_EndTime.Text = dr["DatetimeEnd"].ToString().Substring(11, 5);
                    TextBox_Distance.Text = dr["DistanceInNM"].ToString();
                    TextBox_FuelIntake.Text = dr["FuelIntakeInLiters"].ToString();
                    TextBox_FromLocation.Text = dr["FromLocation"].ToString();
                    TextBox_ToLocation.Text = dr["ToLocation"].ToString();
                    TextBox_Notes.Text = dr["Notes"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
            return true;
        }

        private void Populate_Date_Time()
        {
            DateTime now = DateTime.Now;
            TextBox_StartDate.Text = now.ToString("yyyy-MM-dd");
            TextBox_EndDate.Text = now.ToString("yyyy-MM-dd");
            TextBox_StartTime.Text = now.ToString("hh:mm");
            TextBox_EndTime.Text = now.ToString("hh:mm");
        }

        protected void Button_SaveLogEntry_Click(object sender, EventArgs e)
        {
            if (ViewState["LogEntryId"] == null)
            {
                Insert_Log_Entry(1);
            }
            else
            {
                Response.Write("<script>alert('Exists ongoing! " + "LogEntryId: " + ViewState["LogEntryId"] + "');</script>");
                Update_Log_Entry(1);
            }
        }

        private void Update_Log_Entry(int isOngoing)
        {
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC logbook.LogEntry_Update " +
                    "@LogEntryId = @lei, @BoatId = @bi, @DatetimeStart = @dts,@DatetimeEnd = @dte,@DistanceInNM = @dinm,@FuelIntakeInLiters = @fiil,@FromLocation = @fl,@ToLocation = @tl,@Notes = @n, @IsOngoing=@io", con);

                string sqlDateTimeStart = TextBox_StartDate.Text + " " + TextBox_StartTime.Text + ":00.000";
                string sqlDateTimeEnd = TextBox_EndDate.Text + " " + TextBox_EndTime.Text + ":00.000";
                //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("lei", ViewState["LogEntryId"]);
                cmd.Parameters.AddWithValue("@bi", DropDown_Boat.SelectedValue);
                cmd.Parameters.AddWithValue("@dts", sqlDateTimeStart);
                cmd.Parameters.AddWithValue("@dte", sqlDateTimeEnd);
                cmd.Parameters.AddWithValue("@dinm", TextBox_Distance.Text.Trim());
                cmd.Parameters.AddWithValue("@fiil", TextBox_FuelIntake.Text.Trim());
                cmd.Parameters.AddWithValue("@fl", TextBox_FromLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@tl", TextBox_ToLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@n", TextBox_Notes.Text.Trim());
                cmd.Parameters.AddWithValue("@io", isOngoing);
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Entry Updated Successfully!');</script>");

                //Response.Redirect("UserProfile.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
            if (isOngoing == 0)
            {
                Response.Redirect("ViewLog.aspx");
            }
        }

        protected void Button_ConfirmLogEntry_Click(object sender, EventArgs e)
        {
            if(ViewState["LogEntryId"] == null)
            {
                if (Validate_Input()) 
                {
                    Insert_Log_Entry(0);
                }
            }
            else
            {
                if (Validate_Input())
                {
                    Update_Log_Entry(0);
                }
            }
        }

        private bool Validate_Input()
        {
            if (TextBox_Distance.Text.Trim() != "" && TextBox_FuelIntake.Text.Trim() != "" && TextBox_FromLocation.Text.Trim() != "" && TextBox_ToLocation.Text.Trim() != "")
                //&& TextBox_StartDate.Text != "" && TextBox_StartTime.Text != "" && TextBox_EndDate.Text != "" & TextBox_EndTime.Text. != ""
            {
                return true;
            } 
            else
            {
                Response.Write("<script>alert('Not Valid inputs');</script>");
                return false;
            }
        }

        private void Insert_Log_Entry(int isOngoing)
        {
            Page.DataBind();
            try
            {
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC logbook.LogEntry_Insert " +
                    "@BoatId = @bi, @DatetimeStart = @dts,@DatetimeEnd = @dte,@DistanceInNM = @dinm,@FuelIntakeInLiters = @fiil,@FromLocation = @fl,@ToLocation = @tl,@Notes = @n, @IsOngoing=@io",  con);

                string sqlDateTimeStart = TextBox_StartDate.Text + " " + TextBox_StartTime.Text + ":00.000";
                string sqlDateTimeEnd = TextBox_EndDate.Text + " " + TextBox_EndTime.Text + ":00.000";
                //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@bi", DropDown_Boat.SelectedValue);
                cmd.Parameters.AddWithValue("@dts", sqlDateTimeStart);
                cmd.Parameters.AddWithValue("@dte", sqlDateTimeEnd);
                cmd.Parameters.AddWithValue("@dinm", TextBox_Distance.Text.Trim());
                cmd.Parameters.AddWithValue("@fiil", TextBox_FuelIntake.Text.Trim());
                cmd.Parameters.AddWithValue("@fl", TextBox_FromLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@tl", TextBox_ToLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@n", TextBox_Notes.Text.Trim());
                cmd.Parameters.AddWithValue("@io", isOngoing);
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Entry inserted Successfully!');</script>");

                //Response.Redirect("UserProfile.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
            if (isOngoing == 0) 
            {
                Response.Redirect("ViewLog.aspx");
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

        protected void Button_SetStartTime_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TextBox_StartTime.Text = now.ToString("hh:mm");
        }
        protected void Button_SetEndTime_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TextBox_EndTime.Text = now.ToString("hh:mm");
        }
    }
}