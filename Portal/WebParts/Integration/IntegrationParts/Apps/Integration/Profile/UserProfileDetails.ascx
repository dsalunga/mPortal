<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfileDetails.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Profile.UserProfileDetails" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<div class="member-details">
    <h2 runat="server" id="lblHeader">Member Profile</h2>
    <asp:HiddenField ID="hMemberLinkId" runat="server" Value="-1" />
    <uc1:TabControl ID="TabControl1" Visible="false" SelectedIndex="0" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
        runat="server" />
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewProfile" runat="server">
            <a runat="server" id="linkUpdatePhoto" title="Update Profile Photo">
                <img src="/Content/Assets/Images/nophoto.png" width="300" runat="server" id="memberPhoto" style="border: solid 2px #aaa; margin: 2px 0 2px 0" /></a>
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
            <div class="FieldLine" runat="server" id="panelEmail" visible="false">
                <span class="FieldLabel">Email</span> <strong runat="server" id="lblEmail"></strong>
            </div>
            <div class="FieldLine" runat="server" id="panelEmail2" visible="false">
                <span class="FieldLabel">Email (Secondary)</span> <strong runat="server" id="lblEmail2"></strong>
            </div>
            <div class="FieldLine" runat="server" id="panelUserName" visible="false">
                <span class="FieldLabel">User Name</span> <strong runat="server" id="lblUserName"></strong>
            </div>
            <div class="FieldLine">
                <span class="FieldLabel">Group ID #</span> <strong runat="server" id="lblExternalIdNo"></strong>
            </div>
            <div class="FieldLine">
                <span class="FieldLabel">Date of Membership</span> <strong runat="server" id="lblMembershipDate"></strong>
            </div>
            <% if (ViewerIsManager)
               { %>
            <div class="FieldLine">
                <span class="FieldLabel">Locale</span> <strong><%= UserLocale %></strong>
            </div>
            <% } %>
            <div style="float: left; clear: both">
                <div runat="server" id="panelPersonalAndWorkInfo" visible="false">
                    <!-- # start of Personal Information # -->
                    <br />
                    <br />
                    <h3>PERSONAL Information</h3>
                    <div class="FieldLine" id="panelHomeAddressLine1" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" ID="Label1" AssociatedControlID="txtHomeAddress1"
                            runat="server">Address Line 1</asp:Label>
                        <span id="txtHomeAddress1" runat="server" style="font-weight: bold"></span>
                    </div>
                    <div class="FieldLine" id="panelHomeAddressLine2" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" ID="Label13" AssociatedControlID="txtHomeAddress2"
                            runat="server">Address Line 2</asp:Label>
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
                    <br />
                    <div class="FieldLine" id="panelMobile" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" ID="Label5" AssociatedControlID="txtMobile" runat="server">Mobile Number</asp:Label>
                        <span id="txtMobile" runat="server" style="font-weight: bold"></span>
                    </div>
                    <div class="FieldLine" id="panelHomePhone" runat="server" visible="false">
                        <asp:Label CssClass="FieldLabel" ID="Label6" AssociatedControlID="txtHomePhone" runat="server">Home Phone</asp:Label>
                        <span id="txtHomePhone" runat="server" style="font-weight: bold"></span>
                    </div>
                    <br />
                    <br />
                    <!-- # Start of Work Information # -->
                    <div runat="server" id="panelWorkInfo">
                        <h3>WORK Information</h3>
                        <div class="FieldLine" id="panelWorkAddressLine1" runat="server" visible="false">
                            <asp:Label CssClass="FieldLabel" ID="Label7" AssociatedControlID="txtWorkAddress1"
                                runat="server">Address Line 1</asp:Label>
                            <span id="txtWorkAddress1" runat="server" style="font-weight: bold"></span>
                        </div>
                        <div class="FieldLine" id="panelWorkAddressLine2" runat="server" visible="false">
                            <asp:Label CssClass="FieldLabel" ID="Label14" AssociatedControlID="txtWorkAddress2"
                                runat="server">Address Line 2</asp:Label>
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
                    <br />
                    <h3>Locale Group</h3>
                    <div style="margin-left: 20px">
                        <asp:DataList ID="rblLocaleGroups" runat="server" RepeatColumns="1" RepeatDirection="Vertical">
                            <ItemTemplate>
                                <li>
                                    <%# Eval("Name") %></li>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div runat="server" id="panelGS" visible="false">
                    <br />
                    <br />
                    <h3>GS and AGS Assigned</h3>
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
                    <br />
                    <br />
                    <h3>Committees &amp; Ministries</h3>
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
                    <br />
                    <br />
                    <h3>Special Groups</h3>
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
                    <br />
                    <br />
                    <h3>Privacy Settings</h3>
                    <label class="checkbox inline">
                        <input runat="server" type="checkbox" disabled="disabled" id="chkPrivate" value="chkPrivate" />
                        Make my contact information private (only Admin can view)
                    </label>
                </div>
                <%--
            <div>
                <asp:Button CssClass="Command" Width="85px" ID="cmdCancel" runat="server" Text="OK"
                    OnClick="cmdCancel_Click" Visible="False" />
            </div>
                --%>
                <br />
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
