<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderNavbar.ascx.cs" Inherits="WCMS.WebSystem.Content.Parts.Central.HeaderNavbar" %>
<%@ Import Namespace="WCMS.WebSystem" %>
<%= RazorHelper.RenderPage("~/Content/Parts/Central/HeaderNavbar.cshtml", new {Context = this}, "Central.HeaderNavbar") %>