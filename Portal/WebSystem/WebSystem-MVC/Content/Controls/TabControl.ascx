<%@ Control Language="C#" AutoEventWireup="true" Inherits="WCMS.WebSystem.Controls.TabControl"
    EnableViewState="true" CodeBehind="TabControl.ascx.cs" %>
<%--<li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Home</a></li>
    <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Profile</a></li>
    <li role="presentation"><a href="#messages" aria-controls="messages" role="tab" data-toggle="tab">Messages</a></li>
    <li role="presentation"><a href="#settings" aria-controls="settings" role="tab" data-toggle="tab">Settings</a></li>--%>
<div runat="server" role="tabpanel" id="panelTabPanel" clientidmode="Static" class="portal-tabpanel">
  <!-- Nav tabs -->
  <ul class="nav nav-tabs" role="tablist" runat="server" id="panelTab" clientidmode="Static"></ul>
</div>