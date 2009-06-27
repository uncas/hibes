<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Estimates.aspx.cs" Inherits="Uncas.EBS.UI.Estimates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="parts">
                <div class="part">
                    <h2>
                        <%= Resources.Phrases.Estimates %></h2>
                </div>
                <div class="part">
                    <div class="options">
                        <div class="option">
                            <uncas:ProjectSelection ID="psProjects" runat="server">
                            </uncas:ProjectSelection>
                        </div>
                        <div class="option">
                            <%= Resources.Phrases.MaxPriority %>:
                            <uncas:NumberBox ID="nbMaxPriority" runat="server" AutoPostBack="true" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="parts">
                <div class="part">
                    <div id="divSummary">
                        <asp:FormView ID="fvSummary" runat="server" DataSourceID="odsSummary">
                            <ItemTemplate>
                                <table>
                                    <thead>
                                        <tr>
                                            <th align="right">
                                                <%= Resources.Phrases.Average %>
                                            </th>
                                            <th align="right">
                                                <%= Resources.Phrases.StandardDeviation %>
                                            </th>
                                            <th align="right">
                                                <%= Resources.Phrases.Issues %>
                                            </th>
                                            <th align="right">
                                                <%= Resources.Phrases.Tasks %>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAverage" runat="server" Text='<%# Eval("Average", "{0:N1}") %>'></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblDeviation" runat="server" Text='<%# Bind("StandardDeviation", "{0:N1}") %>'></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblIssues" runat="server" Text='<%# Bind("NumberOfOpenIssues") %>'></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblTasks" runat="server" Text='<%# Bind("NumberOfOpenTasks") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </thead>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="odsSummary" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                            SelectMethod="GetProjectEstimate">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div id="divDistribution">
                        <asp:GridView ID="gvProbabilities" runat="server" DataSourceID="odsProbabilities"
                            AutoGenerateColumns="false">
                            <RowStyle HorizontalAlign="Right" />
                            <Columns>
                                <uncas:BoundFieldResource HeaderResourceName="From" DataField="Lower" />
                                <uncas:BoundFieldResource HeaderResourceName="To" DataField="Upper" />
                                <uncas:BoundFieldResource HeaderResourceName="Probability" DataField="Probability"
                                    DataFormatString="{0:P0}" HtmlEncode="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsProbabilities" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                            SelectMethod="GetIntervalProbabilities">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
                <div class="part">
                    <asp:Chart ID="chartProbabilities" runat="server" DataSourceID="odsProbabilities">
                        <Series>
                            <asp:Series Name="Series1" XValueMember="Lower" YValueMembers="Probability" ChartType="Column"
                                Color="#339999">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY Minimum="0">
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
            <div id="divIssues">
                <asp:GridView ID="gvIssues" runat="server" DataSourceID="odsIssues" AutoGenerateColumns="false"
                    AllowSorting="false" EmptyDataText="Ingen sager har de valgte kriterier.">
                    <Columns>
                        <uncas:BoundFieldResource HeaderResourceName="Project" DataField="ProjectName" />
                        <uncas:BoundFieldResource HeaderResourceName="Priority" DataField="Priority" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <uncas:HyperLinkFieldResource HeaderResourceName="Issue" DataNavigateUrlFields="IssueId"
                            DataNavigateUrlFormatString="Tasks.aspx?Issue={0}" DataTextField="IssueTitle" />
                        <uncas:BoundFieldResource HeaderResourceName="Average" DataField="Average" DataFormatString="{0:N1}"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                    SelectMethod="GetIssueEstimates">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
