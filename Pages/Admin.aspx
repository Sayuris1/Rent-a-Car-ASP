﻿<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="asp.Pages.Admin" %>
<asp:content id="Content1" ContentPlaceHolderID="MainContent" runat="server"
    class="w3-content w3-border-left w3-border-right">
    <div class="w3-content" style="margin-top: 5%; margin-left: 17%;">
        <asp:Login id="admin_login" runat="server" OnAuthenticate="on_authenticate" OnLoggedIn="on_logged_in">
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
                                                        tooltip="User Name is required." validationgroup="admin_login">
                                                        ***
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
                                                        tooltip="Password is required." validationgroup="admin_login">
                                                        ***
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
    <!-- Admin Control Panel If Logged In -->
    <asp:Panel ID="logged_in_panel" runat="server" style="margin-top: -2.7%;">
        <div class="w3-sidebar w3-bar-block w3-light-grey w3-card" style="width:130px">
            <h5 class="w3-bar-item">Tables</h5>
            <asp:Button CssClass="w3-bar-item w3-button w3-hover-red" id="car_types_tab" Text="Car Types"
                OnClick="car_types_tab_clicked" runat="server" />
            <asp:Button CssClass="w3-bar-item w3-button w3-hover-red" id="cars_tab" Text="Cars"
                OnClick="cars_tab_clicked" runat="server" />
            <asp:Button CssClass="w3-bar-item w3-button w3-hover-red" id="Bookings_tab" Text="Bookings"
                OnClick="bookings_tab_clicked" runat="server" />
        </div>

        <asp:Panel ID="home" runat="server" style="margin-left: 10%;">
            <div class="w3-panel w3-red" style="width: 96%;">
                <h1>Login Succeeded</h1>
                <h3>Please select a table to edit from the navigation bar.</h3>
            </div>
        </asp:Panel>
        <asp:Panel ID="cars_panel" runat="server" style="margin-left: 10%;">
            <asp:DataList CssClass="w3-table-all w3-xlarge" style="width: 97%;" id="car_list" BorderColor="black"
                CellPadding="5" CellSpacing="5" RepeatDirection="Vertical" RepeatLayout="Table" runat="server"
                OnEditCommand="edit_command" OnUpdateCommand="update_command" OnDeleteCommand="delete_command"
                OnCancelCommand="cancel_command">
                <ItemTemplate>
                    Car Id: <%# DataBinder.Eval(Container.DataItem, "id") %>
                    <br />
                    Type Id: <%# DataBinder.Eval(Container.DataItem, "type_id") %>
                    <br />
                    Type Name: <%# DataBinder.Eval(Container.DataItem, "name") %>
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-right"
                        id="car_edit" Text="Edit" CommandName="Edit" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="w3-left">Car Id: </div>
                    <asp:Label class="w3-left" width=100 height=30 style="margin-left: 0.5%;"
                        id="edit_car_id" Text=<%# DataBinder.Eval(Container.DataItem, "id") %> runat="server" />
                    </asp:textbox>
                    <br />
                    <div class="w3-left">Type Id: </div>
                    <asp:textbox class="w3-input w3-border w3-light-grey w3-left" style="margin-left: 0.5%;" width=100
                        height=30 text=<%# DataBinder.Eval(Container.DataItem, "type_id") %> id="edit_car_type_id"
                        runat="server">
                    </asp:textbox>
                    <br />
                    Type Name: <%# DataBinder.Eval(Container.DataItem, "name") %>
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-right"
                        id="car_update" Text="Update" CommandName="Update" runat="server" style="margin-left: 3%;" />
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-right"
                        id="car_delete" Text="Delete" CommandName="Delete" runat="server" style="margin-left: 3%;" />
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-right"
                        id="car_cancel" Text="Cancel" CommandName="Cancel" runat="server" />
                </EditItemTemplate>

            </asp:DataList>
        </asp:Panel>
    </asp:Panel>
</asp:content>