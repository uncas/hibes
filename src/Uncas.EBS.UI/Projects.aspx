<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Projects.aspx.cs" Inherits="Uncas.EBS.UI.Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.Projects %></h2>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblInfo" runat="server" CssClass="error"></asp:Label>
            <asp:FormView ID="fvNewProject" runat="server" DataSourceID="odsProjects">
                <EmptyDataTemplate>
                    <asp:LinkButton ID="lbNew" runat="server" CommandName="New"><%= Resources.Phrases.CreateNewProject %></asp:LinkButton>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbNew" runat="server" CommandName="New"><%= Resources.Phrases.CreateNewProject %></asp:LinkButton>
                </ItemTemplate>
                <InsertItemTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>
                                    <%= Resources.Phrases.Project %>
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
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="Insert"><%= Resources.Phrases.Insert %></asp:LinkButton>
                                <asp:LinkButton ID="lbCancel" runat="server" CommandName="Cancel"><%= Resources.Phrases.Cancel %></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:GridView ID="gvProjects" runat="server" DataSourceID="odsProjects" DataKeyNames="ProjectId"
                AutoGenerateColumns="false">
                <Columns>
                    <uncas:BoundFieldResource HeaderResourceName="Project" DataField="ProjectName" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <uncas:EditButton ID="ebEdit" runat="server" />
                            <uncas:DeleteButton ID="dbDelete" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uncas:UpdateButton ID="ubUpdate" runat="server" />
                            <uncas:CancelButton ID="cbCancel" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsProjects" runat="server" TypeName="Uncas.EBS.UI.Controllers.ProjectController"
                SelectMethod="GetProjects" InsertMethod="InsertProject" UpdateMethod="UpdateProject"
                DeleteMethod="DeleteProject" OldValuesParameterFormatString="Original_{0}"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
