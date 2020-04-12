<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Emergency.aspx.cs" Inherits="CarlLaptopProject.Pages.Emergency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12 mx-auto">
                <img class="rounded mx-auto d-block img-fluid" src="../Images/KeepCalm.jpg" />
                <hr />
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" runat="server">Radio Call Sign</span>
                        </div>
                        <asp:TextBox ID="TextBox_RadioCallSign" class="form-control" Text="SN050650502" runat ="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text" runat="server">Phone</span>
                        </div>
                        <asp:TextBox ID="TextBox1" class="form-control" Text="90510" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="form-group">
                        <asp:TextBox ID="TextBox_Instructions1" CssClass="form-control" Text="Some instructions to follow here..." runat="server" TextMode="MultiLine" ReadOnly="True"> </asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
