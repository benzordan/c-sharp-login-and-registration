<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="as_assignment.Homepage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>SIT Connect Member Homepage</h1><table class="nav-justified">
            <tr>
                <td style="width: 546px">&nbsp;</td>
                <td style="width: 923px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 546px">&nbsp;</td>
                <td style="width: 923px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 546px">&nbsp;</td>
                <td style="width: 923px">
                    <asp:Label ID="lb_message" runat="server" Text="lb message"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 546px">&nbsp;</td>
                <td style="width: 923px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 546px">
                    <asp:Button ID="Logout" runat="server" Text="Logout" OnClick="Button1_Click" style="height: 26px" />
                </td>
                <td style="width: 923px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            </table>
    </div>
</asp:Content>