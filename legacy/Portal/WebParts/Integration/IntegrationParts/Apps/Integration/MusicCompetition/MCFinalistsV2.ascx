<%@ Control Language="C#" AutoEventWireup="true" ClassName="WCMS.WebSystem.Apps.MusicCompetition.MCFinalistsV2" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<%@ Import Namespace="WCMS.Framework" %>
<%@ Import Namespace="WCMS.Framework.Core" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>
<%@ Import Namespace="WCMS.WebSystem.Apps.Integration" %>

<script runat="server">

    public string DoneUrl { get; set; }
    public string ResultsUrl { get; set; }

    public string EventInfoContent { get; set; }

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }

    public string EventTitle { get; set; }
    public string ShortEventTitle { get; set; }

    public bool HasVoted { get; set; }
    public bool VotingEnded { get; set; }
    public bool HasValidInfo { get; set; }
    public bool IsServiceAccount { get; set; }
    public bool IsJudgeView { get; set; }

    public int CompetitionId { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var context = new WContext(this);
            var element = context.Element;
            var paramObj = context.ParameterizedObject;

            IsJudgeView = DataHelper.GetBool(ParameterizedWebObject.GetValue("IsJudgeView", "0", element, paramObj));
            DoneUrl = ParameterizedWebObject.GetValue("DoneUrl", "/Vote-Wall/", element, paramObj);
            ResultsUrl = ParameterizedWebObject.GetValue("ResultsUrl", "/Vote-Results/", element, paramObj);

            EventTitle = ParameterizedWebObject.GetValue("EventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination", element, paramObj);
            ShortEventTitle = ParameterizedWebObject.GetValue("ShortEventTitle", "ASOP Asia & Oceania District 3 - People's Choice Nomination", element, paramObj);
            EventInfoContent = ParameterizedWebObject.GetValue("EventInfoContent", element, paramObj);

            CompetitionId = DataHelper.GetId(ParameterizedWebObject.GetValue("CompetitionId", element, paramObj));
            hCompetitionId.Value = CompetitionId.ToString();

            var competition = MCCompetition.Provider.Get(CompetitionId);
            VotingEnded = competition.IsVoteLocked;

            var candidateId = context.GetId("finalist");
            if (candidateId > 0)
                hCandidateId.Value = candidateId.ToString();

            if (WSession.Current.IsLoggedIn)
            {
                var user = WSession.Current.User;
                IsServiceAccount = user.IsServiceAccount();
                UserName = user.UserName;

                if (!IsServiceAccount)
                {
                    Mobile = user.MobileNumber;
                    Email = user.Email;
                    FirstName = user.FirstName;
                    LastName = user.LastName;

                    textFirstName.Value = user.FirstName;
                    textLastName.Value = user.LastName;

                    if (MCVote.Provider.GetByEmail(CompetitionId, user.Email, 1) != null)
                        HasVoted = true;

                    portalStatus.InnerHtml = string.Format(loggedInTemplate.InnerHtml, user.FirstAndLastName, profileUrlTemplate.InnerHtml);
                }
            }
        }
    }
</script>

<script src="//connect.facebook.net/en_US/all.js" id="facebook-jssdk" type="text/javascript"></script>
<script type="text/javascript">
    var hasVoted = <%= HasVoted ? "true" : "false" %>;
    var voteOption = 0;
    var userName = '<%= UserName %>';
    var firstName = '<%= FirstName %>';
    var lastName = '<%= LastName %>';
    var mobile = '<%= Mobile %>';
    var email = '<%= Email %>';
    var doneUrl = '<%= DoneUrl %>'; //"/Public/ASOP-Vote-Wall.aspx";
    var competitionId = -1;
    var resultsUrl = '<%= ResultsUrl %>';
    var votingEnded = <%= VotingEnded ? "true" : "false" %>;
    var defaultPhoto = '<%=IntegrationConstants.ASOPDefaultFinalistPhoto%>';
    var fbChecking = false;
</script>
<script type="text/javascript" src="/Content/Parts/MusicCompetition/res/js/finalists.js"></script>

<style type="text/css">
    .vote-option-icon {
        margin-bottom: 5px;
        /*-moz-box-shadow: rgba(0, 0, 0, 0.5) 0 4px 10px;
        -webkit-box-shadow: rgba(0, 0, 0, 0.5) 0 4px 10px;
        box-shadow: rgba(0, 0, 0, 0.5) 0 4px 10px;
        -moz-border-radius: 10px;
        border-radius: 10px;
        width: 200px;*/
    }

    .vote-option {
        padding-bottom: 50px;
        width: 245px;
        float: left
    }

        .vote-option a {
            cursor: pointer;
        }

    #vote-options.vote_forms {
        background: none;
    }
</style>

<asp:HiddenField ID="hCompetitionId" ClientIDMode="Static" runat="server" Value="-1" />
<asp:HiddenField ID="hCandidateId" ClientIDMode="Static" runat="server" Value="-1" />
<input type="hidden" id="hResourcePath" value="<%=IntegrationConstants.MCBasePath %><%=hCompetitionId.Value %>/" />
<span id="loggedInTemplate" runat="server" style="display: none">(Logged-in as <strong><a target="_blank" href="{1}">{0}</a></strong>)</span>
<span id="profileUrlTemplate" runat="server" style="display: none">/My-Profile/</span>

<div id="fb-root"></div>
<h3>The Finalists</h3>
<div class="finalist_wrap">
    <div class="finalist_section finalist_entries current"<% =(IsJudgeView ? "data-judge='1'" : "") %>>
        <%
            if (CompetitionId > 0)
            {
                int index = 0;
                var finalists = MCCandidate.Provider.GetList(CompetitionId).OrderBy(i => i.Name);
                foreach (var finalist in finalists)
                {
                    if ((index % 4) == 0)
                    { %>
        <div class="finalist_divider">
            <% } %>
            <div class="finalist_pics" data-entrynumber='<%= finalist.Id %>'>
                <img src="<%= IntegrationConstants.MCBasePath %><%=CompetitionId %>/Photos/thumb/<%= finalist.GetPhotoFile() %>" title="<%= finalist.Name %>, <%= finalist.Interpreter %>" style="width: 172px;" />
                <p class="songtitle"><%= finalist.Entry %></p>
                <% if(string.IsNullOrEmpty(finalist.Interpreter) || finalist.Name.Equals(finalist.Interpreter, StringComparison.InvariantCultureIgnoreCase)) { %>
                <p class="finalist_text1">
                    <span class="finalist_text2"><%= finalist.Name %></span><br />
                    Composer &amp; Interpreter
                </p>
                <% } else { %>
                <p class="finalist_text1">
                    <span class="finalist_text2"><%= finalist.Name %></span><br />
                    Composer
                </p>
                <p class="finalist_text1">
                    <span class="finalist_text2"><%= finalist.Interpreter %></span><br />
                    Interpreter
                </p>
                <% } %>
            </div>
            <%      
                    index++;
                    if ((index % 4) == 0 || index == finalists.Count())
                    { %>
        </div>
        <% }
                }
            } 
        %>
    </div>
    <div id="entrydetails" class="finalist_section finalist_entry_section" data-candidate="">
        <p id="close_entry" class="vote_entry" style="color: white; text-align: center; margin-bottom: 0;">Close</p>
        <% if (!VotingEnded && !IsJudgeView)
           { %>
        <p id="vote-entry" class="vote_entry" style="display: none; color: white; text-align: center; margin-bottom: 0; font-size: 16px; font-weight: bold">Click Here to Vote</p>
        <div id="vote-options" class="vote_forms" style="text-align: center; margin: 15px; padding: 15px">
            <h4 class="text-center">Select Your Account</h4>
            <p>&nbsp;</p>
            <div>
                <div class="vote-option">
                    <a class="vote-option-fb">
                        <img class="vote-option-icon" title="Use your Facebook account" src="/Content/Parts/Integration/Assets/images/icon_fb.jpg" /></a>
                    <div id="fbStatus">Facebook</div>
                </div>
                <div class="vote-option">
                    <a class="vote-option-one">
                        <img class="vote-option-icon" title="Use your Integration Ext account" src="/Content/Parts/Integration/Assets/images/icon_one.jpg" /></a>
                    <div>Integration Ext</div>
                </div>
                <div class="vote-option">
                    <a class="vote-option-portal">
                        <img class="vote-option-icon" title="Use your Music Ministry Portal account" src="/Content/Parts/Integration/Assets/images/mm_logo.jpg" /></a>
                    <div id="portalStatus" runat="server">Music Ministry Portal</div>
                </div>
            </div>
        </div>
        <div class="login vote_forms">
            <div class="form_message">Please login using your <span id="login-form-method">Integration Ext or Music Ministry Portal</span> account.</div>
            <p>&nbsp;</p>
            <p>
                <input class="input_text rounded_corner" name="textUsername" id="textUsername" placeholder="Username" value="" />
            </p>
            <p>
                <input class="input_text rounded_corner" type="password" name="textPassword" id="textPassword" placeholder="Password" value="" />
            </p>
            <p>
                <input id="login" class="login_class submit_class" type="button" value="Login" /><input id="login-cancel" class="login_class submit_class" type="button" value="Cancel" />
            </p>
        </div>
        <div class="signup vote_forms">
            <div class="form_message">Please review your details...</div>
            <br />
            <p>
                <input class="input_text rounded_corner" readonly="readonly" type="text" name="textFirstName" id="textFirstName" runat="server" clientidmode="Static" placeholder="First Name" />
            </p>
            <p>
                <input class="input_text rounded_corner" readonly="readonly" type="text" name="textLastName" id="textLastName" runat="server" clientidmode="Static" placeholder="Last Name" />
            </p>
            <p>
                <input class="input_text rounded_corner" readonly="readonly" type="text" name="textEmail" id="textEmail" placeholder="E-mail" value="<%= Email %>" />
            </p>
            <p>
                <input class="input_text rounded_corner" readonly="readonly" type="text" name="textMobile" id="textMobile" placeholder="Mobile Number" value="<%= Mobile %>" />
            </p>
            <p>
                <input id="submitInfo" class="signup_class submit_class " type="button" value="Vote Now!" /><input id="vote-cancel" class="login_class submit_class" type="button" value="Cancel" />
            </p>
        </div>
        <div class="thankyou vote_forms">
            <div class="form_message">Thank you. Your vote has been confirmed.</div>
        </div>
        <div id="panel-voted" class="vote_forms">
            <div class="form_message">You have already voted using your email. To vote again, please use a different email or account. <a id="re-vote" href="#">Click here to log-off and vote again</a>.</div>
        </div>
        <% } %>
        <img id="photo" src="/Content/Assets/ASOP/_finalist.jpg" alt="" title="" width="800" style="padding-top: 10px;" />
        <p class="entry_text1">
            <span id="playTitle" class="entry_text2">Title of Composition</span><br />
            Title
        </p>
        <p class="entry_text1">
            <span id="composer" class="entry_text2">Name of Composer</span><br />
            Composer
        </p>
        <p class="entry_text1">
            <span id="interpreter" class="entry_text2">Name of Interpreter</span><br />
            Interpreter
        </p>

        <div class="play_music">
            <p id="audioControl">Insert sample streaming Composition here</p>
        </div>
        <p class="entry_text2">Lyrics</p>
        <p id="lyricsContent" class="lyrics">
        </p>
    </div>
</div>
