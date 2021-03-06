﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
                    Populate_Everything();
                }

            }

        }
        protected void DropDown_Boat_IndexChange(object sender, EventArgs e)
        {
            if (Populate_Ongoing())
            {

            } else
            {
                Populate_Everything();
            }
        }

        protected void Button_SaveLogEntry_Click(object sender, EventArgs e)
        {
            if (ViewState["LogEntryId"] == null)
            {
                Insert_Log_Entry(1);
            }
            else
            {
                //Response.Write("<script>alert('Exists ongoing! " + "LogEntryId: " + ViewState["LogEntryId"] + "');</script>");
                Update_Log_Entry(1);
            }

            Response.Redirect("NewLogEntry.aspx");
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

            Response.Redirect("ViewLog.aspx");
        }

        protected void Button_SetStartTime_Click(object sender, EventArgs e)
        {
            DateTime now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central European Standard Time");
            TextBox_StartTime.Text = now.ToString("hh:mm");
        }
        protected void Button_SetEndTime_Click(object sender, EventArgs e)
        {
            DateTime now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central European Standard Time");


            TextBox_EndTime.Text = now.ToString("hh:mm");

        }

        //User defined functions
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
                    TextBox_Distance.Text = dr["DistanceInNM"].ToString().Replace(',','.');
                    TextBox_FuelIntake.Text = dr["FuelIntakeInLiters"].ToString().Replace(',', '.');
                    TextBox_Distance.Text = dr["DistanceInNM"].ToString().Replace(',', '.');
                    TextBox_FuelIntake.Text = dr["FuelIntakeInLiters"].ToString().Replace(',', '.');
                    TextBox_Tacho.Text = dr["Tacho"].ToString().Replace(',', '.');
                    TextBox_OilIntake.Text = dr["OilIntake"].ToString().Replace(',', '.');
                    TextBox_FromLocation.Text = dr["FromLocation"].ToString();
                    TextBox_ToLocation.Text = dr["ToLocation"].ToString();
                    TextBox_Notes.Text = dr["Notes"].ToString();
                    //Response.Write("<script>alert('+" + dr["FullTank"].ToString() + "');</script>");
                    Console.WriteLine($"Distance: {dr["DistanceInNM"].ToString()} Fuelintake: {dr["FuelIntakeInLiters"].ToString()} OilIntake: {dr["OilIntake"].ToString()} Tacho: {dr["Tacho"].ToString()}");

                    if (dr["FullTank"].ToString() == "True")
                    {
                        CheckBox_FullTank.Checked = true;
                    }
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
        private void Insert_Log_Entry(int isOngoing)
        {
            string message = "'Entry inserted Successfully!'";
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
                    "@BoatId = @bi, @DatetimeStart = @dts,@DatetimeEnd = @dte,@DistanceInNM = @dinm,@FuelIntakeInLiters = @fiil, @FullTank = @ft, @Tacho = @t, @OilIntake = @oi, @FromLocation = @fl,@ToLocation = @tl,@Notes = @n, @IsOngoing=@io",  con);

                string sqlDateTimeStart = TextBox_StartDate.Text + " " + TextBox_StartTime.Text + ":00.000";
                string sqlDateTimeEnd = TextBox_EndDate.Text + " " + TextBox_EndTime.Text + ":00.000";
                //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                System.Globalization.CultureInfo usCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@bi", DropDown_Boat.SelectedValue);
                cmd.Parameters.AddWithValue("@dts", sqlDateTimeStart);
                cmd.Parameters.AddWithValue("@dte", sqlDateTimeEnd);
                cmd.Parameters.AddWithValue("@dinm", Math.Round(Decimal.Parse(TextBox_Distance.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@fiil", Math.Round(Decimal.Parse(TextBox_FuelIntake.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@t", Math.Round(Decimal.Parse(TextBox_Tacho.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@oi", Math.Round(Decimal.Parse(TextBox_OilIntake.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@fl", TextBox_FromLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@tl", TextBox_ToLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@n", TextBox_Notes.Text.Trim());
                cmd.Parameters.AddWithValue("@io", isOngoing);
                if (CheckBox_FullTank.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ft", 1);
                    message += " full tank";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ft", 0);
                    message += " not full tank";
                }
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert(' " + message + "');</script>");

                //Response.Redirect("UserProfile.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
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
                    "@LogEntryId = @lei, @BoatId = @bi, @DatetimeStart = @dts,@DatetimeEnd = @dte,@DistanceInNM = @dinm,@FuelIntakeInLiters = @fiil, @FullTank = @ft ,@FromLocation = @fl,@ToLocation = @tl,@Notes = @n, @IsOngoing=@io", con);

                string sqlDateTimeStart = TextBox_StartDate.Text + " " + TextBox_StartTime.Text + ":00.000";
                string sqlDateTimeEnd = TextBox_EndDate.Text + " " + TextBox_EndTime.Text + ":00.000";
                //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                System.Globalization.CultureInfo usCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("lei", ViewState["LogEntryId"]);
                cmd.Parameters.AddWithValue("@bi", DropDown_Boat.SelectedValue);
                cmd.Parameters.AddWithValue("@dts", sqlDateTimeStart);
                cmd.Parameters.AddWithValue("@dte", sqlDateTimeEnd);
                cmd.Parameters.AddWithValue("@dinm", Math.Round(Decimal.Parse(TextBox_Distance.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@fiil", Math.Round(Decimal.Parse(TextBox_FuelIntake.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@t", Math.Round(Decimal.Parse(TextBox_Tacho.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@oi", Math.Round(Decimal.Parse(TextBox_OilIntake.Text, usCulture), 2));
                cmd.Parameters.AddWithValue("@fl", TextBox_FromLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@tl", TextBox_ToLocation.Text.Trim());
                cmd.Parameters.AddWithValue("@n", TextBox_Notes.Text.Trim());
                cmd.Parameters.AddWithValue("@io", isOngoing);

                if (CheckBox_FullTank.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@ft", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ft", 0);

                }
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
        private void Populate_Date_Time()
        {
            DateTime now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central European Standard Time");
            TextBox_StartDate.Text = now.ToString("yyyy-MM-dd");
            TextBox_EndDate.Text = now.ToString("yyyy-MM-dd");
            TextBox_StartTime.Text = now.ToString("hh:mm");
            TextBox_EndTime.Text = now.ToString("hh:mm");
        }
        private void Populate_Everything()
        {
            try
            {
                
                SqlConnection con = new SqlConnection(strCon);
                if (con.State == System.Data.ConnectionState.Closed) //Make sure the connection is open.
                {
                    con.Open();
                }

                //Create the command (Is it possible to simply write a function name here?)
                SqlCommand cmd = new SqlCommand("EXEC uspGetLogEntryInfo @BoatId", con);

                //Define the variables in the SqlCommand
                cmd.Parameters.AddWithValue("@BoatId", DropDown_Boat.SelectedValue);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string msg = "";
                    TextBox_Tacho.Text = dr["LatestTacho"].ToString().Replace(',', '.');
                    TextBox_RemainingFuel.Text = dr["FuelLeftInTank"].ToString().Replace(',', '.');
                    TextBox_RemainingTacho.Text = dr["TachoLeftInTank"].ToString().Replace(',', '.');
                    TextBox_EngineService.Text = dr["HoursToEngineService"].ToString().Replace(',', '.');


                    msg += $"LatestTacho: {dr["LatestTacho"]} ";
                    msg += $"SumFuel: {dr["SumFuel"]} ";
                    msg += $"SumTacho: {dr["SumTacho"]} ";
                    msg += $"FuelPerTacho: {dr["FuelPerTacho"]} ";
                    msg += $"SumTachoSinceFullTank: {dr["SumTachoSinceFullTank"]} ";
                    msg += $"FuelUsedSinceFullTank: {dr["FuelUsedSinceFullTank"]} ";
                    msg += $"FuelCapacity: {dr["FuelCapacity"]} ";
                    msg += $"FuelLeftInTank: {dr["FuelLeftInTank"]} ";
                    msg += $"TachoLeftInTank: {dr["TachoLeftInTank"]} ";
                    msg += $"ServiceInterval: {dr["ServiceInterval"]} ";
                    msg += $"SumTachoAll: {dr["SumTachoAll"]} ";
                    msg += $"HoursToEngineService: {dr["HoursToEngineService"]} ";

                    Textbox_Debug.Text = msg;

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                throw;
            }
        }

    }
}