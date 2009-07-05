<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="PersonSetup.aspx.cs" Inherits="Uncas.EBS.UI.PersonSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.SetupDaysOff %></h2>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:FormView ID="fvPersonOffs" runat="server" DataSourceID="odsPersonOffs">
                <EmptyDataTemplate>
                    <uncas:NewButton ID="nbNew" runat="server"><%= Resources.Phrases.New %></uncas:NewButton>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <uncas:NewButton ID="nbNew" runat="server"><%= Resources.Phrases.New %></uncas:NewButton>
                </ItemTemplate>
                <InsertItemTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>
                                    <%= Resources.Phrases.From %>
                                </th>
                                <th>
                                    <%= Resources.Phrases.To %>
                                </th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <uncas:DateBox ID="dbFrom" runat="server" SelectedDate='<%# Bind("FromDate") %>'>
                                </uncas:DateBox>
                            </td>
                            <td>
                                <uncas:DateBox ID="dbTo" runat="server" SelectedDate='<%# Bind("ToDate") %>'>
                                </uncas:DateBox>
                            </td>
                            <td>
                                <uncas:InsertButton ID="ibInsert" runat="server"></uncas:InsertButton>
                                <uncas:CancelButton ID="cbCancel" runat="server"></uncas:CancelButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:GridView ID="gvPersonOffs" runat="server" DataSourceID="odsPersonOffs" AutoGenerateColumns="false"
                DataKeyNames="PersonOffId">
                <Columns>
                    <uncas:DateField HeaderResourceName="From" DataField="FromDate">
                    </uncas:DateField>
                    <uncas:DateField HeaderResourceName="To" DataField="ToDate">
                    </uncas:DateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <uncas:EditButton ID="ebEdit" runat="server"></uncas:EditButton>
                            <uncas:DeleteButton ID="dbDelete" runat="server"></uncas:DeleteButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uncas:UpdateButton ID="ubUpdate" runat="server"></uncas:UpdateButton>
                            <uncas:CancelButton ID="cbCancel" runat="server"></uncas:CancelButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsPersonOffs" runat="server" TypeName="Uncas.EBS.UI.Controllers.PersonOffController"
                SelectMethod="GetPersonOffs" UpdateMethod="UpdatePersonOff" InsertMethod="InsertPersonOff"
                DeleteMethod="DeletePersonOff" OldValuesParameterFormatString="Original_{0}">
                <InsertParameters>
                    <asp:Parameter Name="FromDate" Type="DateTime" />
                    <asp:Parameter Name="ToDate" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FromDate" Type="DateTime" />
                    <asp:Parameter Name="ToDate" Type="DateTime" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
