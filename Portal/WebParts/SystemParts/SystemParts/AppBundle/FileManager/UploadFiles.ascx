<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadFiles.ascx.cs"
    Inherits="WCMS.WebSystem.WebParts.FileManager.UploadFiles" %>
<%@ Register Src="Controls/Breadcrumb.ascx" TagName="Breadcrumb" TagPrefix="uc1" %>
<div class="_wp_FileManager wp-FileManager">
    <uc1:Breadcrumb ID="Breadcrumb1" runat="server" />
    <br />
    <div id="fileUploads" runat="server" clientidmode="Static">
        <div class="field-row">
            <strong>Select files to upload</strong>
        </div>
        <asp:FileUpload ID="fileUpload1" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
        <asp:FileUpload ID="fileUpload2" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
        <asp:FileUpload ID="fileUpload3" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
        <asp:FileUpload ID="fileUpload4" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
        <asp:FileUpload ID="fileUpload5" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
        <asp:FileUpload ID="fileUpload6" ClientIDMode="Static" CssClass="fileUploadBox" ToolTip="Enter the file path or browse to find the file you want to add."
            runat="server" />
    </div>
    <div runat="server" clientidmode="Static" id="panelPasswordAndArchive" visible="false">
        <div class="field-row">
            <strong>Password and Archive Options</strong>
        </div>
        <div style="padding-bottom: 10px;">
            <asp:CheckBox ClientIDMode="Static" CssClass="field-label aspnet-checkbox" ID="chkPassword" Text="Set Password:"
                runat="server" />
            <asp:TextBox ClientIDMode="Static" ID="txtPassword" TextMode="Password" Columns="30"
                runat="server" CssClass="input"></asp:TextBox><br />
            <span style="padding-left: 20px; font-size: smaller; font-style: italic">Your file will be archived with a password
            and a .zip extension.</span>
            <div style="padding-top: 5px;" id="panelPasswordComplexity">
                <div style="padding-left: 20px; font-weight: bold">
                    Password complexity requirement:
                </div>
                <ul style="margin-top: 0px">
                    <li>Password must be at least 8 characters</li>
                    <li>Must must contain at least one number and one letter (whether lower or upper case)</li>
                    <li>Special characters are OK</li>
                </ul>
            </div>
        </div>
        <div style="padding-bottom: 10px;">
            <asp:CheckBox ClientIDMode="Static" CssClass="field-label aspnet-checkbox" ID="chkArchive" Text="Archive into one file:"
                runat="server" />
            <asp:TextBox ClientIDMode="Static" ID="txtFilename" runat="server" Columns="30" CssClass="input"></asp:TextBox><br />
            <span style="padding-left: 20px; font-size: smaller; font-style: italic">All your file uploads will be archived into one .zip file, enter the archive name.</span>
        </div>
        <div style="padding-bottom: 15px;">
            <asp:CheckBox ClientIDMode="Static" CssClass="field-label aspnet-checkbox" ID="chkDeployer" Text="Extract and deploy all archives"
                runat="server" />
        </div>
    </div>
    <br />
    <div id="buttonBarRow" class="control-box">
        <div id="buttonBar" class="buttonBar">
            <asp:Button ID="cmdUploadNow" CssClass="btn btn-primary" runat="server" Text="Upload" OnClick="cmdUploadNow_Click" />&nbsp;
            <asp:Button ID="cmdCancel" CssClass="btn btn-default" runat="server" Text="Cancel" OnClick="cmdCancel_Click" />
            <div class="pull-right" style="font-size: larger" id="panelStorageInfo" runat="server"
                visible="false">
                <asp:Label runat="server" ID="lblStorageInfo"></asp:Label>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="uploadSubheading2" id="helperText">
        <p>
            <strong>Note</strong>&nbsp;&nbsp;Upload size is limited to 100 MB per file.
        </p>
    </div>
    <br />
    <asp:Label CssClass="Header" EnableViewState="false" Style="color: Red" ID="lblMessage"
        runat="server" ForeColor="Red"></asp:Label>
    <script type="text/javascript">
        $(document).ready(function () {
            var toggleArchiveFilename = function () {
                var checked = $("#chkArchive").is(":checked");
                if (checked) {
                    $("#txtFilename").removeAttr("disabled");
                }
                else {
                    $("#txtFilename").attr("disabled", "disabled");
                }
            }

            var togglePassword = function () {
                var checked = $("#chkPassword").is(":checked");
                if (checked) {
                    $("#txtPassword").removeAttr("disabled");
                }
                else {
                    $("#txtPassword").attr("disabled", "disabled");
                }

                $("#panelPasswordComplexity").css("display", checked ? "" : "none");
            }


            $("#chkPassword").change(function () {
                togglePassword();
            });

            $("#chkArchive").change(function () {
                toggleArchiveFilename();
            });


            togglePassword();
            toggleArchiveFilename();
        });
    </script>
</div>
