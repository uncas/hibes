﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Setup.aspx.cs" Inherits="Uncas.EBS.UI.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul>
        <li><a href="Persons.aspx">
            <%= Resources.Phrases.Persons %></a></li>
        <li><a href="Projects.aspx">
            <%= Resources.Phrases.Projects %></a></li>
    </ul>
</asp:Content>
