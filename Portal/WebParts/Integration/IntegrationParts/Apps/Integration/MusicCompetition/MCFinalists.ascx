<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCFinalistsV2" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>
<style type="text/css">
    .floatleft {
        float: left;
    }

    .finalist_data {
        width: 430px;
        font-family: Arial, Helvetica, sans-serif;
        padding-left: 20px;
    }

    .boldme {
        font-weight: bold;
        font-family: Arial, Helvetica, sans-serif;
    }

    .finalist_contain {
        clear: both;
        margin-top: 30px;
        height: 245px;
        min-width: 800px;
    }
</style>
<h1>FINALISTS</h1>
<%
    var context = new WContext(this);
    var element = context.Element;
    var competitionId = DataHelper.GetId(element.GetParameterValue("CompetitionId"));
    if (competitionId > 0)
    {
        var finalists = MCComposer.Provider.GetList(competitionId);
        foreach (var finalist in finalists)
        {
            if (finalist.IsActive)
            {
%>
<div class="finalist_contain">
    <% if (!string.IsNullOrEmpty(finalist.PhotoFile))
       { %>
    <img class="floatleft" src="/Content/Parts/MusicCompetition/res/composers/<%= finalist.PhotoFile %>" />
    <% } %>
    <div class="floatleft finalist_data">
        <span class="boldme">Name:</span>&nbsp;<%= finalist.Name %><br />
        <% if (!string.IsNullOrEmpty(finalist.NickName))
           { %>
        <span class="boldme">Nick Name:</span>&nbsp;<%= finalist.NickName %><br />
        <% } %>
        <%--<span class="boldme">Country:</span>Australia<br />--%>
        <% if (!string.IsNullOrEmpty(finalist.Locale))
           { %>
        <span class="boldme">Locale:</span>&nbsp;<%= finalist.Locale %><br />
        <% } %>
        <span class="boldme">Entry:</span>&nbsp;<%= finalist.Entry %><br />
        <% if (!string.IsNullOrEmpty(finalist.Work))
           { %>
        <span class="boldme">Work:</span>&nbsp;<%= finalist.Work %><br />
        <% } %>
        <%--<span class="boldme">Group Involvement:</span> Locale Coordinator<br />--%>
        <% if (!string.IsNullOrEmpty(finalist.Description))
           { %>
        <p><%= finalist.Description %></p>
        <% } %>
    </div>
</div>
<%          }
        }
    } 
%>
<br />
<br />
<br />
<br />
