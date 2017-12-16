<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.ascx.cs" Inherits="WCMS.WebSystem.WebParts.Messaging.SendEmail" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<style type="text/css">
    #rblSendVia label {
        margin-right: 10px;
        font-weight: bold;
    }

    #radioSendAs label {
        margin-right: 10px;
    }

    #gridRecipients tbody tr td {
        border-color: #ccc;
    }
</style>
<asp:HiddenField runat="server" ID="hMaxSMS" Value="-1" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hRecipients" Value="" />
<asp:HiddenField runat="server" ID="hExcluded" Value="" />
<asp:HiddenField runat="server" ID="hView" Value="" />
<asp:HiddenField runat="server" ID="hFullControl" Value="0" />
<asp:HiddenField runat="server" ID="hSingleRecipient" Value="" />
<asp:HiddenField runat="server" ID="hEmailSubject" Value="" />
<asp:HiddenField runat="server" ID="hBaseGroup" ClientIDMode="Static" Value="" />
<div runat="server" id="panelSend" clientidmode="static" class="no-bottom-margin">
    <div style="font-size: larger">
        Send Message Via:&nbsp;<asp:RadioButtonList ID="rblSendVia" CssClass="aspnet-radio" runat="server" RepeatDirection="Horizontal"
            RepeatLayout="Flow" AutoPostBack="True" ClientIDMode="Static" OnSelectedIndexChanged="rblSendVia_SelectedIndexChanged"
            Font-Bold="False">
            <asp:ListItem Selected="True" Value="1">SMS</asp:ListItem>
            <asp:ListItem Value="0">E-mail</asp:ListItem>
            <asp:ListItem Value="2">SMS & E-mail</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <br />
    <br />
    <span id="panelRecipientMultiple" runat="server">
        <h3 style="margin-bottom: 5px;">Send To
        </h3>
        <div style="padding-bottom: 5px;">
            <asp:TextBox ID="txtAdd" runat="server" CssClass="input" ClientIDMode="Static" Columns="70"></asp:TextBox>
            <asp:Button ID="cmdAdd" runat="server" CssClass="btn btn-default btn-sm" ClientIDMode="Static" OnClientClick="return Add_Click();"
                Text="Add..." OnClick="cmdAdd_Click" />
            <asp:Button ID="cmdReset" runat="server" CssClass="btn btn-default btn-sm" Text="Reset" OnClick="cmdReset_Click" />
        </div>
        <div id="panelRecipientView" runat="server" visible="false" style="margin: 5px 0px 3px 0px">
            View recipients by:&nbsp;<span style="font-weight: bold" id="lblCurrentView" runat="server">
            </span>&nbsp;(<asp:LinkButton ID="cmdChangeView" runat="server" OnClick="cmdChangeView_Click"></asp:LinkButton>)
        </div>
        <asp:GridView ClientIDMode="Static" CssClass="gridview-ctrl" ID="gridRecipients" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="2" DataKeyNames="Id" DataSourceID="ObjectDataSource1"
            ForeColor="#333333" Width="95%" OnRowCommand="GridView1_RowCommand" PageSize="7"
            EmptyDataText="<span style='background-color:yellow;'>There are no recipients. Please select recipients by clicking the Add button.</span>"
            AllowPaging="True" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="Solid">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="MobileNumber" HeaderText="Mobile" SortExpression="MobileNumber"
                    HeaderStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <HeaderStyle HorizontalAlign="center" Width="20px" />
                    <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" CommandName="Custom_Delete" runat="server" ImageUrl="~/Content/Assets/Images/Common/ico_x.gif"
                            CommandArgument='<%# Eval("Id") %>' ToolTip="Remove" OnClientClick="return confirm('Are you you want to remove this item?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="White" />
            <PagerSettings PageButtonCount="25" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetRecipients"
            TypeName="WCMS.WebSystem.WebParts.Messaging.SendEmail">
            <SelectParameters>
                <asp:ControlParameter Name="view" ControlID="hView" PropertyName="Value" Type="String" />
                <asp:ControlParameter ControlID="hRecipients" Name="customRecipients" PropertyName="Value"
                    Type="String" />
                <asp:ControlParameter ControlID="hExcluded" Name="exclude" PropertyName="Value" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
    </span><span id="panelRecipientSingle" runat="server" visible="false">
        <h3 style="margin-bottom: 5px;">Send To
        </h3>
        <div style="background-color: Yellow; font-size: larger">
            <span id="lblToSingle" runat="server"></span>
        </div>
        <br />
    </span>
    <div runat="server" id="panelSMSMessage" visible="false">
        <br />
        <h3 style="margin-bottom: 5px">SMS Message</h3>
        <div>
            <asp:TextBox ID="txtSMSMessage" ClientIDMode="Static" Width="95%" runat="server"
                TextMode="MultiLine" Rows="10" MaxLength="1000"></asp:TextBox>
            <div>
                <div class="pull-left" id="panelSMSInfo">
                </div>
                <div class="pull-right">
                    <asp:CheckBox ID="chkSendSMSCopy" CssClass="aspnet-checkbox" Text="Send me a copy" runat="server" />
                </div>
            </div>
        </div>
        <br />
    </div>
    <div runat="server" id="panelEmailMessage" visible="false">
        <br />
        <h3 style="margin-bottom: 10px">E-mail Message</h3>
        <div style="font-size: larger">
            Subject:
        </div>
        <asp:TextBox ID="txtSubject" Width="95%" runat="server" Columns="100"></asp:TextBox>
        <br />
        <br />
        <div style="font-size: larger">
            Message:
        </div>
        <div>
            <FCKeditorV2:FCKeditor ID="txtMessage" ToolbarSet="Email" runat="server" Height="400px"
                Width="100%">
            </FCKeditorV2:FCKeditor>
        </div>
        <span id="panelSendAs" runat="server">
            <br />
            <div style="font-size: larger">
                Send As:
            </div>
            <div>
                <asp:RadioButtonList ID="radioSendAs" CssClass="aspnet-radio" ToolTip="NOTE: When 'To (Group Email)' is selected, meta-tags like $(FIRST_NAME) will not work. Meta-tags apply only to Individual Emails." runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="0">To (Individual Email)</asp:ListItem>
                    <asp:ListItem Value="1">To (Group Email)</asp:ListItem>
                    <asp:ListItem Value="2">BCC</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </span>
    </div>
    <p id="lblStatus" runat="server" style="color: Red;">
    </p>
    <div class="control-box">
        <div>
            <asp:Button ID="cmdSend" runat="server" CssClass="btn btn-primary" Text="Send"
                OnClick="cmdSend_Click" />&nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel"
                    CssClass="btn btn-default" OnClick="cmdCancel_Click" Visible="false" />
        </div>
    </div>
</div>
<div id="panelSent" runat="server" visible="false">
    <h3>Your message has been sent.</h3>
</div>
<script type="text/javascript">
    function Add_Click() {
        var addValue = $("#txtAdd").val().Trim();
        var baseGroup = $("#hBaseGroup").val();

        if (addValue == "") {
            ShowAccountBrowser("txtAdd", -1, 1, 1, 1, "cmdAdd", baseGroup);
            return false;
        }

        return true;
    }

    $(document).ready(function () {
        var textBoxSMS = $("#txtSMSMessage");
        var divSMSinfo = $("#panelSMSInfo");
        if (textBoxSMS.length > 0 && divSMSinfo.length > 0) {
            var maxSMS = parseInt($("#hMaxSMS").val());

            textBoxSMS.keyup(function () {
                UpdateSMSCounter(textBoxSMS[0], divSMSinfo[0], maxSMS);
            });

            textBoxSMS.change(function () {
                UpdateSMSCounter(textBoxSMS[0], divSMSinfo[0], maxSMS);
            });

            UpdateSMSCounter(textBoxSMS[0], divSMSinfo[0], maxSMS);
        }
    });
</script>
