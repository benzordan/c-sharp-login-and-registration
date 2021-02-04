<%@ Page Title="Registration Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="as_assignment.Registration" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("lbPW").innerHTML = "Password Length must be at least 8 characters";
                document.getElementById("lbPW").style.color = "Red";
                return ("too short");
            }
            if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbPW").innerHTML = "Password require at least 1 number";
                document.getElementById("lbPW").style.color = "red";
                return ("no number");
            }
            if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbPW").innerHTML = "Password require at least 1 lowercase letter";
                document.getElementById("lbPW").style.color = "red";
                return ("no lowercase");
            }
            if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbPW").innerHTML = "Password require at least 1 uppercase letter";
                document.getElementById("lbPW").style.color = "red";
                return ("no uppercase");
            }
            if (str.search(/^A-Za-z0-9]/) == -1) {
                document.getElementById("lbPW").innerHTML = "Password require at least 1 special character";
                document.getElementById("lbPW").style.color = "red";
                return ("no specialchar");
            }
            document.getElementById("lbPW").innerHTML = "Excellent!";
            document.getElementById("lbPW").style.color = "green";
        }
    </script>

    <div class="container">
        <div class="row">
            <h3>Registration</h3>
            <table class="nav-justified">
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 20px;">&nbsp;</td>
                    <td style="width: 174px; height: 20px;" class="modal-sm"></td>
                    <td style="width: 177px; height: 20px;"></td>
                    <td style="height: 20px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 20px">
                        <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td style="height: 20px; width: 174px;">
                        <asp:TextBox ID="tb_firstName" runat="server" Height="24px" Width="162px"></asp:TextBox>
                    </td>
                    <td style="height: 20px; width: 177px;">&nbsp;</td>
                    <td style="height: 20px">
                    </td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 28px;">
                    </td>
                    <td style="width: 174px; height: 28px;" class="modal-sm">
                        <asp:Label ID="lbFNError" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 28px;"></td>
                    <td style="height: 28px">
                        </td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 30px;">
                        <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td style="width: 174px; height: 30px;" class="modal-sm">
                        <asp:TextBox ID="tb_lastName" runat="server" Height="24px" Width="162px"></asp:TextBox>
                    </td>
                    <td style="width: 177px; height: 30px;"></td>
                    <td style="height: 30px">
                        </td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;">
                    </td>
                    <td style="width: 174px; height: 27px;" class="modal-sm">
                        <asp:Label ID="lbLNError" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 27px;"></td>
                    <td style="height: 27px">
                        </td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 30px;">
                        <asp:Label ID="Label3" runat="server" Text="Email Address"></asp:Label>
                    </td>
                    <td style="width: 174px; height: 30px;" class="modal-sm">
                        <asp:TextBox ID="tb_email" runat="server" Height="24px" Width="162px"></asp:TextBox>
                    </td>
                    <td style="width: 177px; height: 30px;">&nbsp;</td>
                    <td style="height: 30px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 28px;"></td>
                    <td style="width: 174px; height: 28px;">
                        <asp:Label ID="lbEmailError" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 28px;"></td>
                    <td style="height: 28px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 37px;">Date Of Birth (dd/mm/yyyy)</td>
                    <td class="calendar" style="width: 174px; height: 37px;">
                        <asp:TextBox ID="tb_DOB" runat="server" Height="24px" Width="162px" TextMode="Date"></asp:TextBox>
                    </td>
                    <td style="width: 177px; height: 37px;"></td>
                    <td style="height: 37px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;"></td>
                    <td class="calendar" style="width: 174px; height: 27px;">
                        <asp:Label ID="lbDOB" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 27px;"></td>
                    <td style="height: 27px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 30px;">Password</td>
                    <td style="width: 174px; height: 30px;" class="modal-sm">
                        <asp:TextBox ID="tb_password" runat="server" Height="24px" Width="162px" OnTextChanged="TextBox1_TextChanged" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox>
                    </td>
                    <td style="width: 177px; height: 30px;">
                        <asp:Button ID="checkPW" runat="server" OnClick="checkPW_Click" Text="Check" />
                    </td>
                    <td style="height: 30px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 20px;"></td>
                    <td style="width: 174px; height: 20px;" class="modal-sm">
                        <asp:Label ID="lbPW" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 20px;"></td>
                    <td style="height: 20px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;"></td>
                    <td style="width: 174px; height: 27px;" class="modal-sm">
                        <asp:Label ID="lbPWError" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 27px;"></td>
                    <td style="height: 27px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 20px;">Credit Card Number</td>
                    <td style="height: 20px; width: 174px">
                        <asp:TextBox ID="tb_creditCardNumber" runat="server" Height="24px" Width="162px"></asp:TextBox>
                    </td>
                    <td style="height: 20px; width: 177px">&nbsp;</td>
                    <td style="height: 20px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;"></td>
                    <td style="width: 174px; height: 27px;" class="modal-sm">
                        <asp:Label ID="lbCardError" runat="server"></asp:Label>
                    </td>
                    <td style="width: 177px; height: 27px;"></td>
                    <td style="height: 27px"></td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;">&nbsp;</td>
                    <td style="width: 174px; height: 27px;" class="modal-sm">
                        &nbsp;</td>
                    <td style="width: 177px; height: 27px;">&nbsp;</td>
                    <td style="height: 27px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;">&nbsp;</td>
                    <td style="width: 174px; height: 27px;" class="modal-sm">
                        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />    
                        <asp:Button ID="Register" runat="server" Height="30px" OnClick="Button1_Click1" Text="Register" Width="70px" />
                    </td>
                    <td style="width: 177px; height: 27px;">&nbsp;</td>
                    <td style="height: 27px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 27px;">&nbsp;</td>
                    <td style="width: 174px; height: 27px;" class="modal-sm"></td>
                    <td style="width: 177px; height: 27px;">&nbsp;</td>
                    <td style="height: 27px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="modal-sm" style="width: 186px; height: 20px;"></td>
                    <td style="width: 174px; height: 20px;" class="modal-sm">
                        &nbsp;</td>
                    <td style="width: 177px; height: 20px;"></td>
                    <td style="height: 20px">&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>