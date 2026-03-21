<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBar.ascx.cs" Inherits="WCMS.WebSystem.Content.Parts.Central.SideBar" %>
<div class="well sidebar-nav affix-top">
    <ul class="nav nav-list">
        <li class="nav-header">Web Page</li>
        <li class="divider"></li>
        <li><a href="#">Link</a></li>
        <li class="nav-header">Web Site</li>
        <li class="active"><a href="#">Link</a></li>
        <li><a href="#">Link</a></li>
        <li class="divider"></li>

        <li class="nav-header">Sidebar</li>
        <li><a href="#">Link</a></li>
        <li><a href="#">Link</a></li>
        <li><a href="#">Link</a></li>
        <li class="dropdown">
            <a class="dropdown-toggle"
                data-toggle="dropdown"
                href="#">Dropdown
                                <b class="caret"></b>
            </a>
            <ul class="dropdown-menu">
                <li class="nav-header">Sidebar</li>
                <li class="active"><a href="#">Link</a></li>
                <li><a href="#">Link</a></li>
                <li><a href="#">Link</a></li>
            </ul>
        </li>
    </ul>
</div>
