<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HanLearning.Dictionary.Learning.Default" %>
<%@ Import Namespace="HanLearning.Data" %>

<asp:Content ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="LeftNavbar" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="RightNavbar" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% List<CharacterData> learned = new List<CharacterData>(); %>

    <% foreach(CharacterData learning in query.CharacterData.Characters.Values) { %>
        <% if (learning.LearningStatus == LearningStatus.Learned) { learned.Add(learning); continue; } %>
        <% else if (learning.LearningStatus == LearningStatus.DoNotKnow) { continue; } %>
        <div class="learning-entry row" data-code="<%= learning.UTFCode %>">
            <div class="col-xs-6 col-md-10">
                <div class="row">
                    <div class="character col-xs-12 col-md-2">&#<%= learning.UTFCode %></div>
                    <div class="col-xs-12 col-md-8">
                        <div>
                            <h4>Readings</h4>
                            <% foreach(CharacterReading reading in learning.Readings) { %>
                                <div><%= reading.Reading %></div>
                            <% } %>
                        </div>
                        <div>
                            <h4>Definitions</h4>
                            <% foreach(CharacterDefinition definition in learning.Definitions) { %>
                                <div><%= definition.Definition %></div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="options col-xs-6 col-md-2">
                <% if (learning.LearningStatus == LearningStatus.Learning) { %>
                    <button class="stop btn btn-danger">Stop Learning</button>
                <% } %>
                <% if (((HanLearning.Masters.Site)Master).SelfLearning) { %>
                    <button class="learned btn btn-success">Learned</button>
                <% } %>
            </div>
        </div>
    <% } %>

    <div class="row">
        <div class="col-md-12">
            <div class="learned-board-header" data-toggle="collapse" data-target=".learned-board">Learned Characters (Count: <%= learned.Count %>)</div>
        </div>
    </div>
    <div class="learned-board row collapse">
        <% foreach(CharacterData learning in learned) { %>
        <div class="character col-xs-3 col-sm-2 col-md-1">&#<%= learning.UTFCode %></div>
        <% } %>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterScripts" runat="server">
    <script type="text/javascript">
        $(document).on("click", ".options .stop", function () {
            var $this = $(this);
            var $entry = $this.closest(".learning-entry"); 
            var code = $entry.attr("data-code");
            updateLearning(code, 0, function (data) {
                $entry.remove();
            })
        }).on("click", ".options .learned", function () {
            var $this = $(this);
            var $entry = $this.closest(".learning-entry");
            var code = $entry.attr("data-code");
            updateLearning(code, 3, function (data) {
                $entry.remove();
            })
        });

    </script>
</asp:Content>