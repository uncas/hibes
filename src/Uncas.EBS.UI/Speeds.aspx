<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Speeds.aspx.cs" Inherits="Uncas.EBS.UI.Speeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.History %></h2>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div>
                <uncas:PersonSelection ID="ps1" runat="server" AutoPostBack="true" ShowAllOption="true">
                </uncas:PersonSelection>
            </div>
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
            <asp:GridView ID="gvClosedTasks" runat="server" DataSourceID="odsClosedTasks" AutoGenerateColumns="false">
                <Columns>
                    <uncas:BoundFieldResource HeaderResourceName="Date" DataField="CreatedDate">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="Task" DataField="Description">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="Original" DataField="OriginalEstimate">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="Elapsed" DataField="Elapsed">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="Speed" DataField="Speed">
                    </uncas:BoundFieldResource>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsClosedTasks" runat="server" TypeName="Uncas.EBS.UI.Controllers.TaskController"
                SelectMethod="GetClosedTasks">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ps1" Name="RefPersonId" PropertyName="PersonId"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
