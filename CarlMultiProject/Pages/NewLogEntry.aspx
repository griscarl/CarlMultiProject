<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="NewLogEntry.aspx.cs" Inherits="CarlMultiProject.Pages.NewLogEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <img src="../Images/logbook_big.jpg" width="100" class="card-img-top">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                    <h3>New Log Entry</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col6 mx-auto">
                                <asp:DropDownList class="form-control" ID="DropDown_Boat" runat="server" OnSelectedIndexChanged="DropDown_Boat_IndexChange" AutoPostBack="true">
                                    <asp:ListItem Value="Database Connection Issue"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label Text="Start Date" runat="server" />
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_StartDate" runat="server" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label Text="Start Time" runat="server" />
                                <div class="form-group">
                                    <div class="input-group flex-nowrap">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_StartTime" runat="server" TextMode="Time"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:Button ID="Button_SetStartTime" CssClass="btn btn-primary" Text="Now" runat="server" OnClick="Button_SetStartTime_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label Text="End Date" runat="server" />
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_EndDate" runat="server" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label Text="End Time" runat="server" />
                                <div class="form-group">
                                    <div class="input-group flex-nowrap">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_EndTime" runat="server" TextMode="Time"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:Button ID="Button_SetEndTime" CssClass="btn btn-primary" Text="Now" runat="server" OnClick="Button_SetEndTime_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label Text="Distance" runat="server" />
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_Distance" runat="server" Text="0" TextMode="Number"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">NM</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label Text="Fuel Intake" runat="server" />
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox_FuelIntake" runat="server" Text="0" TextMode="Number"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text">l</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label Text="From" runat="server" />
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_FromLocation" runat="server" placeholder="Harbour1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <asp:Label Text="To" runat="server" />
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_ToLocation" runat="server" placeholder="Harbour2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:Label Text="Notes" runat="server" />
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_Notes" runat="server" placeholder="Write interesting notes here..." TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group">
                                    <asp:Button ID="Button_SaveLogEntry" CssClass="form-control btn-outline-success btn-lg" runat="server" Text="Save Log Entry" OnClick="Button_SaveLogEntry_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6 mx-auto">
                                <div class="form-group">
                                    <asp:Button ID="Button_ConfirmLogEntry" CssClass="form-control btn-success btn-block" runat="server" Text="Confirm Log entry" OnClick="Button_ConfirmLogEntry_Click" />
                                </div>
                            </div>
                        </div>
                        <%--<a href="https://www.youtube.com/watch?v=oHg5SJYRHA0" class="btn btn-primary">Login</a>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
