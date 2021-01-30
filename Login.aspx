<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="as_assignment.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Login to SIT Connect</h1>
        <table class="nav-justified">
            <tr>
                <td style="width: 138px">
                    <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                </td>
                <td style="width: 220px">
                    <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 20px; width: 138px"></td>
                <td style="height: 20px; width: 220px;"></td>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="width: 138px; height: 22px;">
                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                </td>
                <td style="width: 220px; height: 22px">
                    <asp:TextBox ID="tb_PW" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td style="width: 138px; height: 20px;"></td>
                <td style="height: 20px; width: 220px">
                    <asp:HyperLink ID="HyperLink1" runat="server">Forgot Password?</asp:HyperLink>
                </td>
                <td style="height: 20px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 138px; height: 20px;"></td>
                <td style="width: 220px; height: 20px;"></td>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="width: 138px">&nbsp;</td>
                <td style="width: 220px">
                    <!-- Insert captcha -->
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 138px">&nbsp;</td>
                <td style="width: 220px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 138px; height: 20px;"></td>
                <td style="width: 220px; height: 20px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" />
                </td>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td style="width: 138px">&nbsp;</td>
                <td style="width: 220px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 138px">&nbsp;</td>
                <td style="width: 220px">
                    <asp:Label ID="lb_Error" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
</asp:Content>