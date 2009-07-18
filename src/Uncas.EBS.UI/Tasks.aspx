<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Tasks.aspx.cs" Inherits="Uncas.EBS.UI.Tasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div id="issue">
                <asp:GridView ID="gvIssue" runat="server" AutoGenerateColumns="False" DataSourceID="odsIssue"
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
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
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
                                <uncas:StatusLabel ID="slStatus" runat="server" Status='<%# Eval("Status") %>'>
                                </uncas:StatusLabel>
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
                                <uncas:EditButton ID="ebEdit" runat="server">
                                </uncas:EditButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uncas:UpdateButton ID="ubUpdate" runat="server">
                                </uncas:UpdateButton>
                                <uncas:CancelButton ID="ubCancel" runat="server">
                                </uncas:CancelButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsIssue" runat="server" TypeName="Uncas.EBS.UI.Controllers.IssueController"
                    SelectMethod="GetIssue" UpdateMethod="UpdateIssue" OldValuesParameterFormatString="Original_{0}">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="IssueId" QueryStringField="Issue" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Original_IssueId" Type="Int32" />
                        <asp:Parameter Name="Title" Type="String" />
                        <asp:Parameter Name="Status" Type="Object" />
                        <asp:Parameter Name="Priority" Type="Int32" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="newTask">
                <asp:FormView ID="fvNewTask" runat="server" DataSourceID="odsTasks">
                    <EmptyDataTemplate>
                        <uncas:NewButton ID="nbNew" runat="server">
                            <%= Resources.Phrases.CreateNewTask %>
                        </uncas:NewButton>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <uncas:NewButton ID="nbNew" runat="server">
                            <%= Resources.Phrases.CreateNewTask %>
                        </uncas:NewButton>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <%= Resources.Phrases.Number %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Task %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Status %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Original %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Elapsed %>
                                    </th>
                                    <th>
                                        <%= Resources.Phrases.Person %>
                                    </th>
                                    <th style="display: none;">
                                        <%= Resources.Phrases.Begin %>
                                    </th>
                                    <th style="display: none;">
                                        <%= Resources.Phrases.End %>
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tr>
                                <td>
                                    <uncas:NumberBox ID="nbSequence" runat="server" Text='<%# Bind("Sequence") %>' />
                                </td>
                                <td>
                                    <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <uncas:StatusSelection ID="ssStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                    </uncas:StatusSelection>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbOriginal" runat="server" Text='<%# Bind("OriginalEstimate") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbElapsed" runat="server" Text='<%# Bind("Elapsed") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <uncas:PersonSelection ID="psPerson" runat="server" SelectedValue='<%# Bind("RefPersonId") %>'>
                                    </uncas:PersonSelection>
                                </td>
                                <td style="display: none;">
                                    <uncas:DateBox ID="dbStart" runat="server" DateObject='<%# Bind("StartDate") %>'>
                                    </uncas:DateBox>
                                </td>
                                <td style="display: none;">
                                    <uncas:DateBox ID="dbEnd" runat="server" DateObject='<%# Bind("EndDate") %>'>
                                    </uncas:DateBox>
                                </td>
                                <td>
                                    <uncas:InsertButton ID="ibInsert" runat="server">
                                    </uncas:InsertButton>
                                    <uncas:CancelButton ID="cbCancel" runat="server">
                                    </uncas:CancelButton>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
            <div id="divStatus">
                <uncas:StatusOptions ID="soStatus" runat="server">
                </uncas:StatusOptions>
            </div>
            <div id="divTasks">
                <asp:GridView ID="gvTasks" runat="server" DataSourceID="odsTasks" AutoGenerateColumns="False"
                    DataKeyNames="TaskId">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Number %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <uncas:NumberBox ID="nbSequence" runat="server" Text='<%# Bind("Sequence") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSequence" runat="server" Text='<%# Bind("Sequence") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <uncas:BoundFieldResource HeaderResourceName="Task" DataField="Description">
                            <ControlStyle Width="250px" />
                        </uncas:BoundFieldResource>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Status %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <uncas:StatusSelection ID="ssStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                </uncas:StatusSelection>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <uncas:StatusLabel ID="slStatus" runat="server" Status='<%# Eval("Status") %>'>
                                </uncas:StatusLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <uncas:BoundFieldResource HeaderResourceName="Original" DataField="OriginalEstimate"
                            HtmlEncode="false" DataFormatString="{0:N1}" ReadOnly="true" ItemStyle-HorizontalAlign="Right" />
                        <uncas:BoundFieldResource HeaderResourceName="Current" DataField="CurrentEstimate"
                            HtmlEncode="false" DataFormatString="{0:N1}" ItemStyle-HorizontalAlign="Right" />
                        <uncas:BoundFieldResource HeaderResourceName="Elapsed" DataField="Elapsed" HtmlEncode="false"
                            DataFormatString="{0:N1}" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= Resources.Phrases.Person %>
                            </HeaderTemplate>
                            <EditItemTemplate>
                                <uncas:PersonSelection ID="psPerson" runat="server" SelectedValue='<%# Bind("RefPersonId") %>'>
                                </uncas:PersonSelection>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPerson" runat="server" Text='<%# Eval("PersonName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <uncas:DateField HeaderResourceName="Begin" DataField="StartDate" Visible="false">
                        </uncas:DateField>
                        <uncas:DateField HeaderResourceName="End" DataField="EndDate" Visible="false">
                        </uncas:DateField>
                        <uncas:BoundFieldResource HeaderResourceName="Created" DataField="CreatedDate" HtmlEncode="false"
                            DataFormatString="{0:d}" ReadOnly="true" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <uncas:EditButton ID="ebEdit" runat="server">
                                </uncas:EditButton>
                                <uncas:DeleteButton ID="dbDelete" runat="server">
                                </uncas:DeleteButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <uncas:UpdateButton ID="ubUpdate" runat="server">
                                </uncas:UpdateButton>
                                <uncas:CancelButton ID="ubCancel" runat="server">
                                </uncas:CancelButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsTasks" runat="server" TypeName="Uncas.EBS.UI.Controllers.TaskController"
                    SelectMethod="GetTasks" UpdateMethod="UpdateTask" DeleteMethod="DeleteTask" InsertMethod="InsertTask"
                    OldValuesParameterFormatString="Original_{0}">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="IssueId" QueryStringField="Issue" Type="Int32" />
                        <asp:ControlParameter Name="Status" ControlID="soStatus" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="StartDate" Type="DateTime" />
                        <asp:Parameter Name="EndDate" Type="DateTime" />
                        <asp:Parameter Name="CurrentEstimate" Type="Double" />
                        <asp:Parameter Name="Elapsed" Type="Double" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:QueryStringParameter Name="RefIssueId" QueryStringField="Issue" Type="Int32" />
                        <asp:Parameter Name="StartDate" Type="DateTime" />
                        <asp:Parameter Name="EndDate" Type="DateTime" />
                        <asp:Parameter Name="OriginalEstimate" Type="Double" />
                        <asp:Parameter Name="Elapsed" Type="Double" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
