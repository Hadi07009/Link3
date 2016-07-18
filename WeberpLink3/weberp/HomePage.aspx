<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" ValidateRequest="false"  EnableEventValidation="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="UserControls/Footer.ascx" tagname="Footer" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <div>
            <table style="width: 100%; border:none;border-width:0px">
                <tr class="pagetopTest">
                    <td style="width:100px;height:80px;">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logo_120_100.png" />
                    </td>
                    <td colspan="4" style="border:none; vertical-align:bottom; font-weight:normal; font-size:20pt;text-align:right; font-family:'Clarke Serifa';color:white">                      
                        HRIS & PAYROLL SYSTEM
                    </td>
                </tr>
               <%-- <tr >
                    <td colspan="4" style="background-color:red" >
                        &nbsp;</td>
                </tr>--%>
                <tr>
                    <td class="pageLeftTest" ></td>
                    <td style="text-align:left;vertical-align:top">
                        <asp:Panel ID="PanelLeft" runat="server" Height="100%" Width="100%" CssClass="loginLeft">
                            <table style="width:99%;" >
                                <tr>
                                    <td colspan="3" style="text-align: left;">
                                        <%--<div style="margin-left:0px; height:350px">--%>
                                            <asp:Image ID="Image1" runat="server" BorderStyle="Solid" BorderWidth="0px" Height="400px" Width="100%" />
                                        <%--</div>--%>
                                        <cc1:SlideShowExtender ID="SlideShowExtender1" runat="server" AutoPlay="true" Loop="true"  SlideShowServiceMethod="GetSlides" TargetControlID="Image1">
                                        </cc1:SlideShowExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="auto-style10" style="font-family:'Helvetica Neue LT';color:#4285F4">                                        
                                       
                                         <p class="auto-style13">
                                             At link3, we understand the need to value our natural resources as much as we understand the value of our customers. We believe that a tiny change in our business process can make a huge difference in the
                                             quality of our service to our customers. Our effort to create a paperless office is ongoing; this will save lot of trees which would otherwise be cut down to manufacture paper and therefore have an impact on the entire ecosystem.                                             
                                         </p>
                                        
                                         <p class="MsoNormal">
                                             &nbsp;</p>
                                       
                                   </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                </td>
                                </tr>
                                <tr>
                                <td>
                                    
                                    &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                            </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                                
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="text-align:left;vertical-align:top"  >
                        
                        <asp:Panel ID="PanelRight" runat="server" Height="250px" Width="250px" BackColor="white"  style="text-align: left" CssClass="loginRight">
                           
                           <table style="width:99%;" >
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="User ID" Font-Names="Helvetica Neue LT"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtuserid" runat="server" Width="250px" Height="25px"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtuserid" WatermarkCssClass="Watermark" WatermarkText="Enter your user id.." />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Password" Font-Names="Helvetica Neue LT"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtpass" runat="server" TextMode="Password" Width="250px" Height="25px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lbl" runat="server" Text="                      Invalid userid or password..." Visible="False"
                                                            Width="193px" ForeColor="Red" Font-Names="Helvetica Neue LT"></asp:Label>
                                           </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Width="100px" Font-Names="Helvetica Neue LT"/>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                         
                        </asp:Panel>
                        
                    </td>
                    <td class="pageLeftTest"></td>
               </tr>
                <tr>
                <td  colspan="4">
                    <uc1:Footer ID="Footer1" runat="server" />
                </td>
            </tr>
            </table>
        </div>

    </form>
</body>
    <head runat="server">
        <title></title>
        <!-- Start WOWSlider.com HEAD section -->
        <link rel="stylesheet" type="text/css" href="engine1/style.css" />
        <script type="text/javascript" src="engine1/jquery.js"></script>
        <!-- End WOWSlider.com HEAD section -->


        <style type="text/css">
        .pagetopTest
        {
            background-color:#034EA2;
            padding-bottom: 0px;
            padding-left: 0px;
            border-bottom: #0480B6;
            width:300px;
            height:80px;
            }
        .pageLeftTest
            {
            width:150px;
            height:500px;
            background-color:whitesmoke;
            }
body {
    font: 14px/1.4 "HelveticaNeue","Helvetica Neue",Helvetica,sans-serif;
}
a {
    color: #ff1313;
}
h1 {
    color: #333333;
    font-size: 28px;
    font-weight: bold;
    margin-bottom: 15px;
}
.container {
    margin: 0 auto;
    padding: 10px;
    /*width: 990px;*/
    height:80px;
}
.container1 {
    margin: 0 auto;
    padding: 0px;
    /*width: 990px;*/
    height:20px;
   
}
header {
    background: #ff1313 none repeat scroll 0 0;
    float: left;
    width: 100%;
}
.logo {
    background: rgba(0, 0, 0, 0) url("../images/logo.png") no-repeat scroll 0 0;
    float: left;
    height: 109px;
    width: 375px;
}
.header_left {
    float: right;
    width: 575px;
}
.helpline {
    color: #000;
    float: right;
    font-size: 24px;
    font-weight: bold;
    padding-top: 15px;
    text-align: right;
    width: 600px;
}
nav {
    float: right;
    font: 600 15px/41px Arial,Helvetica,sans-serif;
    margin-top: 10px;
    width: 545px;
}
nav a {
    color: #fff;
    padding: 10px 12px;
    text-decoration: none;
}
nav a:hover {
    background-color: #fff;
    color: #ff1313;
}
#topHeading {
    font-size: 15px;
    line-height: 15px;
    margin-bottom: 1px;
}
#topHeading span {
    color: #ff1313;
    font-size: 23px;
    font-weight: bold;
}
.homeheading {
    font-size: 40px;
    font-weight: bold;
    line-height: 38px;
    padding: 0 0 15px;
}
.homeheading span {
    color: #f00;
}
.banner_left {
    float: left;
    margin-bottom: 20px;
    width: 685px;
}
.banner_right {
    float: left;
    margin-bottom: 20px;
    width: 305px;
}
.homeText {
    float: left;
    width: 660px;
}
aside {
    float: right;
    width: 306px;
}
.details {
    float: left;
    font-size: 18px;
    line-height: 28px;
    width: 660px;
}
.details span {
    font-size: 20px;
    font-weight: bold;
}
.details h2 {
    color: #ff1313;
    font-size: 24px;
    font-weight: bold;
    padding: 33px 0;
}
.details h3 {
    color: #666;
    font-size: 20px;
}
.apply a {
    color: #f00;
    font-size: 16px;
    font-weight: normal;
    padding-top: 10px;
}
.gettingStart {
    color: #000;
    font-size: 30px;
    font-weight: bold;
    padding: 5px 0;
}
.steps {
    background: #fff none repeat scroll 0 0;
    border: 1px solid #ccc;
    color: #333;
    float: left;
    font-size: 13px;
    height: 250px;
    margin-right: 15px;
    margin-top: 20px;
    padding: 15px 0;
    width: 317px;
}
.steps:hover {
    background-color: #ff1313;
    color: #000;
}
.steps:hover h3 {
    background-color: #fff;
    color: #000;
}
.steps:hover h2 a {
    color: #fff;
}
.steps ul {
    list-style: outside none circle;
    margin: 10px 0 0 20px;
    padding: 0 10px;
}
.steps li {
    font-size: 18px;
    padding: 3px 5px;
    text-align: left;
}
.steps li a {
    color: #000;
    text-decoration: none;
}
.steps li a:hover {
    text-decoration: underline;
}
.steps:hover li a {
    color: #fff;
}
.steps:hover .note {
    background: #fff none repeat scroll 0 0;
    color: #000;
}
.note {
    background-color: #ff1313;
    border: 1px solid #ccc;
    color: #fff;
    margin: 20px auto 0;
    padding: 10px;
    width: 260px;
}
.steps h3 {
    background-color: #ff1313;
    color: #fff;
    font-size: 17px;
    font-weight: bold;
    margin-bottom: 30px;
    margin-left: 5px;
    padding: 5px 0;
    text-align: center;
    width: 80px;
}
.steps h2 a {
    color: #333;
    font-size: 33px;
    font-weight: bold;
    text-align: center;
    text-decoration: none;
    width: 100%;
}
footer {
    background: #222222 none repeat scroll 0 0;
    float: left;
    margin-top: 25px;
    width: 100%;
}
footer .box {
    float: left;
    padding-bottom: 80px;
    width: 350px;
}
footer .box h2 {
    color: #fff;
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 10px;
}
footer .box h3 {
    color: #fff;
    font-size: 16px;
    padding-bottom: 10px;
    padding-top: 30px;
}
footer .box .content {
    color: #999;
    font-size: 13px;
    padding-left: 0;
    padding-right: 50px;
}
.socialMedia {
    float: right;
    width: 200px;
}
.facebook {
    background: rgba(0, 0, 0, 0) url("../images/fb.png") repeat scroll 0 0;
    height: 41px;
    margin-bottom: 15px;
    margin-top: 15px;
    width: 219px;
}
.facebook:hover {
    background: rgba(0, 0, 0, 0) url("../images/fb_hover.png") repeat scroll 0 0;
}
.rights {
    color: #666;
    float: left;
    font-size: 12px;
    padding: 95px 0 15px;
    width: 100%;
}
.button {
    border-radius: 12px;
    font-size: 22px;
    font-weight: bold;
    height: 44px;
    margin-top: 15px;
    padding-top: 14px;
    text-align: center;
    width: 250px;
}
.button a {
    color: #fff;
    text-decoration: none;
}
.greyButton {
    background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #232323, #040404) repeat scroll 0 0;
    border: 1px solid #111;
}
.greyButton:hover {
    background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #e60000, #9e0001) repeat scroll 0 0;
}
.redButton {
    background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #e60000, #9e0001) repeat scroll 0 0;
}
.redButton:hover {
    background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #232323, #040404) repeat scroll 0 0;
    border: 1px solid #111;
}
.loginLeft {
    float: left;
    margin-right: 30px;
    width: 800px;
}
.loginLeft h2 {
    font-size: 24px;
    font-weight: bold;
    padding: 20px 0;
}
.loginLeft h3 {
    color: #666;
    font-size: 20px;
    font-weight: bold;
}
.loginRight {
    background-color: #f5f5f5;
    float: right;
    margin-right: 10px;
    padding: 20px;
    width: 250px;
}
.forgotPwd {
    background-color: #f5f5f5;
    margin: 80px auto;
    padding: 20px;
    width: 360px;
}
.signupRight {
    float: right;
    margin-right: 30px;
    width: 283px;
}
.signupRight h2 {
    font-size: 24px;
    font-weight: bold;
    padding: 20px 0;
}
.signupRight h3 {
    color: #333;
    font-size: 18px;
    font-weight: bold;
}
.signupLeft {
    background-color: #f5f5f5;
    float: left;
    margin-right: 10px;
    padding: 20px;
    width: 600px;
}
#eligible {
    font-size: 16px;
    margin-bottom: 10px;
}
#eligible span {
    color: #ff1313;
    font-size: 16px;
    font-weight: bold;
}
.address {
    line-height: 20px;
    margin-left: 20px;
    margin-top: 35px;
}
.address h3 {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 10px;
}
.help {
    font-style: italic;
    margin: 20px 0 20px 20px;
}
.collapsible {
    background-color: #ff1313;
    color: #fff;
    cursor: pointer;
    font-size: 15px;
    font-weight: bold;
    margin-top: 5px;
    padding: 8px 12px;
}
.contents {
    background: #333 none repeat scroll 0 0;
    color: #fff;
    padding: 15px;
}
.contents p {
    padding: 8px 0;
}
.contents li {
    list-style: outside none decimal;
    margin-left: 30px;
    padding-bottom: 8px;
}
th {
    background: #333 none repeat scroll 0 0;
    border-bottom: 1px solid #ccc;
    color: #fff;
    font-size: 14px;
    font-weight: bold;
    padding: 10px;
}
td {
    /*border-bottom: 1px solid #ccc;*/
    font-size: 13px;
    padding: 5px;
}
.container h2 {
    font-size: 18px;
    font-weight: bold;
    padding: 20px 0 10px;
}
.container li {
    list-style: outside none circle;
    margin-left: 30px;
    padding: 5px;
}
#slides {
    display: none;
}
#slides a {
    display: none !important;
}
#slides li {
    display: none !important;
}
.msg {
    background: #ffe1e1 none repeat scroll 0 0;
    border: 1px solid #666;
    color: #900;
    margin-left: 10px;
    padding: 5px 10px;
    width: 87%;
}
.loginBullets {
    font-size: 16px;
    margin-bottom: 50px;
    margin-top: 30px;
}
.dashboard_left {
    float: left;
    width: 600px;
}
.dashboard_left form {
    background-color: #f5f5f5;
    padding: 15px 0;
}
.dashboard_left h1 {
    font-size: 24px;
    margin-bottom: 25px;
}
.dashboard_left h2 {
    border-bottom: 1px solid #999;
    font-size: 18px;
    font-weight: normal;
    margin: 0 15px 15px;
    padding: 10px 0 0;
}
.welcome {
    float: left;
    margin: 10px 0 25px;
    width: 100%;
}
.welcome span {
    font-weight: bold;
    margin-left: 0;
}
.dashboard_box {
    background-color: #ccc;
    border-bottom: 1px solid #666666;
    float: left;
    font-family: Arial,Helvetica,sans-serif;
    height: 70px;
    padding: 35px 0;
    width: 100%;
}
.dashboard_box h5 {
    color: #666666;
    font-family: Arial,Helvetica,sans-serif;
    font-size: 22px;
    margin-left: 20px;
}
.dashboard_box h5 span {
    color: #fff;
}
.currentStep {
    background-color: #83bb00;
}
.currentStep h5 {
    color: #000;
}
.dashboard_box h5 strong {
    font-weight: bold;
}
.dashboard_box p {
    color: #333;
    margin: 0 20px;
}
.currentStep p {
    color: #000;
}
.dashboard_right {
    float: right;
    width: 300px;
}
.dashboard_right h2 {
    padding: 10px 0 0 5px;
}
.dashboard_right ul {
    list-style: outside none none;
    margin: 0;
    padding: 0;
}
.dashboard_right li {
    list-style: outside none none;
    margin-left: 0;
}
.dashboard_right li a {
    border-bottom: 1px solid #333;
    color: #333;
    float: left;
    padding: 12px;
    text-decoration: none;
    width: 90%;
}
.dashboard_right li a:hover {
    background-color: #333;
    color: #fff;
}
.authorized {
    float: left;
    line-height: 18px;
    width: 350px;
}
.authorized h3 {
    font-size: 16px;
    font-weight: bold;
    padding-bottom: 10px;
    padding-top: 30px;
}
.fWorker {
    font-size: 18px;
    font-weight: bold;
    margin-top: 15px;
}
.fWorker li {
    list-style: outside none decimal;
}
.fWorker li span {
    font-size: 14px;
    font-weight: normal;
}
.notNeed h4 {
    font-size: 16px;
    font-weight: bold;
    margin: 20px 0 10px;
}
#UploadForm p {
    font-weight: bold;
    margin: 0 15px;
}
.addfiles {
    float: left;
    margin: 10px 0 15px;
    width: 100%;
}
.addfiles span {
    margin: 10px 8px 15px 15px;
}
.loginNav {
    float: right;
    text-align: right;
    width: 100%;
}
.Supportbtn {
    float: left;
    margin-bottom: 15px;
    width: 100%;
}
.Supportbtn ul {
    margin: 0;
    padding: 0;
}
.Supportbtn li {
    float: left;
    list-style: outside none none;
    margin-left: 5px;
}
.Supportbtn li a {
    color: #333;
    margin: 3px 10px;
    text-decoration: none;
}
.compose a {
    background-color: #333;
    color: #fff;
    padding: 6px 10px;
    text-decoration: none;
}
.msgBld {
    font-weight: bold;
}
.msgDetail a {
    color: #000;
    text-decoration: none;
}
.msgDetail a:hover {
    text-decoration: underline;
}
.msgSubject {
    background-color: #fff;
    float: left;
    font-size: 15px;
    font-weight: bold;
    margin-bottom: 15px;
    margin-left: 15px;
    padding: 7px 10px;
    width: 600px;
}
.msgText {
    background-color: #fff;
    border-bottom: 1px dotted #666;
    float: left;
    font-size: 14px;
    margin-bottom: 15px;
    margin-left: 15px;
    padding: 7px 10px;
    width: 600px;
}
.dtmsg {
    float: left;
    font-size: 14px;
    padding-bottom: 5px;
    padding-right: 20px;
    text-align: right;
    width: 97%;
}
.auto-style10 {
            text-align: justify;
        }
            .auto-style12 {
                width: 800px;
            }
            p.MsoNormal
	            {margin-top:0in;
	            margin-right:0in;
	            margin-bottom:8.0pt;
	            margin-left:0in;
	            line-height:107%;
	            font-size:11.0pt;
	            font-family:'Helvetica Neue LT';
	            }
            .auto-style13 {
                font-size: 11.0pt;
                font-family: Calibri, sans-serif;
                margin-left: 0in;
                margin-right: 0in;
                margin-top: 0in;
                margin-bottom: .0001pt;
            }
            </style>
        <script runat="Server" type="text/C#">
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static Slide[] GetSlides()
        {
            var slides = new Slide[15];

            slides[0] = new Slide("ImagesSlide/01.jpg", "Blue Hills", "Go Blue");
            slides[1] = new Slide("ImagesSlide/02.jpg", "Sunset", "Setting sun");
            slides[2] = new Slide("ImagesSlide/03.jpg", "Winter", "Wintery...");
            slides[3] = new Slide("ImagesSlide/04.jpg", "Water lillies", "Lillies in the water");
            slides[4] = new Slide("ImagesSlide/05.jpg", "Sedona", "Portrait style picture");
            slides[5] = new Slide("ImagesSlide/06.jpg", "Sedona1", "Portrait style picture1");
            slides[6] = new Slide("ImagesSlide/07.jpg", "Sedona1", "Portrait style picture1");
            slides[7] = new Slide("ImagesSlide/08.jpg", "Sedona1", "Portrait style picture1");
            slides[8] = new Slide("ImagesSlide/09.jpg", "Sedona1", "Portrait style picture1");
            slides[9] = new Slide("ImagesSlide/10.jpg", "Sedona1", "Portrait style picture1");
            slides[10] = new Slide("ImagesSlide/11.jpg", "Sedona1", "Portrait style picture1");
            slides[11] = new Slide("ImagesSlide/12.jpg", "Sedona1", "Portrait style picture1");
            slides[12] = new Slide("ImagesSlide/13.jpg", "Sedona1", "Portrait style picture1");
            slides[13] = new Slide("ImagesSlide/14.jpg", "Sedona1", "Portrait style picture1");
            slides[14] = new Slide("ImagesSlide/15.jpg", "Sedona1", "Portrait style picture1");
            return(slides);
        }
         </script>
    </head>
</html>
