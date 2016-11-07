<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HanLearning.Dictionary.Users.Login" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading"><h3>Login</h3></div>
                <div class="panel-body">
                    <% if (state != LoginState.Success) { %>
                        <div class="alert alert-danger">
                            <% if (state == LoginState.NoUsername) { %>User does not exist<% } %>
                            <% else if (state == LoginState.BadPassword) { %>Bad Password<% } %>
                        </div>
                    <% } %>
                    <form class="login main-login" runat="server">
                        <asp:Label AssociatedControlID="username" runat="server">Username</asp:Label>
                        <asp:TextBox ID="username" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label AssociatedControlID="password" runat="server">Password</asp:Label>
                        <asp:TextBox ID="password" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:Button Text="Login" CssClass="btn btn-primary" runat="server" />
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>