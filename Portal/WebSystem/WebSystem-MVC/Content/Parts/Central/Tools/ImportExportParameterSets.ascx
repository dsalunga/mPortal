<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportExportParameterSets.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.Central.Tools.ExportImportParameterSets" %>
<%@ Register Src="~/Content/Controls/TabControl.ascx" TagName="TabControl" TagPrefix="uc1" %>
<uc1:TabControl ID="TabControl1" ThemeName="default" OnSelectedTabChanged="TabControl1_SelectedTabChanged"
    runat="server" />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="viewExport" runat="server">
        <div style="margin-top: 5px">
            <asp:DropDownList ID="cboParamSets" CssClass="input" runat="server" DataSourceID="ObjectDataSource1" DataTextField="Name"
                DataValueField="Id">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList"
                TypeName="WCMS.WebSystem.WebParts.Central.Tools.ExportImportParameterSets"></asp:ObjectDataSource>
        </div>
        <div>
            &nbsp;
        </div>
        <div class="control-box">
            <div>
                <asp:Button ID="cmdExport" runat="server" CssClass="btn btn-default" OnClick="cmdExport_Click"
                    Text="Export as XML" />
            </div>
        </div>
    </asp:View>
    <asp:View ID="viewImport" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div>
            &nbsp;
        </div>
        <div class="control-box">
            <div>
                <asp:Button ID="cmdImport" runat="server" CssClass="btn btn-default" OnClick="cmdImport_Click"
                    Text="Import from XML" />
            </div>
        </div>
    </asp:View>
</asp:MultiView>
<br />
<br />
<asp:Label ID="lblStatus" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
