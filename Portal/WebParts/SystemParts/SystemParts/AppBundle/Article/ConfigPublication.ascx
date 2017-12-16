<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.WebParts.Article.ConfigArticleController"
    CodeBehind="ConfigPublication.ascx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<%@ Register Src="../Central/Controls/WebSiteElementSelector.ascx" TagName="WebSiteElementSelector"
    TagPrefix="uc2" %>
<%@ Register Src="../Central/Controls/SaveInFolder.ascx" TagName="SaveInFolder" TagPrefix="uc3" %>
<%@ Register Src="Controls/AdminTabControl.ascx" TagName="AdminTabControl" TagPrefix="uc4" %>
<asp:HiddenField ID="hidObjectId" runat="server" Value="-1" />
<asp:HiddenField ID="hidRecordId" runat="server" Value="-1" />
<div>
    <uc4:AdminTabControl ID="AdminTabControl1" runat="server" />
</div>
<asp:MultiView ID="mtvInstance" runat="server" ActiveViewIndex="0">
    <asp:View ID="viwContent" runat="server">
        <asp:Panel ID="pnlContent" runat="server" Width="100%">
            <asp:MultiView ID="mtvItems" runat="server" ActiveViewIndex="0">
                <asp:View ID="viwInserted" runat="server">
                    <div class="control-box">
                        <div>
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-default" Text="New" Width="85px" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnRemove" runat="server" CssClass="btn btn-default" Text="Remove" OnClick="btnRemove_Click"
                                OnClientClick="return confirm('Remove selected item(s)?');" />
                            <div class="pull-right" id="panelInsertExisting" runat="server">
                                <asp:Button ID="btnExisting" runat="server" Text="Insert..." OnClick="btnExisting_Click" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <!-- List Items / Visible -->
                        <asp:GridView ID="grvContent" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                            OnRowCommand="grvContent_RowCommand" PageSize="15" DataSourceID="ObjectDataSourceInsertedArticles"
                            AllowSorting="True">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerSettings PageButtonCount="35" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Id">
                                    <HeaderTemplate>
                                        <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'grvContent');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" value='<%# Eval("Id") %>' name="grvContent" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" CommandArgument='<%# Eval("ArticleId") %>' CommandName="ContentEdit"
                                            runat="server" ToolTip="Edit this item" ImageUrl="~/Content/Assets/Images/Common/ico_edit.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Eval("Date", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" SortExpression="CreatedByName"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" CommandArgument='<%# Eval("ArticleId") %>' CommandName="A_SendEmail"
                                            runat="server" ToolTip="Send Email Notification" ImageUrl="~/Content/Assets/Images/Common/ico_envelope.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:ObjectDataSource ID="ObjectDataSourceInsertedArticles" runat="server" SelectMethod="SelectLocations"
                        TypeName="WCMS.WebSystem.WebParts.Article.ConfigArticleController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hidObjectId" DefaultValue="-1" Name="objectId" PropertyName="Value"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="hidRecordId" DefaultValue="-1" Name="recordId" PropertyName="Value"
                                Type="Int32" />
                            <asp:Parameter DefaultValue="-2" Name="siteId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="viwExisting" runat="server">
                    <div class="control-box">
                        <div>
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" CssClass="btn btn-default" />
                            <asp:Button ID="btnIDone" runat="server" Text="Done" OnClick="btnIDone_Click" CssClass="btn btn-default" />
                            <%--&nbsp;&nbsp;
                                <asp:Button ID="cmdDuplicate" runat="server" Text="Duplicate" Width="85px" Height="30px"
                                    OnClick="cmdDuplicate_Click" />--%>
                            <div class="pull-right">
                                <asp:DropDownList ID="cboSites" runat="server" CssClass="input"
                                    AutoPostBack="True" OnSelectedIndexChanged="cboSites_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="-2">* Show All Websites *</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grvContentAll" runat="server" CssClass="table table-borderless" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                            PageSize="15" AllowSorting="True" DataSourceID="ObjectDataSourceAllArticles"
                            OnRowCommand="grvContentAll_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerSettings PageButtonCount="35" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <Columns>
                                <asp:TemplateField HeaderText="Id">
                                    <HeaderTemplate>
                                        <input type="checkbox" value="chkMain" onclick="CheckAll(this, 'grvContentAll');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" value='<%# Eval("Id") %>' name="grvContentAll" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" Width="10px" />
                                    <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Custom_Delete" ImageUrl="~/Content/Assets/Images/Common/ico_exit.gif"
                                            AlternateText="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');"
                                            CommandArgument='<%# Eval("Id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date" SortExpression="Date" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Eval("Date", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" SortExpression="CreatedByName"
                                    HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:ObjectDataSource ID="ObjectDataSourceAllArticles" runat="server" SelectMethod="SelectArticles"
                        TypeName="WCMS.WebSystem.WebParts.Article.ConfigArticleController">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboSites" DefaultValue="-2" Name="siteId" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:QueryStringParameter DefaultValue="-1" Name="pageId" QueryStringField="PageId"
                                Type="Int32" />
                            <asp:QueryStringParameter DefaultValue="-1" Name="pageElementId" QueryStringField="PageElementId"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
            </asp:MultiView>
        </asp:Panel>
        <asp:Panel ID="pnlContentEdit" Visible="false" runat="server" Width="100%">
            <br />
            <div>
                <uc1:TabControl ThemeName="green" ID="tabArticle" runat="server" OnSelectedTabChanged="tabArticle_SelectedTabChanged" />
            </div>
            <asp:MultiView ID="mtvContent" runat="server" ActiveViewIndex="0">
                <asp:View ID="viwBasic" runat="server">
                    <table border="0" width="100%" cellpadding="0" cellspacing="3" style="margin: 3px 0 3px 0">
                        <tr>
                            <td style="width: 80px">Title:
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                    Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" Columns="70" CssClass="input" />
                            </td>
                        </tr>
                        <tr>
                            <td>Date:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" CssClass="input" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlDay" CssClass="input" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlYear" CssClass="input" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Description:
                            </td>
                            <td>
                                <FCKeditorV2:FCKeditor ID="fckDescription" runat="server" Height="125px" ToolbarSet="Basic"
                                    Width="98%">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Content:
                            </td>
                            <td>
                                <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Height="400px" Width="98%">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td>Tags:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTags" CssClass="input" placeholder="Optional. e.g. News, Updates, etc." runat="server"
                                    Columns="60" />
                                <span runat="server" id="panelComment" visible="false">&nbsp;Comments:
                                <asp:DropDownList ID="cboCommentOn" runat="server" CssClass="input">
                                    <asp:ListItem Value="-1">(Inherit)</asp:ListItem>
                                    <asp:ListItem Value="1">ON</asp:ListItem>
                                    <asp:ListItem Value="0">OFF</asp:ListItem>
                                </asp:DropDownList></span>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <span style="background-color: Yellow; font-weight: bold">
                                    <asp:CheckBox ID="chkSendEmail" CssClass="aspnet-checkbox" runat="server" Text="Send Email Alert After Update"
                                        Checked="True" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:View>
                <asp:View ID="viwAdvance" runat="server">
                    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="margin-top: 5px;">
                        <tr>
                            <td style="width: 80px;">Content ID:
                            </td>
                            <td>
                                <asp:Literal ID="litID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Author:
                            </td>
                            <td>
                                <asp:TextBox ID="txtAuthor" CssClass="input" runat="server" Columns="75" />
                            </td>
                        </tr>
                        <tr>
                            <td>Rank:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRank" CssClass="input" runat="server">
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkIsActive" CssClass="aspnet-checkbox" runat="server" Text="Active" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Site ID:
                            </td>
                            <td>
                                <asp:DropDownList ID="cboSiteId" CssClass="input" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Save to:
                            </td>
                            <td>
                                <uc3:SaveInFolder ID="saveToFolder" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="viewPublish" runat="server">
                    <br />
                    <uc2:WebSiteElementSelector ID="elementSelector" runat="server" />
                </asp:View>
            </asp:MultiView>
            <div class="control-box">
                <div>
                    <asp:Button ID="cmdPublish" CssClass="btn btn-primary" runat="server" Text="Publish"
                        OnClick="cmdPublish_Click" Visible="false" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CausesValidation="False" CssClass="btn btn-default" />
                </div>
            </div>
        </asp:Panel>
    </asp:View>
    <asp:View ID="viwSettings" runat="server">
        <table border="0" width="100%">
            <tr>
                <td style="width: 80px; font-weight: bold;">RSS Feed:
                </td>
                <td>
                    <a href="" title="" id="linkRSS" runat="server"></a>
                </td>
            </tr>
        </table>
        <table border="0" width="100%">
            <%--<tr id="trDate" runat="server">
                <td style="width: 80px">Date Format:
                </td>
                <td>
                    <asp:TextBox ID="txtDateFormat" runat="server" Columns="40">{0:MMMM d, yyyy}</asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td style="width: 80px">Page Size:
                </td>
                <td>
                    <asp:TextBox ID="txtPageSize" CssClass="input" runat="server" Columns="10" />&nbsp;<asp:CheckBox ID="chkComments" CssClass="aspnet-checkbox" runat="server" Text="Comments" />
                </td>
            </tr>
            <tr>
                <td style="width: 75px">Template:
                </td>
                <td>
                    <asp:DropDownList ID="cboTemplate" CssClass="input" runat="server" DataSourceID="ObjectDataSourceTemplates"
                        DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                        <asp:ListItem Text="* Use Parameters *" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSourceTemplates" runat="server" SelectMethod="SelectTemplates"
                        TypeName="WCMS.WebSystem.WebParts.Article.ConfigArticleController"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>Item Source:
                </td>
                <td>
                    <uc3:SaveInFolder ID="articleFolder" runat="server" />
                </td>
            </tr>
        </table>
        <div class="control-box">
            <div>
                <asp:Button ID="cmdConfigSave" runat="server" Text="Save" OnClick="cmdConfigSave_Click" CssClass="btn btn-default" />
                <%--<asp:Button ID="btnSCancel" runat="server" Text="Cancel" Width="85px" OnClick="btnSCancel_Click"
                        CausesValidation="False" Height="30px" />--%>
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<asp:Label runat="server" ID="lblStatus" ForeColor="Red"></asp:Label>