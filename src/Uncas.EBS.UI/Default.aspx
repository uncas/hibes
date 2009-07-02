<%@ Page Title="" Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Uncas.EBS.UI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>
            <div class="parts">
                <div class="part">
                    <h2>
                        <%= Resources.Phrases.Issues %></h2>
                </div>
                <div class="part">
                    <div class="options">
                        <div class="option">
                            <uncas:ProjectSelection ID="psProjects" runat="server">
                            </uncas:ProjectSelection>
                        </div>
                        <div class="option">
                            <uncas:StatusOptions ID="soStatus" runat="server">
                            </uncas:StatusOptions>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:FormView ID="fvNewIssue" runat="server" DataSourceID="odsIssues">
                    <EmptyDataTemplate>
                        <asp:LinkButton ID="lbNew" runat="server" CommandName="New"><%= Resources.Phrases.CreateNewIssue %></asp:LinkButton>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbNew" runat="server" CommandName="New"><%= Resources.Phrases.CreateNewIssue %></asp:LinkButton>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <%= Resources.Phrases.Project %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Priority %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Issue %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Status %>
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tr>
                                <td>
                                    <asp:TextBox ID="tbProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <uncas:NumberBox ID="nbPriority" runat="server" Text='<%# Bind("Priority") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <uncas:StatusSelection ID="ssStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                    </uncas:StatusSelection>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert"><%= Resources.Phrases.Insert %></asp:LinkButton>
                                    <asp:LinkButton ID="lbCancel" runat="server" CommandName="Cancel"><%= Resources.Phrases.Cancel %></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
            <div id="issues">
                <asp:GridView ID="gvIssues" runat="server" AutoGenerateColumns="False" DataSourceID="odsIssues"
                    DataKeyNames="IssueId">
                    <Columns>
                        <uncas:BoundFieldResource HeaderResourceName="Project" DataField="ProjectName" ReadOnly="true" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Priority %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <uncas:NumberBox ID="nbPriority" runat="server" Text='<%# Bind("Priority") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Issue %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="tbTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="hlTitle" runat="server" NavigateUrl='<%# Eval("IssueId", "Tasks.aspx?Issue={0}") %>'
                                    Text='<%# Eval("Title") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Status %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <uncas:StatusSelection ID="ssStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                </uncas:StatusSelection>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <uncas:StatusLabel ID="slStatus" runat="server" Status='<%# Eval("Status") %>'></uncas:StatusLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <uncas:BoundFieldResource HeaderResourceName="Created" DataField="CreatedDate" DataFormatString="{0:d}"
                            HtmlEncode="False" ReadOnly="True" />
                        <uncas:BoundFieldResource HeaderResourceName="Tasks" DataField="NumberOfTasks" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" ReadOnly="True" />
                        <uncas:BoundFieldResource HeaderResourceName="Elapsed" DataField="Elapsed" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" ReadOnly="True" />
                        <uncas:BoundFieldResource HeaderResourceName="Remaining" DataField="Remaining" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" ReadOnly="True" />
                        <uncas:BoundFieldResource HeaderResourceName="Progress" DataField="FractionElapsed"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:P0}"
                            ReadOnly="True" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <uncas:EditButton ID="ebEdit" runat="server"></uncas:EditButton>
                                <uncas:DeleteButton ID="dbDelete" runat="server"></uncas:DeleteButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uncas:UpdateButton ID="ubUpdate" runat="server"></uncas:UpdateButton>
                                <uncas:CancelButton ID="ubCancel" runat="server"></uncas:CancelButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppIssueRepository"
                    SelectMethod="GetIssues" UpdateMethod="UpdateIssue" InsertMethod="InsertIssue"
                    DeleteMethod="DeleteIssue" OldValuesParameterFormatString="Original_{0}">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="psProjects" Name="projectId" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="soStatus" Name="status" PropertyName="SelectedValue"
                            Type="Object" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Original_IssueId" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Status" Type="Object" />
                        <asp:Parameter Name="Priority" Type="Int32" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
