<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="asp.Pages.Admin" %>
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
        <asp:Panel visible=false ID="cars_panel" runat="server" style="margin-left: 10%;">
            <asp:DataList CssClass="w3-table-all w3-xlarge" style="width: 97%;" id="car_list" BorderColor="black"
                CellPadding="5" CellSpacing="5" RepeatColumns="5" RepeatDirection="Vertical" RepeatLayout="Table"
                runat="server" OnEditCommand="car_edit" OnUpdateCommand="car_update" OnDeleteCommand="car_delete"
                OnCancelCommand="car_cancel">
                <ItemTemplate>
                    Car Id: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "id") %></label>
                    <br />
                    Type Id: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "type_id") %></label>
                    <br />
                    Type Name: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "name") %></label>
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge"
                        id="car_edit_button" Text="Edit" CommandName="Edit" runat="server"
                        style="margin-right: 50%; margin-top: 1%;" />
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="w3-left">Car Id: </div>
                    <asp:Label class="w3-right" width=100 height=30 style="margin-right: 30%;" id="edit_car_id"
                        Text=<%# DataBinder.Eval(Container.DataItem, "id") %> runat="server" />
                    </asp:textbox>
                    <br />
                    <div class="w3-left">Type Id: </div>
                    <asp:textbox class="w3-input w3-border w3-light-grey w3-right" style="margin-right: 30%;" width=100
                        height=30 text=<%# DataBinder.Eval(Container.DataItem, "type_id") %> id="edit_car_type_id"
                        runat="server">
                    </asp:textbox>
                    <br />
                    Type Name: <label class="w3-right"
                        style="margin-right: 45%;"><%# DataBinder.Eval(Container.DataItem, "name") %></label>
                    <div>
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge"
                            id="car_update_button" Text="Update" CommandName="Update" runat="server"
                            style="margin-left: 3%; margin-top: 1%;" />
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge"
                            id="car_delete_button" Text="Delete" CommandName="Delete" runat="server"
                            style="margin-left: 3%; margin-top: 1%;" />
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge"
                            id="car_cancel_button" Text="Cancel" CommandName="Cancel" runat="server"
                            style="margin-left: 3%; margin-top: 1%;" />
                    </div>
                </EditItemTemplate>
            </asp:DataList>
            <div class="w3-left" style="font-size: 200%; margin-top: 1.5%;">Type Id: </div>
            <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                style="margin-left: 0.5%; margin-top: 1.6%; font-size: 200%;" width=100 height=30 id="add_car_type_id"
                runat="server" />
            <asp:Button class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-xlarge" id="add_button"
                Text="Add" style="margin-left: 1%; margin-top: 1%;" OnCommand="add_car" runat="server" />
            </div>
        </asp:Panel>
        <asp:Panel ID="car_types_panel" visible=false runat="server" style="margin-left: 10%;">
            <asp:DataList CssClass="w3-table-all w3-xlarge" style="width: 97%;" id="car_types_list" BorderColor="black"
                CellPadding="5" CellSpacing="5" RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Table"
                runat="server" OnEditCommand="car_types_edit" OnUpdateCommand="car_types_update"
                OnDeleteCommand="car_types_delete" OnCancelCommand="car_types_cancel">
                <ItemTemplate>
                    <div class="w3-row">
                        <div class="w3-col l4" style="margin-right: 3%;">
                            Type Id: <label class="w3-right"><%# DataBinder.Eval(Container.DataItem, "id") %></label>
                            <br />
                            Daily Cost: <label
                                class="w3-right"><%# DataBinder.Eval(Container.DataItem, "daily_cost") %></label>
                            <br />
                            Door: <label class="w3-right"><%# DataBinder.Eval(Container.DataItem, "door") %></label>
                            <br />
                            Passenger: <label
                                class="w3-right"><%# DataBinder.Eval(Container.DataItem, "passenger") %></label>
                        </div>
                        <div class="w3-col l6">
                            Transmission: <label
                                class="w3-right"><%# DataBinder.Eval(Container.DataItem, "transmission") %></label>
                            <br />
                            AC: <label class="w3-right"><%# DataBinder.Eval(Container.DataItem, "ac") %></label>
                            <br />
                            Fuel: <label class="w3-right"><%# DataBinder.Eval(Container.DataItem, "fuel") %></label>
                            <br />
                            Type Name: <label
                                class="w3-right"><%# DataBinder.Eval(Container.DataItem, "name") %></label>
                        </div>
                    </div>
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                        id="car_types_edit_button" Text="Edit" CommandName="Edit" runat="server"
                        style="margin-top: 1%;" />
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="w3-row">
                        <div class="w3-col l5" style="margin-right: 5%;">
                            <div class="w3-left">Type Id: </div>
                            <asp:Label class="w3-right" width=100 height=30 id="car_type_edit_id"
                                Text=<%# DataBinder.Eval(Container.DataItem, "id") %> runat="server" />
                            <br />
                            <div class="w3-left">Daily Cost: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "daily_cost") %> id="edit_daily_cost"
                                runat="server">
                            </asp:textbox>
                            <br />
                            <div class="w3-left">Door: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "door") %> id="edit_door" runat="server">
                            </asp:textbox>
                            <br />
                            <div class="w3-left">Passenger: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "passenger") %> id="edit_passenger"
                                runat="server">
                            </asp:textbox>
                        </div>
                        <div class="w3-col l6">
                            <div class="w3-left">Transmission: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "transmission") %> id="edit_transmission"
                                runat="server">
                            </asp:textbox>
                            <br />
                            <div class="w3-left">AC: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "ac") %> id="edit_ac" runat="server">
                            </asp:textbox>
                            <br />
                            <div class="w3-left">Fuel: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "fuel") %> id="edit_fuel" runat="server">
                            </asp:textbox>
                            <br />
                            <div class="w3-left">Name: </div>
                            <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" width=100 height=30
                                text=<%# DataBinder.Eval(Container.DataItem, "name") %> id="edit_name" runat="server">
                            </asp:textbox>
                        </div>
                    </div>
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                        id="car_types_update_button" Text="Update" CommandName="Update" runat="server"
                        style="margin-left: 3%; margin-top: 1%;" />
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                        id="car_types_delete_button" Text="Delete" CommandName="Delete" runat="server"
                        style="margin-left: 3%; margin-top: 1%;" />
                    <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                        id="car_types_cancel_button" Text="Cancel" CommandName="Cancel" runat="server"
                        style="margin-left: 3%; margin-top: 1%;" />
                </EditItemTemplate>
            </asp:DataList>
            <div class="w3-container w3-cell">
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%;">Daily Cost: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 1.5%; margin-top: 1.6%; font-size: 200%;" width=100 height=30
                        id="add_daily_cost" runat="server" />
                </div>
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%;">Door: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-top: 1.6%; font-size: 200%; margin-left: 16%;" width=100 height=30 id="add_door"
                        runat="server" />
                    <div class="w3-left" style="font-size: 200%; margin-top: 12%; margin-left: -58%;">Passenger: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-top: 1.6%; font-size: 200%; margin-top: 12%; margin-left: -26%;" width=100
                        height=30 id="add_passenger" runat="server" />
                </div>
            </div>
            <div class="w3-container w3-cell">
                <div class="w3-left" style="font-size: 200%; margin-top: 1.5%;">Transmission: </div>
                <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                    style="margin-left: 61%; margin-top: -14%; font-size: 200%;" width=100 height=30
                    id="add_transmission" runat="server" />
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%; margin-left: -7%;">AC: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 62%; margin-top: -16%; font-size: 200%;" width=100 height=30 id="add_ac"
                        runat="server" />
                </div>
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%; margin-left: -7%;">Fuel: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 62%; margin-top: -16%; font-size: 200%;" width=100 height=30 id="add_fuel"
                        runat="server" />
                </div>
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%; margin-left: -7%;">Name: </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 62%; margin-top: -16%; font-size: 200%;" width=100 height=30 id="add_name"
                        runat="server" />
                </div>
            </div>
            <asp:Button class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-xlarge"
                id="car_types_add_button" Text="Add" style="margin-left: 45%; margin-top: -5%;" OnCommand="add_car_type"
                runat="server" />
        </asp:Panel>
        <asp:Panel ID="bookings_panel" visible=false runat="server" style="margin-left: 10%;">
            <asp:DataList CssClass="w3-table-all w3-xlarge" style="width: 97%;" id="bookings_list" BorderColor="black"
                CellPadding="5" CellSpacing="5" RepeatColumns="5" RepeatDirection="Vertical" RepeatLayout="Table"
                runat="server" OnEditCommand="bookings_edit" OnUpdateCommand="bookings_update"
                OnDeleteCommand="bookings_delete" OnCancelCommand="bookings_cancel">
                <ItemTemplate>
                    Booking Id: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "id") %></label>
                    <br />
                    Car Id: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "car_id") %></label>
                    <br />
                    Days to Pick: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "days_to_pick") %></label>
                    <br />
                    Days to Drop: <label class="w3-right"
                        style="margin-right: 30%;"><%# DataBinder.Eval(Container.DataItem, "days_to_drop") %></label>
                    <div class="w3-container">
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                            id="bookings_button" Text="Edit" CommandName="Edit" runat="server" />
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="w3-left">Booking Id: </div>
                    <asp:Label class="w3-right" style="margin-right: 30%;" width=100 height=30 id="booking_id"
                        Text=<%# DataBinder.Eval(Container.DataItem, "id") %> runat="server" />
                    <br />
                    <div class="w3-left">Car Id: </div>
                    <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" style="margin-right: 30%;" width=100 height=30
                        text=<%# DataBinder.Eval(Container.DataItem, "car_id") %> id="edit_booking_car_id"
                        runat="server">
                    </asp:textbox>
                    <br />
                    <div class="w3-left">Days to Pick: </div>
                    <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" style="margin-right: 30%;" width=100 height=30
                        text=<%# DataBinder.Eval(Container.DataItem, "days_to_pick") %> id="edit_days_to_pick"
                        runat="server">
                    </asp:textbox>
                    <br />
                    <div class="w3-left">Days to Drop: </div>
                    <asp:textbox class="w3-input w3-border w3-light-grey w3-left w3-right" style="margin-right: 30%;" width=100 height=30
                        text=<%# DataBinder.Eval(Container.DataItem, "days_to_drop") %> id="edit_days_to_drop"
                        runat="server">
                    </asp:textbox>
                    <div class="w3-container">
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                            id="bookings_update_button" Text="Update" CommandName="Update" runat="server"
                            style="margin-left: 3%; margin-top: 5%;" />
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                            id="bookings_delete_button" Text="Delete" CommandName="Delete" runat="server"
                            style="margin-left: 3%; margin-top: 5%;" />
                        <asp:LinkButton class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-left"
                            id="bookings_cancel_button" Text="Cancel" CommandName="Cancel" runat="server"
                            style="margin-left: 3%; margin-top: 5%;" />
                    </div>
                </EditItemTemplate>
            </asp:DataList>
            <div class="w3-container w3-cell">
                <div class="w3-left" style="font-size: 200%; margin-top: 1.5%;">Car Id: </div>
                <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                    style="margin-left: 61%; margin-top: -14%; font-size: 200%;" width=100 height=30 id="add_car_id"
                    runat="server" />
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%; margin-left: -7%;">Days to Pick:
                    </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 62%; margin-top: -16%; font-size: 200%;" width=100 height=30
                        id="add_days_to_pick" runat="server" />
                </div>
                <div class="w3-container">
                    <div class="w3-left" style="font-size: 200%; margin-top: 1.5%; margin-left: -7%;">Days to Drop:
                    </div>
                    <asp:textbox class="w3-input w3-left w3-border w3-light-grey"
                        style="margin-left: 62%; margin-top: -16%; font-size: 200%;" width=100 height=30
                        id="add_days_to_drop" runat="server" />
                </div>
            </div>
            <asp:Button class="w3-button w3-dark-grey w3-light w3-hover-red w3-round-xxlarge w3-xlarge"
                id="bookings_add_button" Text="Add" style="margin-left: 45%; margin-top: -5%;" OnCommand="add_booking"
                runat="server" />
        </asp:Panel>
    </asp:Panel>
</asp:content>