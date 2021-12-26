<%@ Page EnableEventValidation="false" Title="About" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="asp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <body class="w3-content w3-border-left w3-border-right">

        <!-- Sidebar/menu -->
        <div class="w3-sidebar w3-light-grey" style="width: 21.5%; margin-top:40px;" id="mySidebar">
            <div class="w3-container w3-display-container">
                <h3>Rental starts at $99</h3>
                <p><label style="font-size: large;"><i class="fa fa-calendar-check-o"></i> Pick-Up</label></p>
                <!-- Calendar -->
                <div class="calendarWrapper">
                    <asp:Calendar ID="calendar_pick" runat="server" CssClass="myCalendar" DayNameFormat="Short"
                        Font-Names="Tahoma" cellspacing="0" cellpadding="0" title="Calendar" style="border-width: 1px; border-style: solid; font-family: Tahoma; 
                        border-collapse: collapse; width: 350px;" OnDayRender="pick_render"
                        OnSelectionChanged="selection_changed">
                        <OtherMonthDayStyle CssClass="calDay otherMonthDay" />
                        <DayStyle CssClass="calDay" />
                        <DayHeaderStyle CssClass="calDayHeader" ForeColor="#2d3338" />
                        <SelectedDayStyle Font-Bold="True" CssClass="calDaySelected" />
                        <TodayDayStyle CssClass="calToday" />
                        <SelectorStyle CssClass="calSelector" />
                        <NextPrevStyle CssClass="calNextPrev" />
                        <TitleStyle CssClass="calTitle" />
                    </asp:Calendar>
                </div>
                <p><label style="font-size: large;"><i class="fa fa-calendar-o"></i> Drop-Off</label></p>
                <!-- Calendar -->
                <div class="calendarWrapper">
                    <asp:Calendar ID="calendar_drop" runat="server" CssClass="myCalendar" DayNameFormat="Short"
                        Font-Names="Tahoma" cellspacing="0" cellpadding="0" title="Calendar" style="border-width: 1px; border-style: solid; font-family: Tahoma; 
                        border-collapse: collapse; width: 350px;" OnDayRender="drop_render"
                        OnSelectionChanged="selection_changed">
                        <OtherMonthDayStyle CssClass="calDay otherMonthDay" />
                        <DayStyle CssClass="calDay" />
                        <DayHeaderStyle CssClass="calDayHeader" ForeColor="#2d3338" />
                        <SelectedDayStyle Font-Bold="True" CssClass="calDaySelected" />
                        <TodayDayStyle CssClass="calToday" />
                        <SelectorStyle CssClass="calSelector" />
                        <NextPrevStyle CssClass="calNextPrev" />
                        <TitleStyle CssClass="calTitle" />
                    </asp:Calendar>
                </div>
            </div>
        </div>

        <!-- !PAGE CONTENT! -->
        <asp:Repeater id="car_repeater" runat="server" OnItemCommand="item_command">
            <ItemTemplate>
                <div class="w3-main w3-white" style="margin-left:400px">

                    <asp:Image ID="car_img" runat="server" class="w3-image" style="margin-left: 5%;" AlternateText="Car Img" />
                    <div class="w3-container w3-center">
                        <h4 class="w3-center" style="font-size: xx-large;"><strong><%#Eval("name") %></strong></h4>
                        <div class="w3-row w3-large">
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-usd"></i> Daily Cost: <%#Eval("daily_cost") %></p>
                                <p><i class="fa fa-fw fa-folder"></i> Doors: <%#Eval("door") %></p>
                                <p><i class="fa fa-fw fa-user-o"></i> Passengers: <%#Eval("passenger") %></p>
                            </div>
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-gears"></i> Transmission: <%#Eval("transmission") %></p>
                                <p><i class="fa fa-fw fa-thermometer-full"></i> AC: <%#Eval("ac") %></p>
                                <p><i class="fa fa-fw fa-battery-full"></i> Fuel: <%#Eval("fuel") %></p>
                            </div>
                        </div>
                        <hr>

                        <h4><strong>Amenities</strong></h4>
                        <div class="w3-row w3-large">
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-signal"></i> Navigation</p>
                                <p><i class="fa fa-fw fa-wifi"></i> WiFi</p>
                                <p><i class="fa fa-fw fa-tv"></i> TV</p>
                            </div>
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-cutlery"></i> Meals</p>
                                <p><i class="fa fa-fw fa-thermometer"></i> Heating</p>
                                <p><i class="fa fa-fw fa-wheelchair"></i> Accessible</p>
                            </div>
                        </div>
                        <hr>

                        <asp:Button ID="reserve_button" runat="server" Text=" Click to Reserve Right Now !! "
                            class="w3-button w3-deep-orange" style="padding-right: 5%; padding-left: 5%;"
                            Font-Size="Large" OnCommand="reserve_clicked"
                            CommandArgument='<%# Container.ItemIndex + 1%>' />
                        <h4><strong>Extra Info</strong></h4>
                        <p>Our apartment is really clean and we like to keep it that way. Enjoy the lorem ipsum dolor
                            sit
                            amet,
                            consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna
                            aliqua.
                            Ut
                            enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
                            commodo
                            consequat.</p>
                        <p>We accept: <i class="fa fa-credit-card w3-large"></i> <i
                                class="fa fa-cc-mastercard w3-large"></i> <i class="fa fa-cc-amex w3-large"></i> <i
                                class="fa fa-cc-cc-visa w3-large"></i><i class="fa fa-cc-paypal w3-large"></i></p>
                        <hr>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </body>
</asp:Content>