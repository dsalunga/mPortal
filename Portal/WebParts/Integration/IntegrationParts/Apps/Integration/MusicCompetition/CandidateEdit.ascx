<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CandidateEdit.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.MusicCompetition.CandidateEdit" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<asp:HiddenField ID="hCompetitionId" runat="server" Value="" />
<table border="0">
    <tr>
        <td style="vertical-align: top">Song:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEntry"
            ErrorMessage="Song" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td style="vertical-align: top">
            <asp:TextBox ID="txtEntry" runat="server" CssClass="col-md-5 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Composer:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
            ErrorMessage="Composer" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="col-md-4 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Lyricist:
        </td>
        <td>
            <asp:TextBox ID="txtLyricist" runat="server" CssClass="col-md-4 input" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Interpreter:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName"
            ErrorMessage="Interpreter" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtInterpreter" CssClass="col-md-4 input" runat="server" Columns="75"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Audio (.MP3):
        </td>
        <td style="vertical-align: top" class="no-bottom-margin">
            <asp:TextBox ID="txtSourceUrl" placeholder="Audio sample" CssClass="col-md-4 input" runat="server" Columns="100"></asp:TextBox>&nbsp;<a class="btn btn-default btn-sm" href="/Central/Tools/Asset-Manager/?Path=~/ASOP/<%=hCompetitionId.Value %>/Music/" target="_blank">Upload...</a>
            <br />
            <span style="font-size: smaller">Upload the file and enter only the music filename, example: <span style="font-style: italic">finalist-song-title-01.mp3</span></span>
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>Audio2 (.OGG):
        </td>
        <td style="vertical-align: top">
            <asp:TextBox ID="txtSourceUrl2" placeholder="Backup source (for non-compatible browsers)"
                runat="server" Columns="100" CssClass="col-md-6 input"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Photo File:
        </td>
        <td style="vertical-align: top" class="no-bottom-margin">
            <asp:TextBox ID="txtPhotoFile" runat="server" CssClass="col-md-4 input" Columns="75"></asp:TextBox>&nbsp;<a class="btn btn-default btn-sm" href="/Central/Tools/Asset-Manager/?Path=~/ASOP/<%=hCompetitionId.Value %>/Photos/" target="_blank">Upload...</a>
            <br />
            <span style="font-size: smaller">Upload the file and enter only the photo filename, example: <span style="font-style: italic">finalist01.jpg</span></span>
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top">Lyrics:
        </td>
        <td>
            <uc2:TextEditor IsPlainTextDefault="false" EditorToolbarSet="Basic" ID="txtLyrics"
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Rank:
        </td>
        <td>
            <asp:TextBox ID="txtRank" placeholder="For sorting in Judges page"
                runat="server" Columns="50" CssClass="col-md-1 input"></asp:TextBox><span runat="server" id="panelWinnerRank" visible="true">&nbsp;Winner Rank:&nbsp;<asp:TextBox ID="txtWinnerRank" placeholder="For sorting in Winners page"
                    runat="server" Columns="5" CssClass="input"></asp:TextBox></span>
        </td>
    </tr>
    <tr>
        <td>Competition:
        </td>
        <td>
            <asp:DropDownList ID="cboCompetition" AppendDataBoundItems="true" CssClass="input" runat="server" DataTextField="Name" DataValueField="Id">
                <asp:ListItem Text="- None -" Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <asp:Button ID="cmdUpdate" CssClass="btn btn-primary" runat="server" Text="Update"
            OnClick="cmdUpdate_Click" />
        <a href="#" runat="server" class="btn btn-default" id="linkCancel">Cancel</a>
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following are required:"
    ShowMessageBox="True" ShowSummary="False" />
