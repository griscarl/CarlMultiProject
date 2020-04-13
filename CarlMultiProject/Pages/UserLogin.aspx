<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ELibraryManagement.UserLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card" >
                    <img src="../Images/Pikabook_wide.png" class="card-img-top" >
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <h3>User Login</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <Label>Username</Label> 
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_Username" runat="server" placeholder="Username"></asp:TextBox>
                                </div>
                                <Label>Password</Label> 
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox_Password" runat="server" placeholder="Hunter2" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button class="btn btn-success btn-block btn-lg" ID="Button_Login" runat="server" Text="Login" OnClick="Button_Login_Click" />
                                </div>
                                <div class="form-group">
                                    <a href="UserSignup.aspx"><input class="btn btn-primary btn-block" id="Button_Signup" type="button" OnClick="Button_Signup_Click" value="Sign Up" /></a>
                                </div>
                                <div class="form-group">
                                    <input class="btn btn-outline-warning btn-sm" id="Button_ForgotPassword" type="button" OnClick="Button_ForgotPassword_Click" value="Forgot Password" />
                                </div>
                            </div>
                        </div>
                        <%--<a href="https://www.youtube.com/watch?v=oHg5SJYRHA0" class="btn btn-primary">Login</a>--%>
                    </div>
                </div>

                <a href="homepage.aspx">Back to Home</a>
            </div>
        </div>

    </div>

</asp:Content>
