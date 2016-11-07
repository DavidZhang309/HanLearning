<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="TextTranslator.aspx.cs" Inherits="HanLearning.Tools.TextTranslator" %>
<asp:Content ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <asp:TextBox ID="textBlock" TextMode="MultiLine" CssClass="form-control textarea-block" runat="server"></asp:TextBox>
        <asp:Label AssociatedControlID="characterSetOption" runat="server">Character Set: </asp:Label>
        <asp:DropDownList ID="characterSetOption" CssClass="form-control" runat="server">
            <asp:ListItem Value="0" Text="Normal"></asp:ListItem>
            <asp:ListItem Value="2" Text="Traditional"></asp:ListItem>
            <asp:ListItem Value="1" Text="Simplified"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button Text="Analyse" CssClass="btn btn-default" runat="server" />

        <div class="row">
            <div class="col-sm-6">
                <h3>Set</h3>
                <div>Total Number of words: </div>
                <div>Number of words known: </div>
                <div>Percentage of words known: </div>
            </div>
            <div class="col-sm-6">
                <h3>Text</h3>
                <div>Total Number of words: </div>
                <div>Number of words known: </div>
                <div>Percentage of words known: </div>
            </div>
        </div>

        <h3>Analysed Text</h3>
        <div class="character-block analysis">
            <asp:PlaceHolder ID="resultArea" runat="server"></asp:PlaceHolder>
        </div>
    </form>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterScripts" runat="server">
</asp:Content>
