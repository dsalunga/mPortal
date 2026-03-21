<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAttendance.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.MyAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:HiddenField ID="hStartDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hEndDate" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hMemberId" runat="server" Value="-1" ClientIDMode="Static" />
<asp:HiddenField ID="hMakeUpUrl" runat="server" Value="" ClientIDMode="Static" />
<asp:HiddenField ID="hGridItemFormat" runat="server" Value="" ClientIDMode="Static" />
<div class="attendance-makeup-calendar no-bottom-margin" style="max-width: 750px;">
    <div style="margin-bottom: 5px;">
        <asp:RadioButton CssClass="aspnet-radio" ID="radioMonth" GroupName="Attendance" Text="Month:" runat="server"
            Checked="True" />&nbsp;<asp:Button ID="cmdPrevious" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdPrevious_Click"
                Text="&lt;&lt;" Visible="True" />
        <asp:DropDownList ID="cboMonth" runat="server" CssClass="input" OnSelectedIndexChanged="cboMonth_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:DropDownList ID="cboYear" runat="server" CssClass="input" OnSelectedIndexChanged="cboYear_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="cmdNext" CssClass="btn btn-default btn-sm" runat="server" OnClick="cmdNext_Click" Text="&gt;&gt;" Visible="True" />
        &nbsp;
    <asp:Button ID="cmdToday" CssClass="btn btn-default btn-sm" runat="server" Text="Today" OnClick="cmdToday_Click" Visible="True" />
    </div>
    <div>
        <asp:RadioButton ID="radioRange" CssClass="aspnet-radio" GroupName="Attendance" Text="Range:" runat="server" />&nbsp;
    <asp:TextBox ID="txtFromDate" Columns="20" CssClass="span2 input" runat="server" ClientIDMode="Static"></asp:TextBox>
        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" ClientIDMode="Static"
            Enabled="True" TargetControlID="txtFromDate" Format="yyyy-MMM-dd" Animated="False"
            DefaultView="Months">
        </cc1:CalendarExtender>
        &nbsp;to date&nbsp;
    <asp:TextBox ID="txtToDate" Columns="20" CssClass="span2 input" runat="server" ClientIDMode="Static"></asp:TextBox>
        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" ClientIDMode="Static"
            Enabled="True" TargetControlID="txtToDate" Format="yyyy-MMM-dd" Animated="False"
            DefaultView="Months">
        </cc1:CalendarExtender>
    </div>
    <br />
    <div>
        <div style="float: left">
            <asp:Button ID="cmdShowAttendance" CssClass="btn btn-primary" Width="60px" runat="server" Text="Go" OnClick="cmdShowAttendance_Click" />&nbsp;<asp:Label
                runat="server" ID="lblStatus" ForeColor="#e13056"></asp:Label>
        </div>
        <div style="float: right">
            <h3 runat="server" id="lblName" style="margin-top: 10px"></h3>
        </div>
    </div>
    <div>
        <br />
        <br />
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="viewMonth" runat="server">
                <div class="table-responsive">
                    <asp:Calendar CssClass="Calendar-Table" ID="monthCalendar" runat="server" BackColor="White"
                        BorderColor="White" BorderWidth="1px" EnableTheming="False" FirstDayOfWeek="Sunday"
                        Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth"
                        OnDayRender="monthCalendar_DayRender" OnVisibleMonthChanged="monthCalendar_VisibleMonthChanged"
                        ShowGridLines="True" Width="100%">
                        <SelectedDayStyle BackColor="#3366cc" CssClass="Calendar-Day Selected-Calendar-Day" />
                        <TodayDayStyle CssClass="Calendar-Day Today-Day" />
                        <OtherMonthDayStyle CssClass="Calendar-Day Other-Month-Day" />
                        <DayStyle CssClass="Calendar-Day" Width="120px" />
                        <NextPrevStyle Font-Bold="True" Font-Size="9pt" ForeColor="#ffffff" VerticalAlign="Bottom" />
                        <DayHeaderStyle CssClass="Day-Header" Font-Bold="True" />
                        <TitleStyle BorderWidth="1px" BorderColor="#BBBBBB" BackColor="#565656" CssClass="EventCalendar-Calendar-Title"
                            Font-Bold="True" Font-Size="12pt" ForeColor="#ffffff" />
                    </asp:Calendar>
                </div>
                <br />
                <div>
                    <div style="float: left; width: 80px">
                        <div style="width: 15px; height: 15px; background-color: #25EF2B; float: left; margin-top: 2px; margin-right: 4px;">
                        </div>
                        On-Time
                    </div>
                    <div style="float: left; width: 80px">
                        <div style="width: 15px; height: 15px; background-color: #B4E370; float: left; margin-top: 2px; margin-right: 4px;">
                        </div>
                        Make-Up
                    </div>
                    <div style="float: left; width: 80px">
                        <div style="width: 15px; height: 15px; background-color: #ffff00; float: left; margin-top: 2px; margin-right: 4px;">
                        </div>
                        Late
                    </div>
                    <div style="float: left; width: 80px">
                        <div style="width: 15px; height: 15px; background-color: #DA4D4D; float: left; margin-top: 2px; margin-right: 4px;">
                        </div>
                        Absent
                    </div>
                    <div style="float: left;">
                        <div style="width: 15px; height: 15px; background-color: #0000FF; float: left; margin-top: 2px; margin-right: 4px;">
                        </div>
                        Make-Up: Pending Approval
                    </div>
                </div>
            </asp:View>
            <asp:View ID="viewRange" runat="server">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="AttendanceID" DataSourceID="ObjectDataSource1"
                        ForeColor="#333333" GridLines="None" PageSize="25" Width="100%" ClientIDMode="Static"
                        CssClass="table table-condensed table-borderless" EmptyDataText="No attendance data found.">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="WeekNo" HeaderStyle-HorizontalAlign="Left" HeaderText="Week No"
                                SortExpression="WeekNo" />
                            <asp:BoundField DataField="ServiceType" HeaderStyle-HorizontalAlign="Left" HeaderText="Service"
                                SortExpression="ServiceType" HtmlEncode="false" />
                            <asp:BoundField DataField="ServiceDateTime" HeaderStyle-HorizontalAlign="Left" HeaderText="Service Date/Time"
                                SortExpression="ServiceDateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}" HtmlEncode="false" />
                            <asp:BoundField DataField="DateTimeIn" HeaderStyle-HorizontalAlign="Left" HeaderText="Date/Time In"
                                SortExpression="DateTimeIn" />
                            <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" HeaderText="Status"
                                SortExpression="Status" />
                            <asp:BoundField DataField="Remarks" HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks"
                                SortExpression="Remarks" />
                        </Columns>
                        <RowStyle BackColor="#F5F5E6" />
                        <EditRowStyle BackColor="#2461BF" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#5C5247" ForeColor="White" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#5C5247" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerSettings PageButtonCount="25" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                        TypeName="WCMS.WebSystem.WebParts.Profile.MyAttendance" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hMemberId" DefaultValue="-1" Name="memberId" PropertyName="Value"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="hStartDate" DefaultValue="" Name="startDate" PropertyName="Value"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="hEndDate" Name="endDate" PropertyName="Value" Type="DateTime" />
                            <asp:ControlParameter ControlID="hMakeUpUrl" DefaultValue="" Name="makeUpUrl" PropertyName="Value"
                                Type="String" />
                            <asp:ControlParameter ControlID="hGridItemFormat" DefaultValue="" Name="gridItemFormat"
                                PropertyName="Value" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</div>