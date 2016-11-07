<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HanLearning.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftNavbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightNavbar" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading"><h3>Importer</h3></div>
                <div class="panel-body">
                    <form method="post">
                        <input value="Unihan" class="hidden" />
                        <div class="input-group">
                            <span class="input-group-addon">Unihan data</span>
                            <input type="file" name="unihan-file" class="form-control" />
                            <span class="input-group-addon">
                                <input type="checkbox" name="drop-data" />
                                <label>Drop Data</label>
                            </span>
                            <span class="input-group-btn">
                                <input type="submit" value="Import" class="btn btn-primary" />
                            </span>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterScripts" runat="server">
</asp:Content>
