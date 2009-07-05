<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Speeds.aspx.cs" Inherits="Uncas.EBS.UI.Speeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.History %></h2>
    <asp:Chart ID="chartClosedTasks" runat="server" DataSourceID="odsClosedTasks">
        <Series>
            <asp:Series Name="SeriesOriginalVsElapsed" YValueMembers="OriginalEstimate" XValueMember="Elapsed"
                ChartType="Point" Color="#339999" MarkerSize="9" MarkerStyle="Circle">
            </asp:Series>
            <asp:Series ChartType="Line" Color="Black">
                <Points>
                    <asp:DataPoint XValue="0" YValues="0" />
                    <asp:DataPoint XValue="5" YValues="5" />
                </Points>
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartAreaClosedTasks">
                <AxisX Minimum="0">
                </AxisX>
                <AxisY Minimum="0">
                </AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <asp:GridView ID="gvClosedTasks" runat="server" DataSourceID="odsClosedTasks" Visible="false">
    </asp:GridView>
    <asp:ObjectDataSource ID="odsClosedTasks" runat="server" TypeName="Uncas.EBS.UI.Controllers.TaskController"
        SelectMethod="GetClosedTasks"></asp:ObjectDataSource>
</asp:Content>
