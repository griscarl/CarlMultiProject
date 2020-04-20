<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewLog.aspx.cs" Inherits="CarlMultiProject.Pages.ViewLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-10 mx-auto">
                <h3>Captains Log</h3>
            </div>
        </div>
        <div class="card">
            <%--<img src="../Images/logbook_old_top.gif" width="100" class="card-img-top">--%>
            <div class="card-body">
                <div class="row">
                    <div class="col6 mx-auto">
                        <asp:DropDownList class="form-control" ID="DropDown_Boat" runat="server" OnSelectedIndexChanged="DropDown_Boat_IndexChange" AutoPostBack="true">
                            <asp:ListItem Value="Database Connection Issue"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:con %>" SelectCommand="
SELECT
	*
FROM
	[logbook].[LogEntry] LE"></asp:SqlDataSource>
                    <div class="col">
                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LogEntryId">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="Button_EditLogEntry" CssClass="btn btn-primary" Text="Edit" runat="server" OnClick="Button_EditLogEntry_Click" CommandArgument='<%# Eval("LogEntryId")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DatetimeStart" HeaderText="DEP" SortExpression="DatetimeStart" >
                                <ControlStyle Width="100px" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="LogEntryId" HeaderText="LogEntryId" SortExpression="DatetimeEnd" />--%>
                                <asp:BoundField DataField="DatetimeEnd" HeaderText="ARR" SortExpression="DatetimeEnd" />
                                <asp:BoundField DataField="TripTime" HeaderText="TripTime" SortExpression="TripTime" />
                                <asp:BoundField DataField="DistanceInNM" HeaderText="Distance in NM" SortExpression="DistanceInNM" />
                                <asp:BoundField DataField="AccumulatedDistance" HeaderText="Acc.Distance" SortExpression="AccumulatedDistance" />
                                <asp:BoundField DataField="Tacho" HeaderText="TACHO" SortExpression="Tacho" />
                                <asp:BoundField DataField="AccumulatedTacho" HeaderText="Acc.Tacho" SortExpression="AccumulatedTacho" />
                                <asp:BoundField DataField="FullTank" HeaderText="Full Tank" SortExpression="FullTank" />
                                <asp:BoundField DataField="FuelIntakeInLiters" HeaderText="Fuel" SortExpression="Fuel" />
                                <asp:BoundField DataField="AccumulatedFuel" HeaderText="Acc.Fuel" SortExpression="AccumulatedFuel" />
                                <asp:BoundField DataField="OilIntake" HeaderText="Oil" SortExpression="OilIntake" />
                                <asp:BoundField DataField="AccumulatedOil" HeaderText="Acc.Oil" SortExpression="AccumulatedOil" />
                                <asp:BoundField DataField="FromLocation" HeaderText="From" SortExpression="FromLocation" />
                                <asp:BoundField DataField="ToLocation" HeaderText="To" SortExpression="ToLocation" />
                                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--<img src="../Images/logbook_old_bot.gif" width="100px" class="card-img-bottom">--%>
        </div>
    </div>
</asp:Content>
