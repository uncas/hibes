<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColorCodes.ascx.cs"
    Inherits="Uncas.EBS.UI.Controls.ColorCodes" %>
<ul id="colorCodes">
    <li class="inProgress">
        <uncas:ResourceLabel ID="rlInProgress" runat="server" ResourceName="InProgress"></uncas:ResourceLabel></li>
    <li class="notStarted">
        <uncas:ResourceLabel ID="rlNotStarted" runat="server" ResourceName="NotStarted"></uncas:ResourceLabel></li>
    <li class="noTasks">
        <uncas:ResourceLabel ID="rlNoTasks" runat="server" ResourceName="NoTasks"></uncas:ResourceLabel></li>
</ul>
