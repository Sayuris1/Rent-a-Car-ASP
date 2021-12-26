<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="asp.Pages.Admin" %>
<asp:content id="Content1" ContentPlaceHolderID="MainContent" runat="server"
    class="w3-content w3-border-left w3-border-right">
    <div class="w3-content" style="margin-top: 5%; margin-left: 17%;">
        <asp:Login id="admin_login" runat="server">
            <layouttemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                    <tbody>
                        <tr>
                            <td>
                                <table cellpadding="0" class="w3-table-all w3-xxlarge" style="width: 250%;">
                                    <tbody>
                                        <tr>
                                            <td class="w3-red" align="center" colspan="2">
                                                Log In
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="w3-row">
                                                    <asp:textbox
                                                        class="w3-input w3-border w3-light-grey w3-threequarter"
                                                        placeholder="User Name" id="UserName" runat="server">
                                                    </asp:textbox>
                                                    <asp:requiredfieldvalidator class="w3-rest" id="UserNameRequired"
                                                        runat="server" controltovalidate="UserName"
                                                        errormessage="User Name is required."
                                                        tooltip="User Name is required." validationgroup="admin_login">***
                                                    </asp:requiredfieldvalidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="w3-row">
                                                    <asp:textbox
                                                        class="w3-input w3-border w3-light-grey w3-threequarter"
                                                        placeholder="Password" id="Password" runat="server"
                                                        textmode="Password">
                                                    </asp:textbox>
                                                    <asp:requiredfieldvalidator class="w3-rest" id="PasswordRequired"
                                                        runat="server" controltovalidate="Password"
                                                        errormessage="Password is required."
                                                        tooltip="Password is required." validationgroup="admin_login">***
                                                    </asp:requiredfieldvalidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color: Red;">
                                                <asp:literal id="FailureText" runat="server" enableviewstate="False">
                                                </asp:literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="middle" colspan="2">
                                                <asp:button class="w3-button w3-red" id="LoginButton" runat="server"
                                                    commandname="Login" text="Log In" validationgroup="admin_login">
                                                </asp:button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </layouttemplate>

        </asp:Login>
    </div>
</asp:content>