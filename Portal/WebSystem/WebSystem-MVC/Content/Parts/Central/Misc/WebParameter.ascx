<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebParameter.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.WebParameterController" %>
<%@ Register Src="~/Content/Controls/TextEditor.ascx" TagName="TextEditor" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<h1 class="central page-header">
    New Parameter
</h1>
<table width="100%">
    <tr>
        <td width="125">Name:<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="cboName"
            ErrorMessage="Name" SetFocusOnError="True" ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:ComboBox ID="cboName" runat="server" Width="500px" AutoPostBack="True" AutoCompleteMode="None"
                OnSelectedIndexChanged="cboName_SelectedIndexChanged" DropDownStyle="Simple">
            </asp:ComboBox>
            <br />
        </td>
    </tr>
    <tr>
        <td valign="top">Value:
        </td>
        <td>
            <uc2:TextEditor ID="txtValue" runat="server" />
            <%--<asp:TextBox ID="txtValue" runat="server" Rows="10" TextMode="MultiLine" Width="500px"></asp:TextBox>--%>
            <br />
            <asp:CheckBox ID="chkRequired" CssClass="aspnet-checkbox" Text="Required" runat="server" />
        </td>
    </tr>
</table>
<div class="control-box">
    <div>
        <div class="btn-group">
            <button type="submit" id="cmdUpdate" class="btn btn-default" runat="server"
                onserverclick="cmdUpdate_Click" name="cmdUpdate">
                Update</button>
            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                <span class="caret"></span>
                <span class="sr-only">Toggle Dropdown</span>
            </button>
            <ul class="dropdown-menu" role="menu">
                <li>
                    <asp:LinkButton ID="cmdUpdateAndAddNew"
                        runat="server" Text="Update & Add New" OnClick="cmdUpdateAndAddNew_Click" ToolTip="Update & Add New"></asp:LinkButton></li>
            </ul>
        </div>
        <asp:Button ID="cmdCancel" runat="server" CssClass="btn btn-default" Text="Cancel" OnClick="cmdCancel_Click"
            CausesValidation="False" />
    </div>
</div>
