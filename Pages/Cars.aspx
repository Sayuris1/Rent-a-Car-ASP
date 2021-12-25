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
                        border-collapse: collapse; width: 350px;" OnDayRender="OnDayRender"
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
                        border-collapse: collapse; width: 350px;" OnDayRender="OnDayRender"
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

                    <img class="w3-image" src="/jpgs/start_car.jpg" alt="The Car"
                        style="min-width:1000px; margin-left:30px; padding-top:80px;" width="1500" height="800">
                    <div class="w3-container">
                        <h4><strong>The space</strong></h4>
                        <div class="w3-row w3-large">
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-male"></i> Max people: 4</p>
                                <p><i class="fa fa-fw fa-bath"></i> Bathrooms: 2</p>
                                <p><i class="fa fa-fw fa-bed"></i> Bedrooms: 1</p>
                            </div>
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-clock-o"></i> Check In: After 3PM</p>
                                <p><i class="fa fa-fw fa-clock-o"></i> Check Out: 12PM</p>
                            </div>
                        </div>
                        <hr>

                        <h4><strong>Amenities</strong></h4>
                        <div class="w3-row w3-large">
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-shower"></i> Shower</p>
                                <p><i class="fa fa-fw fa-wifi"></i> WiFi</p>
                                <p><i class="fa fa-fw fa-tv"></i> TV</p>
                            </div>
                            <div class="w3-col s6">
                                <p><i class="fa fa-fw fa-cutlery"></i> Kitchen</p>
                                <p><i class="fa fa-fw fa-thermometer"></i> Heating</p>
                                <p><i class="fa fa-fw fa-wheelchair"></i> Accessible</p>
                            </div>
                        </div>
                        <hr>

                        <asp:Button ID="reserve_button" runat="server" Text=" Click to Reserve Right Now !! "
                            class="w3-button w3-deep-orange"
                            style="margin-left: 35%; padding-right: 5%; padding-left: 5%;" Font-Size="Large"
                            OnCommand="reserve_clicked" CommandArgument='<%# Container.ItemIndex + 1%>' />
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