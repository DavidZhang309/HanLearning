<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HanLearning.Dictionary.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftNavbar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightNavbar" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <form class="search-form" method="post">
                <div class="input-group">
                    <span class="input-group-addon">Search</span>
                    <input type="text" id="search-input" name="search" class="form-control" />
                    <span class="input-group-btn">
                        <input type="submit" value="Search" class="btn btn-primary" />
                    </span>
                </div>
            </form>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default result hidden">
                <div class="panel-heading">
					<h3>Search Results</h3>
				</div>
				<div class="panel-body">
                    <div class="load-info hidden alert alert-info"><i class="fa fa-spinner fa-spin"></i> Loading</div>
					<div class="load-fail hidden alert alert-danger">Unable to find any words</div>
					<div id="result-area"></div>
				</div>
            </div>
        </div>
    </div>
    <!-- Templates -->
    <div id="templates" class="hidden">
        <div class="def-entry row" data-entry-code="0">
			<div class="col-xs-3">
				<div class="character"></div>
			</div>
			<div class="col-xs-9">
                <div class="row">
                    <div class="col-md-6">
					    <h4>Reading</h4>
					    <div class="readings"></div>
				    </div>
				    <div class="col-md-6">
					    <h4>Definition</h4>
					    <div class="definitions"></div>
				    </div>
				    <div class="col-md-12 collapse">
					    <h4>Option</h4>
					    <div class="options">
                            <button class="learn btn btn-info">Learn</button>
					    </div>
				    </div>
                </div>
			</div>
		</div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="FooterScripts" runat="server">
    <script type="text/javascript">
        var isLoggedIn = <%= ((HanLearning.Masters.Site)Master).UserID != -1 ? "true" : "false" %>;
        function populateSearch() {
            var $resultPanel = $('.result.panel');
            var $infoAlert = $resultPanel.find(".load-info");
            var $errorAlert = $resultPanel.find(".load-fail");
            var $result = $("#result-area").html("");
            $resultPanel.removeClass('hidden');
            $infoAlert.removeClass('hidden');
            $errorAlert.addClass('hidden');

            //parse search data
            var xml = ["<query>"];
            xml.push("<culture>", $(".culture-select option:selected").val(), "</culture><chars>");
            var searchText = $(".search-form #search-input").val();
            for (i = 0; i < searchText.length; i++) {
                xml.push("<char>", searchText.charCodeAt(i), "</char>");
            }
            xml.push("</chars></query>");

            console.log(xml.join(""));

            $.ajax({
                url: 'Queries/CharLookup.ashx',
                type: 'post',
                dataType: 'xml',
                data: xml.join(''),
                success: function (data) {
                    if (!handleServerError(data)) {
                        return;
                    }
                    // remove alerts
                    $infoAlert.addClass('hidden');

                    $.each(data.getElementsByTagName("character"), function (charIndex, charElement) {
                        var $template = $("#templates .def-entry").clone();
                        $template.attr("data-entry-code", charElement.getAttribute("utf"));
                        $template.find(".character").html("&#" + charElement.getAttribute("utf"));

                        //readings
                        $.each(charElement.getElementsByTagName("reading"), function (readingIndex, readingElement) {
                            var system = readingElement.getAttribute("system");
                            var prefix = (system == null || system == "") ? "" : (system + ": ");
                            $template.find(".readings").append("<div>" + prefix + readingElement.textContent + " (" + readingElement.getAttribute("source") + ")</div>");
                        });

                        //definitions
                        $.each(charElement.getElementsByTagName("definition"), function (readingIndex, defElement) {
                            $template.find(".definitions").append("<div>" + defElement.textContent + " (" + defElement.getAttribute("source") + ")</div>");
                        });



                        $result.append($template);
                    });
                },
                error: function (response) {
                    handleError(response);
                    $infoAlert.addClass('hidden');
                    $errorAlert.removeClass('hidden');
                }
            });

            $resultPanel.find('#search-input').text();
        }

        var temp = null;

        $(document).ready(function () {
            var $form = $('.search-form').submit(function (e) {
                e.preventDefault();
                
                populateSearch();
            });
        }).on('change', '.culture-select', function(){
            populateSearch();
        }).on('click', '.def-entry', function(){
            if (isLoggedIn) {
                $(this).find('.collapse').collapse('toggle');   
            }
        }).on('click', '.def-entry .learn', function(e){
            e.stopPropagation();
            var $this = $(this);
            updateLearning($this.closest('.def-entry').attr('data-entry-code'), 1, function() {
                $this.addClass("unlearn btn-danger")
                    .removeClass("learn btn-info")
                    .text("Stop Learning");
            });
        }).on('click', '.def-entry .unlearn', function(e){
            e.stopPropagation();
            var $this = $(this);
            updateLearning($this.closest('.def-entry').attr('data-entry-code'), 0, function() {
                $this.addClass("learn btn-info")
                    .removeClass("unlearn btn-danger")
                    .text("Learn");
            });
        });
    </script>
</asp:Content>