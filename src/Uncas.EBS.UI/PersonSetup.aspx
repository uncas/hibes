<%@ Page Language="C#" MasterPageFile="~/EBSMaster.Master" AutoEventWireup="true"
    CodeBehind="PersonSetup.aspx.cs" Inherits="Uncas.EBS.UI.PersonSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        <%= Resources.Phrases.SetupDaysOff %></h2>
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
                        <asp:TextBox ID="tbFrom" runat="server" Text='<%# Bind("FromDate") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="tbTo" runat="server" Text='<%# Bind("ToDate") %>'></asp:TextBox>
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
            <uncas:BoundFieldResource HeaderResourceName="From" DataField="FromDate" DataFormatString="{0:d}">
            </uncas:BoundFieldResource>
            <uncas:BoundFieldResource HeaderResourceName="To" DataField="ToDate" DataFormatString="{0:d}">
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
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsPersonOffs" runat="server" TypeName="Uncas.EBS.UI.AppRepository.AppPersonOffRepository"
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
</asp:Content>
