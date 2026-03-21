<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountInfo.ascx.cs"
    Inherits="WCMS.WebSystem.Apps.Integration.Account.UserProfileDetails" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<div class="container">
    <div class="row">
        <% if (IsCurrentUser)
           { %>
        <div class="col-xs-12 col-sm-3" id="sidebar" role="navigation">
            <div class="list-group">
                <a href="/Account/" class="list-group-item active"><strong><span class="glyphicon glyphicon-user" aria-hidden="true"></span>&nbsp;My Account</strong></a>
                <a href="/Account/UpdateInfo" class="list-group-item"><span class="glyphicon glyphicon-edit" aria-hidden="true"></span>&nbsp;Update Profile</a>
                <a href="/Account/UpdatePhoto" class="list-group-item"><span class="glyphicon glyphicon-picture" aria-hidden="true"></span>&nbsp;Update Photo</a>
                <a href="/Account/ChangePassword" class="list-group-item"><span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>&nbsp;Change Password</a>
            </div>
            <div class="list-group">
                <% if (IsExternalLinked)
                   { %>
                <a href="/Account/Attendance" class="list-group-item"><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>&nbsp;My Attendance</a>
                <% } %>
                <%--<a href="/Account/Membership" class="list-group-item">My Group Membership</a>--%>
            </div>
        </div>
        <% } %>
        <div class="col-xs-12 col-sm-9">
            <div class="member-details">
                <h1 runat="server" id="lblHeader" style="margin-top: 0" class="page-header">Member Profile</h1>
                <asp:HiddenField ID="hMemberLinkId" runat="server" Value="-1" />
                <div runat="server" visible="false">
                    <uc1:tabcontrol id="TabControl1" visible="false" selectedindex="0" onselectedtabchanged="TabControl1_SelectedTabChanged"
                        runat="server" />
                </div>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="viewProfile" runat="server">
                        <div>
                            <a runat="server" id="linkUpdatePhoto" title="Update Profile Photo" href="/Account/UpdatePhoto">
                                <img src="/Content/Assets/Images/nophoto.png" class="img-responsive" width="300" runat="server" id="memberPhoto" style="border: solid 2px #aaa; margin: 2px 0 2px 0; display: inline" /></a>
                        </div>
                        <% if (IsCurrentUser) { %>
                        <span class="label label-info">Tap your photo to update</span>
                        <% } %>
                        <br />
                        <br />
                        <div class="col-one" style="margin-top: 5px;" id="panelSendMessage" runat="server"
                            visible="false">
                            <h3>
                                <a id="linkSendMessage" runat="server">Send <span id="lblFirstName" runat="server"></span>&nbsp;a Message </a>
                            </h3>
                            <br />
                        </div>
                        <div>
                            Last updated on <strong runat="server" id="lblLastUpdate">&lt;UNKNOWN&gt;</strong>
                        </div>
                        <div id="panelStatusText" runat="server" visible="false">
                            <br />
                            <div style="padding: 3px; background-color: Yellow; display: inline; font-size: larger">
                                <span id="lblStatusText" runat="server"></span>
                            </div>
                        </div>
                        <br />
                        <div class="FieldLine">
                            <span class="FieldLabel">Name</span> <strong runat="server" id="lblFullName"></strong>
                        </div>
                        <div class="FieldLine" id="panelNickname" runat="server" visible="false">
                            <span class="FieldLabel">Nickname</span> <strong runat="server" id="lblNickname"></strong>
                        </div>
                        <div class="FieldLine" runat="server" id="panelUserName" visible="false">
                            <span class="FieldLabel">User Name</span> <strong runat="server" id="lblUserName"></strong>
                        </div>
                        <div class="FieldLine">
                            <span class="FieldLabel">Group ID</span> <strong runat="server" id="lblExternalIdNo"></strong>
                        </div>
                        <div class="FieldLine">
                            <span class="FieldLabel">Date of Membership</span> <strong runat="server" id="lblMembershipDate"></strong>
                        </div>
                        <div class="FieldLine" id="fieldLocaleCountry" runat="server" visible="false">
                            <span class="FieldLabel">Locale Country</span> <strong id="lblLocaleCountry" runat="server"></strong>
                        </div>
                        <% if (/*ViewerIsManager &&*/ !string.IsNullOrEmpty(UserLocale))
                           { %>
                        <div class="FieldLine">
                            <span class="FieldLabel">Locale Chapter</span> <strong><%= UserLocale %></strong>
                        </div>
                        <% } %>
                        <div style="float: left; clear: both">
                            <div runat="server" id="panelPersonalAndWorkInfo" visible="false">
                                <!-- # start of Personal Information # -->
                                <h3 class="page-header">Contact</h3>
                                <div class="FieldLine" runat="server" id="panelEmail" visible="false">
                                    <span class="FieldLabel">Email</span> <strong runat="server" id="lblEmail"></strong>
                                </div>
                                <div class="FieldLine" runat="server" id="panelEmail2" visible="false">
                                    <span class="FieldLabel">Email (Secondary)</span> <strong runat="server" id="lblEmail2"></strong>
                                </div>
                                <div class="FieldLine" id="panelMobile" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtMobile" runat="server">Mobile Number</asp:Label>
                                    <span id="txtMobile" runat="server" style="font-weight: bold"></span>
                                </div>
                                <div class="FieldLine" id="panelHomePhone" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="txtHomePhone" runat="server">Home Phone</asp:Label>
                                    <span id="txtHomePhone" runat="server" style="font-weight: bold"></span>
                                </div>
                                <br />
                                <div class="FieldLine" id="panelHomeAddressLine1" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtHomeAddress1"
                                        runat="server">Address Line 1</asp:Label>
                                    <span id="txtHomeAddress1" runat="server" style="font-weight: bold"></span>
                                </div>
                                <div class="FieldLine" id="panelHomeAddressLine2" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label13" AssociatedControlID="txtHomeAddress2"
                                        runat="server">Line 2</asp:Label>
                                    <span id="txtHomeAddress2" runat="server" style="font-weight: bold"></span>
                                </div>
                                <div class="FieldLine" id="divHomeState" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label2" AssociatedControlID="txtHomeAddressState"
                                        runat="server">State (US Only)</asp:Label>
                                    <span id="txtHomeAddressState" runat="server" style="font-weight: bold"></span>
                                </div>
                                <div class="FieldLine" id="panelHomeZipCode" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label4" AssociatedControlID="txtHomeAddressZipCode"
                                        runat="server">Zip Code</asp:Label>
                                    <span id="txtHomeAddressZipCode" runat="server" style="font-weight: bold"></span>
                                </div>
                                <div class="FieldLine" id="panelHomeCountry" runat="server" visible="false">
                                    <asp:Label CssClass="FieldLabel" ID="Label3" AssociatedControlID="txtHomeAddressCountry"
                                        runat="server">Country</asp:Label>
                                    <span id="txtHomeAddressCountry" runat="server" style="font-weight: bold"></span>
                                </div>
                                <h3 class="page-header">Other</h3>
                                <div class="FieldLine" runat="server" id="fieldGender" visible="false">
                                    <span class="FieldLabel">Gender</span> <strong runat="server" id="lblGender"></strong>
                                </div>
                                <div class="FieldLine" runat="server" id="fieldMS" visible="false">
                                    <span class="FieldLabel">Marital Status</span> <strong runat="server" id="lblMS"></strong>
                                </div>
                                <div class="FieldLine" runat="server" id="fieldAccountType" visible="false">
                                    <span class="FieldLabel">Account Type</span> <strong runat="server" id="lblAccountType"></strong>
                                </div>
                                <!-- # Start of Work Information # -->
                                <div runat="server" id="panelWorkInfo">
                                    <h3 class="page-header">Work</h3>
                                    <div class="FieldLine" id="panelWorkAddressLine1" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label7" AssociatedControlID="txtWorkAddress1"
                                            runat="server">Address Line 1</asp:Label>
                                        <span id="txtWorkAddress1" runat="server" style="font-weight: bold"></span>
                                    </div>
                                    <div class="FieldLine" id="panelWorkAddressLine2" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label14" AssociatedControlID="txtWorkAddress2"
                                            runat="server">Line 2</asp:Label>
                                        <span id="txtWorkAddress2" runat="server" style="font-weight: bold"></span>
                                    </div>
                                    <div class="FieldLine" id="divWorkState" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label8" AssociatedControlID="txtWorkAddressState"
                                            runat="server">State (US Only)</asp:Label>
                                        <span id="txtWorkAddressState" runat="server" style="font-weight: bold"></span>
                                    </div>
                                    <div class="FieldLine" id="panelWorkZipCode" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label10" AssociatedControlID="txtWorkAddressZipCode"
                                            runat="server">Zip Code</asp:Label>
                                        <span id="txtWorkAddressZipCode" runat="server" style="font-weight: bold"></span>
                                    </div>
                                    <div class="FieldLine" id="panelWorkCountry" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label9" AssociatedControlID="txtWorkAddressCountry"
                                            runat="server">Country</asp:Label>
                                        <span id="txtWorkAddressCountry" runat="server" style="font-weight: bold">&nbsp;</span>
                                    </div>
                                    <br />
                                    <div class="FieldLine" id="panelWorkDesignation" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label11" AssociatedControlID="txtWorkDesignation"
                                            runat="server">Work Designation</asp:Label>
                                        <span id="txtWorkDesignation" runat="server" style="font-weight: bold">&nbsp;</span>
                                    </div>
                                    <div class="FieldLine" id="panelWorkPhone" runat="server" visible="false">
                                        <asp:Label CssClass="FieldLabel" ID="Label12" AssociatedControlID="txtWorkPhone"
                                            runat="server">Work Phone</asp:Label>
                                        <span id="txtWorkPhone" runat="server" style="font-weight: bold"></span>
                                    </div>
                                </div>
                            </div>
                            <%--<div id="panelLocaleGroups" runat="server" visible="false">
                                <br />
                                <h3 class="page-header">Locale Group</h3>
                                <div style="margin-left: 20px">
                                    <asp:DataList ID="rblLocaleGroups" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <li>
                                                <%# Eval("Name") %></li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>--%>
                            <%--<div runat="server" id="panelGS" visible="false">
                                <h3 class="page-header">GS and AGS Assigned</h3>
                                <div style="margin-left: 20px">
                                    <asp:DataList ID="listGS" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <li>
                                                <%# Eval("Name") %></li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>--%>
                            <div id="panelMinistries" runat="server" visible="false">
                                <h3 class="page-header">Committees &amp; Ministries</h3>
                                <div style="margin-left: 20px">
                                    <asp:DataList ID="cblMinistries" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <li>
                                                <%# Eval("Name") %></li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <div id="panelSpecialGroups" runat="server" visible="false">
                                <h3 class="page-header">Special Groups</h3>
                                <div style="margin-left: 20px">
                                    <asp:DataList ID="cblSpecialGroups" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <li>
                                                <%# Eval("Name") %></li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <div id="panelPrivacy" runat="server" visible="false">
                                <h3 class="page-header">Privacy Settings</h3>
                                <label class="checkbox inline" style="font-weight: normal">
                                    <input runat="server" type="checkbox" disabled="disabled" id="chkPrivate" value="chkPrivate" />
                                    Make my contact information private (only Admin can view)
                                </label>
                            </div>
                            <br />
                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                            <br />
                        </div>
                    </asp:View>
                    <asp:View ID="viewOtherInfo" runat="server">
                        <br />
                        <h3>Additional Information:</h3>
                        <asp:MultiView ID="MultiViewOtherInfo" runat="server" ActiveViewIndex="0">
                            <asp:View ID="viewOtherInfoReadOnly" runat="server">
                                <div runat="server" id="lblOtherInfo" style="font-size: larger">
                                </div>
                                <br />
                                <asp:LinkButton ID="cmdOtherInfoEdit" runat="server" OnClick="cmdOtherInfoEdit_Click"
                                    Font-Bold="true" ToolTip="Edit Additional Information">Edit</asp:LinkButton>
                            </asp:View>
                            <asp:View ID="viewOtherInfoEdit" runat="server">
                                <asp:TextBox ID="txtAdditionalInfo" CssClass="input-xlarge" MaxLength="4000" runat="server" Columns="60"
                                    Rows="7" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary" Text="Update"
                                    OnClick="cmdUpdate_Click" />
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        <br />
                        <asp:Label ID="lblOtherStatus" runat="server" ForeColor="Red"></asp:Label>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
</div>
