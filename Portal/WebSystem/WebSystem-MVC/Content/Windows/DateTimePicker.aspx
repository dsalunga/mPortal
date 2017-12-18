<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="DateTimePicker.aspx.cs"
    Inherits="WCMS.WebSystem.Windows.DateTimePicker" %>
<%@ Import Namespace="WCMS.Common.Utilities" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Date/Time Picker</title>
    <link href="<%=WebUtil.Version("~/content/assets/styles/datetimepicker.aspx.min.css")%>" rel="stylesheet" type="text/css"
        media="all" />
    <script src="<%=WebUtil.Version("~/content/assets/scripts/wcms.core.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/datetimepicker.aspx.min.js")%>" type="text/javascript"></script>
    <script src="<%=WebUtil.Version("~/content/assets/scripts/jquery.min.js")%>" type="text/javascript"></script>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <base target="_self" />
</head>
<body onload="Page_Load" runat="server" id="Body">
    <div style="text-align: center; margin-left: auto; margin-right: auto; width: 230px">
        <form id="form1" runat="server">
            <input type="hidden" runat="server" clientidmode="Static" id="hiddenValue" />
            <input type="hidden" runat="server" clientidmode="Static" id="hInline" value="0" />
            <table border="0" cellpadding="0">
                <tr>
                    <td>
                        <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="cmdToday" runat="server" Text="Now" OnClick="cmdToday_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                            CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                            ForeColor="Black" Width="220px" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged"
                            FirstDayOfWeek="Sunday">
                            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="Gray" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        </asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkTime" runat="server" Text="Time:" />&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboHour" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <strong>:</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboMinute" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboSecond" runat="server">
                                        <asp:ListItem Selected="True">AM</asp:ListItem>
                                        <asp:ListItem>PM</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                            <td>
                            </td>
                            <td align="center">
                                HH
                            </td>
                            <td>
                            </td>
                            <td align="center">
                                MM
                            </td>
                        </tr>--%>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br />
                        <asp:Button ID="cmdSubmit" CssClass="large" Width="85px" runat="server" Text="Accept"
                            Font-Bold="true" OnClick="cmdSubmit_Click" />
                        &nbsp;<asp:Button ID="cmdCancel" CssClass="large" Width="85px" Font-Bold="true" OnClientClick="return closeWindow('-1');"
                            runat="server" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
