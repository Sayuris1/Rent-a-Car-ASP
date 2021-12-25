<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="asp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Header Search Availability -->
  <header class="w3-display-container w3-content" style="max-width:1500px;">
    <img class="w3-image" src="/jpgs/start_car.jpg" alt="The Car" style="min-width:1000px" width="1500" height="800">

    <div class="w3-display-middle w3-padding w3-col l9">
      <div class="w3-container w3-red">
        <h2><i class="fa fa-car w3-margin-right"></i>Rent a Car Today !!</h2>
      </div>

      <!-- Calendars -->
      <div class="w3-container w3-white" style="padding-top: 20px; padding-left: 10%;">
        <div class="w3-row-padding">
          <!-- Pick-Up -->
          <div class="w3-half w3-row-padding">
            <div style="padding-bottom:20px; margin-left: 30%;">
              <label style="font-size: large;"><i class="fa fa-calendar-o"></i> Pick-Up</label>
            </div>
            <!-- Calendar -->
            <div class="calendarWrapper">
              <asp:Calendar ID="calendar_pick" runat="server" CssClass="myCalendar" DayNameFormat="Short"
                Font-Names="Tahoma" cellspacing="0" cellpadding="0" title="Calendar" style="border-width: 1px; border-style: solid; font-family: Tahoma; 
                border-collapse: collapse;" OnDayRender="pick_render" OnSelectionChanged="selection_changed">
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

          <!-- Drop-Off -->
          <div class="w3-half w3-row-padding">
            <div style="padding-bottom:20px; margin-left: 30%;">
              <label style="font-size: large;"><i class="fa fa-calendar-o"></i> Drop-Off</label>
            </div>

            <!-- Calendar -->
            <div class="calendarWrapper">
              <asp:Calendar ID="calendar_drop" runat="server" CssClass="myCalendar" DayNameFormat="Short"
                Font-Names="Tahoma" cellspacing="0" cellpadding="0" title="Calendar" style="border-width: 1px; border-style: solid; font-family: Tahoma; 
                border-collapse: collapse;" OnDayRender="drop_render" OnSelectionChanged="selection_changed">
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

        <div style="padding: 3%; margin-left: 30%;">
          <asp:Button ID="search_button" runat="server" Text=" Search A Car To Rent "
            class="w3-button w3-dark-grey w3-margin-right" Font-Size="Large" OnClick="search_clicked" />
        </div>
      </div>
    </div>
  </header>

  <!-- Page content -->
  <div class="w3-content" style="max-width:1532px;">

    <div class="w3-row-padding" id="about">
      <!-- About -->
      <div class="w3-col m3">
        <h3>About
        </h3>
        <h6>One does not have to waste time checking the schedules of buses or estimating the taxi prices. Which is why
          Auto Rental services will provide a diverse range of deals and offers for your traveling convenience.<br>
          Stands for exciting mobility and tailored solutions.<br> We lead in the car rental industry. High-quality
          customer service and seamless rental experience.<br> We always have vehicles available and in stock.<br> Many
          different makes and models to choose from</h6>
        <p class="w3-center">We accept: <i class="fa fa-credit-card w3-large"></i> <i
            class="fa fa-cc-mastercard w3-large"></i> <i class="fa fa-cc-amex w3-large"></i> <i
            class="fa fa-cc-cc-visa w3-large"></i><i class="fa fa-cc-paypal w3-large"></i></p>
      </div>

      <!-- Image of location/map -->
      <div class="w3-col m4">
        <h3>Our Location:</h3>
        <img class="w3-image" src="/jpgs/location.jpg" alt="The Hotel" style="min-width:1000px" width="1500"
          height="800">
      </div>
    </div>

    <div class="w3-panel w3-red w3-leftbar w3-padding-32">
      <h6><i class="fa fa-info w3-deep-orange w3-padding w3-margin-right"></i> On demand, we can offer Playstation, snow
        chains, children care, dog equipment, etc.</h6>
    </div>
  </div>

</asp:Content>