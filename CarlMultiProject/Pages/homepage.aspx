<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="ELibraryManagement.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="HomepageSection">
        <img src="../Images/BarnTrio.jpg" width="4000" class="img-fluid img-background" />
        <div class="row">
            <div class="col-md-4">
                <h1 style="text-align: center">Logbook</h1>
                <asp:ImageButton CssClass="img-fluid" ID="ImageButton_Logbook" src="../Images/Sailboat_symbol.svg.png" OnClick="ImageButton_LogbookClick" runat="server" />
            </div>
            <div class="col-md-4">
                <h1>Other Project</h1>
                <asp:ImageButton CssClass="img-fluid" width=2000px ID="ImageButton1" src="../Images/user-male.png" runat="server" />
            </div>
            <div class="col-md-4">
                <h1>Third Project</h1>
                <asp:ImageButton CssClass="img-fluid" width="2000px" ID="ImageButton2" src="../Images/satisfactory.jpg" runat="server" />
            </div>
        </div>
    </section>
</asp:Content>

