<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_po_detail_print.aspx.cs" Inherits="frm_po_detail_print" Title=""   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 <%@ Register src="usercontrols/ctl_po_detail_view.ascx" tagname="ctl_po_detail_view" tagprefix="uc1" %> 
  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PO PRINT</title>
    
    <style type="text/css">

 p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
        .style4
        {
            font-size: 20pt;
            font-family: "Times New Roman", Times, serif;
        }
        .style5
        {
            height: 23px;
        }
        .style6
        {
            font-weight: bold;
            color: white;
            background-color: #41519A;
            font-style: normal;
            font-variant: normal;
            font-size: 11pt;
            line-height: normal;
            font-family: verdana;
        }
        </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

<table id="tblmaster" runat="server" class="tblmas" style="width: 100%">
        <tr>
            <td style="text-align: center">
                <table style="width:100%;">
                    <tr>
                        <td style="vertical-align: top; text-align: center;">
                                    Shun Shing Cement Mills Limited.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                    Land View Commercial Center (7Th Floor), <st1:street w:st="on"><st1:address w:st="on">
                    28 Gulshan Circle</st1:address></st1:Street>-2, Dhaka-1212, Telephone: 8817690-4<o:p></o:p></td>
                    </tr>
                    <tr>
                        <td class="style5">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style6" style="text-align: center">
                PURCHASE ORDER REPORT</td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    PURCHASE TYPE&nbsp; :<asp:Label ID="lblpotype" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td>
                    PLANT&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    :<asp:Label ID="lblplant" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td>
                    DATE&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    :<asp:Label ID="lblhdate" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td>
                    <asp:PlaceHolder ID="phreport" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>

