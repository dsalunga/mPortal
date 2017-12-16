<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCalendar.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.EventCalendar.AdminCalendar" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<style type="text/css">
    /* WebPart: EventCalendar -- Start */

    table.EventCalendar {
    }

    table.EventCalendar-Title {
        /*border-bottom: 2px solid #AAAAAA;     color: #333399;*/
    }

        table.EventCalendar-Title tr td a {
            text-decoration: underline;
            color: #339 !important;
        }

    .EventCalendar-Title tr td {
        font-size: 12pt;
    }

    .Calendar-Day, .Calendar-Day A {
        height: 85px;
        vertical-align: top;
        text-align: left; /* width: 120px; */
        font-weight: bold;
        color: Black;
    }

    td.Calendar-Day, th.Day-Header {
        border: 1px solid #999;
    }

    .Selected-Calendar-Day {
        background-color: #333399;
        color: White;
    }

        .Selected-Calendar-Day A {
            color: White;
            font-weight: bold;
        }

    .Day-Header {
        font-size: 9pt;
        font-weight: bold; /* width: 120px; */
    }

    .Today-Day /*, .Today-Day A */ {
        background-color: #CCCCCC;
        color: Black;
    }

    .Other-Month-Day, .Other-Month-Day A {
        color: #999999;
    }

    div.Event-List-Item {
        font-weight: normal; /* background-color: Yellow; */
        margin-bottom: 4px;
        margin-top: 2px;
        padding: 2px;
        border: 1px solid #CCCCCC;
    }

        div.Event-List-Item A {
            /* background-color: Yellow; */
        }

    .Selected-Calendar-Day DIV.Event-List-Item {
        font-weight: normal;
        background-color: red;
        margin-top: 2px;
        margin-bottom: 4px;
        padding: 2px;
    }

        .Selected-Calendar-Day DIV.Event-List-Item A {
            /* background-color: red; */
        }


    .Event-List-Item A {
        font-weight: normal;
        text-decoration: none;
        font-weight: bold;
    }

        .Event-List-Item A:hover {
            text-decoration: underline;
        }

    /* WebPart: EventCalendar -- End */
</style>
<table width="100%" style="margin-top: 5px">
    <tr>
        <td colspan="2">
            <div>
                <div>
                    <asp:Button ID="cmdAddEvent" CssClass="btn btn-default btn-sm1" runat="server" Text="New Event" OnClick="cmdAddEvent_Click" />
                    <div style="float: right">
                        Calendar:&nbsp;<asp:DropDownList ID="cboCalendar" CssClass="input" DataTextField="Name" DataValueField="Id"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboCalendar_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="no-bottom-margin">
                    <asp:Button ID="cmdPreviousYear" CssClass="btn btn-default btn-sm" runat="server" Text="&lt;&lt;" OnClick="cmdPreviousYear_Click"
                        Visible="false" /><asp:Button ID="cmdPrevious" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdPrevious_Click"
                            Text="&lt;&lt;" />
                    <asp:DropDownList ID="cboMonth" runat="server" CssClass="input" AutoPostBack="True" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cboYear" runat="server" CssClass="input" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="cmdNext" runat="server" CssClass="btn btn-default btn-sm" OnClick="cmdNext_Click" Text="&gt;&gt;" /><asp:Button
                        ID="cmdNextYear" runat="server" Text="&gt;&gt;" Visible="false" OnClick="cmdNextYear_Click" />
                    &nbsp;
                    <asp:Button ID="cmdToday" runat="server" Text="Today" CssClass="btn btn-default btn-sm" OnClick="cmdToday_Click" />
                </div>
            </div>
            <br />
            <uc1:TabControl ID="tabView" runat="server" />
            <asp:MultiView ID="mvViews" runat="server" ActiveViewIndex="0">
                <asp:View runat="server" ID="viewCalendar">
                    <asp:Calendar ID="monthCalendar" runat="server" CssClass="EventCalendar" BorderWidth="1px"
                        Font-Names="Verdana" Font-Size="9pt" NextPrevFormat="FullMonth" ShowGridLines="True"
                        OnSelectionChanged="Calendar1_SelectionChanged" FirstDayOfWeek="Sunday" OnDayRender="monthCalendar_DayRender"
                        OnVisibleMonthChanged="monthCalendar_VisibleMonthChanged" EnableTheming="False">
                        <SelectedDayStyle CssClass="Calendar-Day Selected-Calendar-Day" BackColor="#3366cc" />
                        <TodayDayStyle CssClass="Calendar-Day Today-Day" />
                        <OtherMonthDayStyle CssClass="Calendar-Day Other-Month-Day" />
                        <DayStyle CssClass="Calendar-Day" Width="120px" />
                        <NextPrevStyle Font-Bold="True" Font-Size="9pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <DayHeaderStyle Font-Bold="True" CssClass="Day-Header" />
                        <TitleStyle BorderColor="#BBBBBB" BorderWidth="1px" Font-Bold="True" Font-Size="12pt"
                            ForeColor="#333399" CssClass="EventCalendar-Title" />
                    </asp:Calendar>
                </asp:View>
                <asp:View runat="server" ID="viewAllEvents">
                    Show all events for:&nbsp;<asp:RadioButton ID="chkMonth" CssClass="aspnet-radio" Text="Current Month" Checked="true"
                        runat="server" AutoPostBack="True" OnCheckedChanged="chkMonth_CheckedChanged"
                        GroupName="EventList" />
                    &nbsp;<asp:RadioButton ID="chkYear" CssClass="aspnet-radio" Text="Current Year" runat="server" AutoPostBack="True"
                        OnCheckedChanged="chkYear_CheckedChanged" GroupName="EventList" />
                    <br />
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333" GridLines="None"
                            Width="100%" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false"
                            PageSize="5">
                            <PagerSettings PageButtonCount="50" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle HorizontalAlign="Center" Width="18px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Custom_Edit" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif"
                                            AlternateText="Edit" CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Subject" HeaderStyle-HorizontalAlign="Left" HeaderText="Subject"
                                    SortExpression="Subject" />
                                <asp:BoundField DataField="FinalLocation" HeaderStyle-HorizontalAlign="Left" HeaderText="Location"
                                    SortExpression="FinalLocation" />
                                <asp:BoundField DataField="StartDate" HeaderStyle-HorizontalAlign="Left" HeaderText="Start"
                                    SortExpression="StartDate" />
                                <asp:BoundField DataField="EndDate" HeaderStyle-HorizontalAlign="Left" HeaderText="End"
                                    SortExpression="EndDate" />
                                <asp:BoundField DataField="CategoryName" HeaderStyle-HorizontalAlign="Left" HeaderText="Category"
                                    SortExpression="CategoryName" />
                                <asp:BoundField DataField="Recurrence" HeaderStyle-HorizontalAlign="Left" HeaderText="Recurrence"
                                    SortExpression="Recurrence" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
