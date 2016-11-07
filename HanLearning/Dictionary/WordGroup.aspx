<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="WordGroup.aspx.cs" Inherits="HanLearning.Dictionary.WordGroup" %>
<%@ Import Namespace="HanLearning.Data" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% foreach(WordGroupData group in Query.Groups) { %>
        <div class="group-entry row" data-group-id="<%= group.GroupID %>">
            <div class="name col-sm-4"><%= group.GroupName %></div>
            <div class="content col-sm-8">
                <% foreach(char c in group.Content) { %>
                    &#<%= Convert.ToInt32(c) %>                    
                <% } %>
            </div>
        </div>
    <% } %>
</asp:Content>