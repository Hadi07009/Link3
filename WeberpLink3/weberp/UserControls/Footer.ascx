<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="footer" %>
<!--Begin Footer-->

<div  class="footerwrap">
<br />
<img src="./Images/returntop.gif" alt="return to top" align="middle" border="0" /><a class="dt2" title="Return to top of the page" href="javascript:scroll(0,0)">Return to top</a>
<br />
<span class="content2">
Copyright ©2010 Link3 Technologies Ltd. All rights reserved. </span>
<br />
  <asp:HyperLink id="Powered" cssClass="dt2" ToolTip="Visit our portal website" NavigateURL="http://www.link3.net"  onclick="window.open(this.href,'popupwindow','width=400,height=400,titlebar=yes,toolbar=yes,scrollbars,resizable'); return false;" runat="server"> Developed By Link3 Software Department. </asp:HyperLink>
 </div>
<!--End Footer-->
