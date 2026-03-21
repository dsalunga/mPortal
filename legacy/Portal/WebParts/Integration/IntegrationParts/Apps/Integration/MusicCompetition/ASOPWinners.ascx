<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.MusicCompetition.ASOPWinners" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>

<%
    var context = new WContext(this);
    var element = context.Element;

    //var paramObj = context.ParameterizedObject;

    //DoneUrl = paramObj.GetParameterValue("DoneUrl", "/Vote-Wall/");
    //ResultsUrl = paramObj.GetParameterValue("ResultsUrl", "/Vote-Results/");

    var eventTitle = element.GetParameterValue("EventTitle");
    //ShortEventTitle = paramObj.GetParameterValue("ShortEventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination");
    var eventInfoContent = element.GetParameterValue("EventInfoContent");

    var competitionId = DataHelper.GetId(element.GetParameterValue("CompetitionId"));
    MusicCompetition competition = null;
    if (competitionId > 0 && (competition = MCCompetition.Provider.Get(competitionId)) != null)
    {
        var finalists = MCCandidate.Provider.GetList(competitionId).Where(i => i.WinnerRank > 0).OrderBy(i => i.WinnerRank);
        MCCandidate bestInterpreter = competition.BestInterpreterId > 0 ? MCCandidate.Provider.Get(competition.BestInterpreterId) : null;
        MCCandidate peoplesChoice = competition.PeoplesChoiceId > 0 ? MCCandidate.Provider.Get(competition.PeoplesChoiceId) : null;

        if (string.IsNullOrEmpty(eventTitle))
            eventTitle = competition.Name + " Winners";
%>
<style type="text/css">
    .winners {
        margin: 40px auto;
        /*width: 476px;*/
        font-size: 13px;
        text-align: center;
    }

    .finalist_wrap p {
        line-height: normal;
        text-align: center;
    }
</style>
<div class="finalist_wrap" style="color: #111;">
    <div style="color: black;">
        <p style="font-size: 20px;"><%=eventTitle %><%--ASOP Asia Oceania District 3 Winners--%></p>
        <p><%=eventInfoContent %><%--District 3 Official Entries for ASOP Asia Oceania 2013 Division Eliminations--%></p>
    </div>
    <%
        if (finalists.Count() > 0)
        {
            foreach (var finalist in finalists)
            {
    %>
    <div class="winners">
        <img src="<%=IntegrationConstants.MCBasePath %><%=competitionId %>/Photos/winners/<%= finalist.GetPhotoFile() %>" title="<%= finalist.Name %>, <%= finalist.Interpreter %>" /><br />
        <p>
            <span style="font-size: larger"><%= finalist.Entry %></span>
            <span style="font-family: Arial, Helvetica, sans-serif;">
                <br />
                <%= finalist.Name %> - <strong>Composer</strong>
                <br />
                <%= finalist.Interpreter %> - <strong>Interpreter</strong></span>
        </p>
    </div>
    <%      
            }
        }

        if (peoplesChoice != null)
        {
    %>
    <div class="winners">
        <img src="<%=IntegrationConstants.MCBasePath %><%=competitionId %>/Photos/winners/<%= peoplesChoice.GetPhotoFile() %>" title="<%= peoplesChoice.Name %>, <%= peoplesChoice.Interpreter %>" /><br />
        <p>
            <span style="font-size: larger">People's Choice</span><br />
            <% =peoplesChoice.Entry %>
            <span style="font-family: Arial, Helvetica, sans-serif;">
                <br />
                <% =peoplesChoice.Name %> - <strong>Composer</strong>
                <br />
                <% =peoplesChoice.Interpreter %> - <strong>Interpreter</strong></span>
        </p>
    </div>
    <% }
        if (bestInterpreter != null)
        { %>
    <div class="winners">
        <img src="<%=IntegrationConstants.MCBasePath %><%=competitionId %>/Photos/winners/interpreter-<%= bestInterpreter.GetPhotoFile() %>" title="<%= bestInterpreter.Name %>, <%= bestInterpreter.Interpreter %>" /><br />
        <p>
            <span style="font-size: larger">Best Interpreter</span><br />
            <span style="font-family: Arial, Helvetica, sans-serif;"><strong><%=bestInterpreter.Interpreter %></strong> - <%=bestInterpreter.Entry %></span>
        </p>
    </div>
    <% } %>
</div>
<% } %>
