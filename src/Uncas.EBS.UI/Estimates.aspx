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
            <h3>
                <%= Resources.Phrases.Date %></h3>
            <div class="parts">
                <div class="part">
                    <asp:ObjectDataSource ID="odsSelectedCompletionDateConfidences" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                        SelectMethod="GetSelectedCompletionDateConfidences">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:GridView ID="gvSelectedCompletionDates" runat="server" DataSourceID="odsSelectedCompletionDateConfidences"
                        AutoGenerateColumns="false">
                        <Columns>
                            <uncas:BoundFieldResource HeaderResourceName="End" DataField="Date" DataFormatString="{0:d}">
                            </uncas:BoundFieldResource>
                            <uncas:BoundFieldResource HeaderResourceName="Probability" DataField="Probability"
                                DataFormatString="{0:P0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                            </uncas:BoundFieldResource>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="part">
                    <asp:ObjectDataSource ID="odsCompletionDateConfidences" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                        SelectMethod="GetCompletionDateConfidences">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:Chart ID="chartCompletionDateConfidences" runat="server" DataSourceID="odsCompletionDateConfidences">
                        <Series>
                            <asp:Series Name="seriesConfidence" XValueMember="Date" YValueMembers="Probability"
                                ChartType="Line" YValueType="Double">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY Minimum="0" Maximum="1">
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                    <asp:Chart ID="chartDateRanges" runat="server" Visible="false">
                        <Series>
                            <asp:Series Name="Tasks" ChartType="RangeBar" YValuesPerPoint="3">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY IsStartedFromZero="False">
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </div>
            </div>
            <h3>
                <%= Resources.Phrases.Days %></h3>
            <div class="parts" id="divSummary">
                <div class="part">
                    <asp:ObjectDataSource ID="odsSummary" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                        SelectMethod="GetProjectEstimate">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:GridView ID="gvSummary" runat="server" DataSourceID="odsSummary" AutoGenerateColumns="false">
                        <HeaderStyle HorizontalAlign="Right" />
                        <RowStyle HorizontalAlign="Right" />
                        <Columns>
                            <uncas:BoundFieldResource HeaderResourceName="Average" DataField="Average" DataFormatString="{0:N0}" />
                            <uncas:BoundFieldResource HeaderResourceName="StandardDeviation" DataField="StandardDeviation"
                                DataFormatString="&plusmn; {0:N1}" />
                            <uncas:BoundFieldResource HeaderResourceName="Elapsed" DataField="Elapsed" DataFormatString="{0:N0}" />
                            <uncas:BoundFieldResource HeaderResourceName="Progress" DataField="Progress" DataFormatString="{0:P0}" />
                            <uncas:BoundFieldResource HeaderResourceName="Issues" DataField="NumberOfOpenIssues" />
                            <uncas:BoundFieldResource HeaderResourceName="Tasks" DataField="NumberOfOpenTasks" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="parts">
                <asp:ObjectDataSource ID="odsProbabilities" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                    SelectMethod="GetIntervalProbabilities">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div class="part" id="divDistribution">
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
                </div>
                <div class="part">
                    <asp:Chart ID="chartProbabilities" runat="server" DataSourceID="odsProbabilities">
                        <Series>
                            <asp:Series Name="Series1" XValueMember="Lower" YValueMembers="Probability" ChartType="Column">
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
            <h3>
                <%= Resources.Phrases.Issues %></h3>
            <div class="parts">
                <div id="divIssues" class="part">
                    <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppProjectRepository"
                        SelectMethod="GetIssueEstimates">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="nbMaxPriority" Name="maxPriority" PropertyName="Text"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:GridView ID="gvIssues" runat="server" DataSourceID="odsIssues" AutoGenerateColumns="false"
                        AllowSorting="false">
                        <Columns>
                            <uncas:BoundFieldResource HeaderResourceName="Project" DataField="ProjectName" />
                            <uncas:BoundFieldResource HeaderResourceName="Priority" DataField="Priority" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-HorizontalAlign="Right" />
                            <uncas:HyperLinkFieldResource HeaderResourceName="Issue" DataNavigateUrlFields="IssueId"
                                DataNavigateUrlFormatString="Tasks.aspx?Issue={0}" DataTextField="IssueTitle" />
                            <uncas:BoundFieldResource HeaderResourceName="Average" DataField="Average" DataFormatString="{0:N1}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                            <uncas:BoundFieldResource HeaderResourceName="Progress" DataField="Progress" DataFormatString="{0:P0}"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
