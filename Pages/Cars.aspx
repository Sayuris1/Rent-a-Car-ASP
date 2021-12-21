<%@ Page Title="About" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="asp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <body class="w3-content w3-border-left w3-border-right">

        <!-- Sidebar/menu -->
        <nav class="w3-sidebar w3-light-grey w3-collapse w3-top" style="z-index:-1; width:260px; padding-top:80px;"
            id="mySidebar">
            <div class="w3-container w3-display-container w3-padding-16">
                <i onclick="w3_close()"
                    class="fa fa-remove w3-hide-large w3-button w3-transparent w3-display-topright"></i>
                <h3>Rental</h3>
                <h3>from $99</h3>
                <h6>per night</h6>
                <hr>
                <p><label><i class="fa fa-calendar-check-o"></i> Pick-Up</label></p>
                <input class="w3-input w3-border" type="text" placeholder="DD MM YYYY" name="CheckIn" required>
                <p><label><i class="fa fa-calendar-o"></i> Drop-Off</label></p>
                <input class="w3-input w3-border" type="text" placeholder="DD MM YYYY" name="CheckOut" required>
                <p><button class="w3-button w3-block w3-green w3-left-align" type="submit"><i
                            class="fa fa-search w3-margin-right"></i> Search availability</button></p>
            </div>
        </nav>

        <!-- !PAGE CONTENT! -->
        <asp:Repeater id="car_repeater" runat="server">
            <ItemTemplate>
                <div class="w3-main w3-white" style="margin-left:260px">

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