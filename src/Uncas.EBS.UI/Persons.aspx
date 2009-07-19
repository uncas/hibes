<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="Persons.aspx.cs" Inherits="Uncas.EBS.UI.Persons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.Persons %></h2>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:FormView ID="fvNewPerson" runat="server" DataSourceID="odsPersons">
                <EmptyDataTemplate>
                    <uncas:NewButton ID="nbNew" runat="server"></uncas:NewButton>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <uncas:NewButton ID="nbNew" runat="server"></uncas:NewButton>
                </ItemTemplate>
                <InsertItemTemplate>
                    <table>
                        <thead>
                            <tr>
                                <th>
                                    <%= Resources.Phrases.Person %>
                                </th>
                                <th>
                                    <%= Resources.Phrases.DaysPerWeek %>
                                </th>
                                <th>
                                    <%= Resources.Phrases.HoursPerDay %>
                                </th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("PersonName") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDays" runat="server" Text='<%# Bind("DaysPerWeek") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="tbHours" runat="server" Text='<%# Bind("HoursPerDay") %>'></asp:TextBox>
                            </td>
                            <td>
                                <uncas:InsertButton ID="ibInsert" runat="server"></uncas:InsertButton>
                                <uncas:CancelButton ID="cbCancel" runat="server"></uncas:CancelButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:GridView ID="gvPersons" runat="server" DataSourceID="odsPersons" DataKeyNames="PersonId"
                AutoGenerateColumns="false">
                <Columns>
                    <uncas:BoundFieldResource HeaderResourceName="Person" DataField="PersonName">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="DaysPerWeek" DataField="DaysPerWeek">
                    </uncas:BoundFieldResource>
                    <uncas:BoundFieldResource HeaderResourceName="HoursPerDay" DataField="HoursPerDay">
                    </uncas:BoundFieldResource>
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
                    <uncas:HyperLinkFieldResource TextResourceName="SetupDaysOff" DataNavigateUrlFields="PersonId"
                        DataNavigateUrlFormatString="PersonSetup.aspx?Person={0}">
                    </uncas:HyperLinkFieldResource>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsPersons" runat="server" TypeName="Uncas.EBS.UI.Controllers.PersonController"
                SelectMethod="GetPersons" InsertMethod="InsertPerson" UpdateMethod="UpdatePerson"
                DeleteMethod="DeletePerson" OldValuesParameterFormatString="Original_{0}">
                <InsertParameters>
                    <asp:Parameter Name="HoursPerDay" Type="Double" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="HoursPerDay" Type="Double" />
                </UpdateParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
