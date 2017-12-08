<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WCMS.BibleReader._Default" %>
<a href="Default.aspx">Default.aspx</a>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" ClientIDMode="Static" runat="server" ContentPlaceHolderID="MainContent">
    <div id="panel-main">
        <div id="panel-left">
            <h2>
                Assisted Query
            </h2>
            <br />
            <div class="clear query-element">
                <label for="cboTranslation">
                    Translation:</label>
                <div>
                    <asp:DropDownList ID="cboTranslation" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="float-left query-element">
                <label for="cboBook">
                    Book:</label>
                <div>
                    <asp:DropDownList ID="cboBook" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="float-left query-element">
                <label for="cboChapter">
                    Chapter:</label>
                <div>
                    <asp:DropDownList ID="cboChapter" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="float-left query-element">
                <label for="cboVerse">
                    Verse:</label>
                <div>
                    <asp:DropDownList ID="cboVerse" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div id="panel-right">
            <h2>
                Free-text query
            </h2>
            <br />
            <asp:TextBox ID="txtFreeText" runat="server"></asp:TextBox>
            <asp:Button ID="cmdSearch" runat="server" Text="Search" />
            <div>
                <em>e.g. 2Cor 2:5; KJV 1Tim 3:15</em></div>
        </div>
        <div id="panel-content" class="clear">
            <h2>
                Verses
            </h2>
        </div>
    </div>
</asp:Content>
