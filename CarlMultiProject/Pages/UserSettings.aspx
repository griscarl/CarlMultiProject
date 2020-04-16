<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="CarlLaptopProject.Pages.UserSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-10 mx-auto">
                <div class="card">
                    <img class="rounded mx-auto d-block img-fluid" src="../Images/settings2.png" />
                    <h3>Settings!</h3>
                    <div class="row">
                        <div class="col">
                            <hr />
                        </div>
                    </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Name</label>
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox_FirstName" runat="server" placeholder="Christian"></asp:TextBox>
                                        <asp:TextBox class="form-control" ID="TextBox_LastName" runat="server" placeholder="Moronsson"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Moron Level</label>
                                    <asp:TextBox class="form-control" ID="TextBox_MoronLevel" Text="10" TextMode="Number" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Email</label>
                                    <asp:TextBox class="form-control" ID="TextBox_Email" runat="server" placeholder="abc@def.com" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Favoritpizza</label>
                                    <asp:TextBox class="form-control" ID="TextBox_Pizza" Text="Kebabpizza" ReadOnly="true" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-xl-3">
                                <asp:Button ID="Button_SaveUserSettings" CssClass="btn btn-primary btn-block" Text="Update User Settings" OnClick="Button_SaveUserSettings_Click" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-xl-3">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Old Password:</span>
                                        </div>
                                        <asp:TextBox ID="TextBox_OldPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-xl-3">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-prepend flex-nowrap">
                                            <span class="input-group-text">New Password:</span>
                                        </div>
                                        <asp:TextBox ID="Textbox_NewPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-xl-3">
                                <asp:Button ID="Button_UpdatePassword" CssClass="btn btn-primary btn-block" Text="Update Password" OnClick="Button_UpdatePassword_Click" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <label>Default Boat</label>
                                <div class="input-group mb-6">
                                    <asp:DropDownList ID="DropDown_Boat" runat="server" OnSelectedIndexChanged="DropDown_Boat_IndexChange" AutoPostBack="true">
                                        <asp:ListItem Value="Database Connection Issues"></asp:ListItem>

                                    </asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:Button ID="btn_BoatDropdown" class="btn btn-primary btn ml-1" Text="New Boat" OnClick="btn_BoatDropdownClick" runat="server" Width="100px"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="BoatDetails" Visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Boat Name</label>
                                        <asp:TextBox class="form-control" ID="TextBox_BoatName" runat="server" placeholder="RMS Titanic"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Boat Model</label>
                                        <asp:TextBox class="form-control" ID="TextBox_BoatModel" runat="server" placeholder="Parant"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Insurance Number</label>
                                        <asp:TextBox class="form-control" ID="TextBox_InsuranceNumber" runat="server" placeholder="Insurance Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Radio Call Sign</label>
                                        <asp:TextBox class="form-control" ID="TextBox_RadioCallSign" runat="server" placeholder="Radio Call Sign"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Length</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_Length" runat="server" placeholder="0.00" Step="0.01" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon3">m</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Width</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_Width" runat="server" placeholder="0.00" Step="0.01" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon4">m</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Weight</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_Weight" runat="server" placeholder="Weight" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon5">kg</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label>Mast Height</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_MastHeight" runat="server" placeholder="Mast Height" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon6">m</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Fuel Capacity</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_FuelCapacity" runat="server" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon8">l</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Fuel Burn</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_FuelBurn" runat="server" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon7">l/h</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Service Interval</label>
                                        <div class="input-group flex-nowrap">
                                            <asp:TextBox class="form-control" ID="TextBox_ServiceInterval" runat="server" TextMode="Number"></asp:TextBox>
                                            <div class="input-group-append">
                                                <span class="input-group-text form-control" id="basic-addon9">h</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <div class="form-group">
                                        <label>Säkert någonting mer jag inte kommer på just nu</label>
                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" TextMode="Multiline" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4 mx-auto">
                                    <asp:Button ID="Button_SaveBoat" CssClass="btn btn-success btn-block btn-lg" Text="Save Boat" runat="server" OnClick="Button_SaveBoat_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
