<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmEmployeeInformation.aspx.cs" Inherits="modules_HRMS_Setup_frmEmployeeInformation" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>




<%@ Register Src="../../../UserControls/ucOthersDocument.ascx" TagName="ucOthersDocument" TagPrefix="uc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../script/jquery.MultiFile.js" type="text/javascript"></script>

    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <cc1:ConfirmBox ID="ConfirmBox1" runat="server" />

    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>

            <style type="text/css">
                .popupform {
                    background-color: #fff;
                    font-family: 'Helvetica Neue LT';
                    border-width: 4px;
                    border-color: gray;
                    border-style: solid;
                    padding: 4px;
                    padding-top: 2px;
                    height: 100%;
                    z-index: 10;
                }

                .auto-style6 {
                    width: 180px;
                }

                .auto-style14 {
                    width: 1px;
                }
            </style>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text="Employee Information" runat="server" />
            </asp:Panel>

            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label99" runat="server" Text="Employee Search"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td align="left">

                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtEmployeeSearch" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeSearch_TextChanged" placeholder="Employee Code" Width="350px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeSearch_AutoCompleteExtender" runat="server"
                                            CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            CompletionListItemCssClass="autocomplete_listItem2"
                                            DelimiterCharacters=""
                                            Enabled="True"
                                            MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeSearch">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn2" Height="28px" OnClick="btnShow_Click" Text="Show Details " Width="120px" />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>

                        </td>
                        <td align="left">
                            <asp:Button ID="btnClear" runat="server" CssClass="btn2" Height="28px" OnClick="btnClear_Click" Text="Clear" Width="120px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="4">
                            <asp:Panel ID="PanelLeaveHdr0" runat="server" CssClass="cpHeaderContent" Width="100%">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td class="auto-style14">&nbsp;</td>
                        <td align="left">
                            <asp:Button ID="btnNewEmployee" runat="server" CssClass="btn2" Height="28px" OnClick="btnNewEmployee_Click" OnClientClick="ShowConfirmBox(this,'Are you Sure Save Employee Information?'); return false;" Text="Save Information" Width="150px" />
                            &nbsp;<asp:Button ID="btnReport" runat="server" CssClass="btn2" Height="28px" OnClick="btnReport_Click" Text="Report" Width="150px" />
                        </td>
                        <td align="right">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label11" runat="server" Text="Select Company"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlcompany" runat="server" AutoPostBack="True" CssClass="tbl" OnSelectedIndexChanged="ddlcompany_SelectedIndexChanged" placeholder="Select Company" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td rowspan="23" style="vertical-align: top">
                            <asp:Panel ID="Panel11" runat="server" Height="100%" Width="100%">
                                <table style="width: 100%; text-align: left">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblImage" runat="server" BorderColor="Black" BorderWidth="0px" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Red" Height="155px" Style="text-align: center; vertical-align: middle" Width="155px"> <br /> Photo
                                             <br />  Not <br />  Available  
                                             
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="ProfileImageUpload" runat="server" Width="100%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:ImageButton ID="imgBtnProfileUpload" runat="server" ImageUrl="~/Images/imageup.jpg" OnClick="imgBtnProfileUpload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RevImg" runat="server" ControlToValidate="ProfileImageUpload" ErrorMessage="Invalid File!(only  .gif, .jpg, .jpeg, .bmp, .png  Files are supported)" ForeColor="Red" ValidationExpression="^.+(.jpg|.JPG|.gif|.GIF|.jpeg|JPEG| .bmp|BMP| .png|PNG)$" Width="100%"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="dvMsg" style="background-color: Red; color: White; width: 190px; padding: 3px; display: none;">
                                                Maximum size allowed is 500 kb
                                            </div>
                                            <asp:Label ID="Label98" runat="server" Text="[150X150,  Files are supported]"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label12" runat="server" Text="Employee ID"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeCode" runat="server" AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged" placeholder="Employee Code" Width="350px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeCode_AutoCompleteExtender" runat="server" BehaviorID="txtEmployeeCode_AutoCompleteExtender" Enabled="true" MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeCode">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label13" runat="server" Text="First Name"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeFirstName" runat="server" placeholder="First Name" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label97" runat="server" Text="Last Name"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeLastName" runat="server" placeholder="Last Name" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label14" runat="server" Text="Joining Date"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:TextBox ID="popupJoiningDate" runat="server" placeholder="Joining Date" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupJoiningDate_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupJoiningDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label15" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem Value="S">Casual</asp:ListItem>
                                <asp:ListItem Value="C">Permanent</asp:ListItem>
                                <asp:ListItem Value="B">Probationary</asp:ListItem>
                                <asp:ListItem Value="P">Contratctual</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label16" runat="server" Text="Office Location"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlOfficeLocation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeLocation_SelectedIndexChanged" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label17" runat="server" Text="Work Location"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlWorkLocation" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label18" runat="server" Text="Department Code"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlDepartmentCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartmentCode_SelectedIndexChanged" Style="margin-top: 0px" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label19" runat="server" Text="Section Code"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlSectionCode" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label20" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlDesignation" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label21" runat="server" Text="Designation Level"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:TextBox ID="txtDesignationLevel" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label22" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td class="auto-style14">:</td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>Officer</asp:ListItem>
                                <asp:ListItem>Staff</asp:ListItem>
                                <asp:ListItem>Worker</asp:ListItem>
                                <asp:ListItem>Internee</asp:ListItem>
                                <asp:ListItem>Manager</asp:ListItem>
                                <asp:ListItem>Executive</asp:ListItem>
                                <asp:ListItem>Assistant</asp:ListItem>
                                <asp:ListItem>BOD</asp:ListItem>
                                <asp:ListItem>Trainee</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label23" runat="server" Text="ID Card Number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtIdCardID" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label24" runat="server" Text="ID Card Expiry Date"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtIdcardExpireDate" runat="server" autocomplete="off" Width="350px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="idcardExpireDate_CalExtender" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtIdcardExpireDate">
                            </ajaxToolkit:CalendarExtender>






                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label25" runat="server" Text="Father Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtFatherName" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label26" runat="server" Text="Mother's Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtMotherName" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label29" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>WidowedDivorced</asp:ListItem>
                                <asp:ListItem>Married</asp:ListItem>
                                <asp:ListItem>Single</asp:ListItem>
                                <asp:ListItem>Separated</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label117" runat="server" Text="Spouse Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtSpouseName" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label27" runat="server" Text="Religion"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlReligion" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>Buddhism</asp:ListItem>
                                <asp:ListItem>Christianity</asp:ListItem>
                                <asp:ListItem>Hindu</asp:ListItem>
                                <asp:ListItem>Islam</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label28" runat="server" Text="Date Of Birth"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="popupDateOfBirth" runat="server" autocomplete="off" Width="350px" placeholder="Date Of Birth"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="popupDateOfBirth_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="popupDateOfBirth">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label30" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlGender" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label31" runat="server" Text="Blood Group"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlBloodGroup" runat="server" Width="355px">
                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                <asp:ListItem>O-</asp:ListItem>
                                <asp:ListItem>O+</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>B-</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>AB-</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>N/A</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label32" runat="server" Text="Company Mobile No"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtHomePhone" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label33" runat="server" Text="Personal Contact No 1"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtpersonalcontactno1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label34" runat="server" Text="Personal Contact No 2"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtpersonalcontactno2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label35" runat="server" Text="NID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNID" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label36" runat="server" Text="TIN "></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtTINNumber" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label129" runat="server" Text="Driving License No "></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDrivingLicense" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label37" runat="server" Text="Passport Number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPassportNumber" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label38" runat="server" Text="Em. Contact Person 1"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmergencyContactPerson1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label114" runat="server" Text="Em. Contact No Person 1 "></asp:Label>
                            &nbsp;</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmergencyContactpersonnumber1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label115" runat="server" Text="Em. Contact Person 2"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmergencyContactPerson2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label116" runat="server" Text="Em. Contact No Person 2 "></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmergencyContactpersonnumber2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label40" runat="server" Text="Company Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailCompany" runat="server" placeholder="Email Address" Width="350px"></asp:TextBox>


                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label113" runat="server" Text="Personal Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailPersonal" runat="server" placeholder="Email Address" Width="350px"></asp:TextBox>

                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label128" runat="server" Text="Shift"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlShift" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label41" runat="server" Text="Probation Period(month)"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtProbationPeriod" runat="server" onkeypress="return isNumberKey(event)" placeholder="month" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="lblStatusShow" runat="server" Font-Italic="True" Text="Status" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Font-Italic="True"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label1" runat="server" Text="Employee Grade" Visible="False"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeGrade" runat="server" Width="350px" Visible="False"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel6" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label7" Text="Bank Account Information" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label80" runat="server" Text="Bank Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlBankName" runat="server" Width="355px" AutoPostBack="True" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label81" runat="server" Text="Branch Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranchName" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label82" runat="server" Text="Bank Account No"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtBankAccountNo" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel7" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label8" Text="Leave Allocation" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label83" runat="server" Text="Leave Type"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAddLeaveIntoList" runat="server" Text="Add to List" OnClick="btnAddLeaveIntoList_Click" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:ListBox ID="lstForLeaveAllocation" runat="server" Height="100px" Width="350px" AutoPostBack="True"
                                OnSelectedIndexChanged="lstForLeaveAllocation_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel8" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label9" Text="Dependents Information" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnAddDependentInformation" runat="server" Text="Add New Record" OnClick="btnAddDependentInformation_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdDependentsInformation" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No record"
                                OnRowCommand="grdDependentsInformation_RowCommand" OnRowDeleting="grdDependentsInformation_RowDeleting"
                                OnRowDataBound="grdDependentsInformation_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="txtNameDependent" HeaderText="Name" />
                                    <asp:BoundField DataField="ddlGenderDependent" HeaderText="Gender" />
                                    <asp:BoundField DataField="popupDateOfBirthDependent" HeaderText="Date Of Birth" />
                                    <asp:BoundField DataField="txtRelationshipWithEmployee" HeaderText="Relationship With Employee" />
                                    <asp:BoundField DataField="ddlGenderCodeDependent" HeaderText="GenderCode" />
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderForDependentsInformation" runat="server" TargetControlID="btnShowPopupDependent" PopupControlID="PanelForDependentsInformation"
                                CancelControlID="btnCancelDependentPopup" BackgroundCssClass="ModalBackgroud">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PanelForDependentsInformation" runat="server" Height="225px" Width="700px" BackColor="White">
                                <table style="width: 100%;" class="popupform">
                                    <tr>
                                        <td class="tblbig" colspan="3" style="height: 25px; text-align: center; background-color: gray; font-size: 20px; color: #FFFFFF;">Dependents Information</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label84" runat="server" Text="Name"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtNameDependent" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label85" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlGenderDependent" runat="server" Width="250px">
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label86" runat="server" Text="Date Of Birth"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="popupDateOfBirthDependent" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="110px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label87" runat="server" Text="Relationship with Employee"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtRelationshipWithEmployee" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddDependentsInformatonSingle" runat="server" Text="Add" Width="125px" OnClick="btnAddDependentsInformatonSingle_Click" />
                                            <asp:Button ID="btnCancelDependentPopup" runat="server" Text="Cancel" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblForErrorMSGOfDependentsInformation" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel1" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label2" Text="Present Address" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label42" runat="server" Text="Present Address"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPresentAddress" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label43" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDivisionPre" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionPre_SelectedIndexChanged" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label44" runat="server" Text="District"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDistrictPre" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label45" runat="server" Text="Postal Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPostalCodePre" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label46" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCountryPre" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label47" runat="server" Text="Contact Person Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactPersonPresent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label48" runat="server" Text="Contact Number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactNumberPresent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label49" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailPresent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel3" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label4" Text="Permanent Address" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label59" runat="server" Text="Permanent Address"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPermanentAddress" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label60" runat="server" Text="Division"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDivisionPer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisionPer_SelectedIndexChanged" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label61" runat="server" Text="District"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlDistrictPer" runat="server" Width="355px">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label62" runat="server" Text="Postal Code"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPostalCodePer" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label63" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCountryPer" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label64" runat="server" Text="Contact Person Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactPersonPermanent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label65" runat="server" Text="Contact Number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactNumberPermanent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label66" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailPermanent" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel2" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label3" Text="Reference" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label50" runat="server" Text="If reference is within the group, then select company, department, employee ID."></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label51" runat="server" Text="Company Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlCompanyNameForRef" runat="server" Width="355px" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyNameForRef_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label53" runat="server" Text="Employee ID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmployeeIDForRef" runat="server" Width="350px" AutoPostBack="True" OnTextChanged="txtEmployeeIDForRef_TextChanged"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="txtEmployeeIDForRef_AutoCompleteExtender" runat="server" DelimiterCharacters="" Enabled="True"
                                CompletionListCssClass="autocomplete_completionListElement"
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                CompletionListItemCssClass="autocomplete_listItem2"
                                MinimumPrefixLength="1" ServiceMethod="GetEmpId" ServicePath="~/modules/Payroll/WebService.asmx" TargetControlID="txtEmployeeIDForRef">
                            </ajaxToolkit:AutoCompleteExtender>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="PanelForEmployeeDetails" runat="server" Height="100%" ScrollBars="None" Width="100%">

                                <asp:Panel ID="PanelForDetailsHeader" runat="server" CssClass="cpHeaderContent" Height="15px" ScrollBars="None" Width="100%">
                                    <asp:Label ID="lblIntoEmployeeDetailsHD" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PanelForEmployeeDetailsBody" runat="server" Height="85px" Width="100%" ScrollBars="None">
                                    <table style="width: 100%; text-align: left">
                                        <tr>
                                            <td style="width: 115px; text-align: left">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label39" runat="server" Text="Name&nbsp;"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label132" runat="server" Text="Designation"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label107" runat="server" Text="Department"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblEmployeeDepartment" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label54" runat="server" Text="If reference is outside the company, then filll up, reference name, contact number, email address and NID. "></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style11" colspan="3">
                            <asp:Label ID="Label120" runat="server" Text="Reference 1" CssClass="cpHeaderContent" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label55" runat="server" Text="Reference name"></asp:Label>
                            &nbsp;</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtReferenceName1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label118" runat="server" Text="Organization Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtOrganizationRf1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label119" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDesignationRef1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label56" runat="server" Text="Contact number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactNumberForRef1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;<asp:Label ID="Label57" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailForRef1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label58" runat="server" Text="NID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNIDForRef1" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label121" runat="server" CssClass="cpHeaderContent" Text="Reference 2" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label122" runat="server" Text="Reference name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtReferenceName2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label123" runat="server" Text="Organization Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtOrganizationRef2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label124" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtDesignationRef2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label125" runat="server" Text="Contact number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactNumberForRef2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label126" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmailForRef2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="Label127" runat="server" Text="NID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtNIDForRef2" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel4" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label5" Text="Academic Qualification" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnAddAcademicRecord" runat="server" OnClick="btnAddAcademicRecord_Click" Text="Add New Record" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdAcademicRecords" runat="server" AutoGenerateColumns="False" EmptyDataText="No record" Width="100%" OnRowCommand="grdAcademicRecords_RowCommand" OnRowDeleting="grdAcademicRecords_RowDeleting" OnRowDataBound="grdAcademicRecords_RowDataBound" OnSelectedIndexChanged="grdAcademicRecords_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="txtNameOfDegree" HeaderText="Name Of Degree" />
                                    <asp:BoundField DataField="txtInstitutionName" HeaderText="Institution Name" />
                                    <asp:BoundField DataField="txtBoardUniversity" HeaderText="Board/University" />
                                    <asp:BoundField DataField="txtMajor" HeaderText="Major in/Group" />
                                    <asp:BoundField DataField="txtResultsGradeDivision" HeaderText="Result Grade/Division" />
                                    <asp:BoundField DataField="txtPassingYear" HeaderText="Passing Year" />
                                    <asp:BoundField DataField="txtCourseDuration" HeaderText="Course Duration" />
                                    <asp:BoundField DataField="FileUploadAcademic" HeaderText="DocumentCode" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButtonDownloadAcademic"
                                                runat="server" Enabled="true" ImageUrl="~/Images/imageAttachment.jpg" 
                                                CommandName="DownloadFile" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderForAcademicRecord" runat="server" TargetControlID="btnShowPopupAcademic" PopupControlID="PanelForAcademicRecords"
                                CancelControlID="btnCancelAcademicPopup" BackgroundCssClass="ModalBackgroud">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PanelForAcademicRecords" runat="server" BackColor="White" Height="400px" Style="display: block" Width="785px">
                                <table style="width: 100%; text-align: left" class="popupform">
                                    <tr>
                                        <td colspan="3" style="height: 25px; text-align: center; background-color: gray; font-size: 20px; color: #FFFFFF;">Academic Qualification</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label67" runat="server" Text="Name Of Degree"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtNameOfDegree" runat="server" Width="250px" Columns="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label68" runat="server" Text="Institution Name"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtInstitutionName" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label69" runat="server" Text="Board/University"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtBoardUniversity" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label130" runat="server" Text="Major in/Group"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtMajor" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label70" runat="server" Text="Results Grade/Division"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtResultsGradeDivision" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label71" runat="server" Text="Passing Year"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtPassingYear" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label72" runat="server" Text="Course Duration (Year's)"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtCourseDuration" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label137" runat="server" Text="Choose File"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadAcademic" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddSingleAcademicRecord" runat="server" Text="Add" Width="125px" OnClick="btnAddSingleAcademicRecord_Click" />
                                            <asp:Button ID="btnCancelAcademicPopup" runat="server" Text="Cancel" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblForErrorMSGAccademic" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                            <asp:Button ID="btnShowPopupDependent" runat="server" Style="display: none" />
                            <asp:Button ID="btnShowPopupProfessional" runat="server" Style="display: none" />
                            <asp:Button ID="btnShowPopupAcademic" runat="server" Style="display: none" />
                            <asp:Button ID="btnShowPopupAsset" runat="server" Style="display: none" />
                            <asp:Button ID="btnShowPopupTrainig" runat="server" Style="display: none" />


                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel5" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label6" Text="Experiance History" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnAddProfessionalRecord" runat="server" Text="Add New Record" OnClick="btnAddProfessionalRecord_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdProfessionalQualification" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No record" OnRowCommand="grdProfessionalQualification_RowCommand" OnRowDeleting="grdProfessionalQualification_RowDeleting" OnRowDataBound="grdProfessionalQualification_RowDataBound" OnSelectedIndexChanged="grdProfessionalQualification_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="txtNameofInstitution" HeaderText="Name of Institution" />
                                    <asp:BoundField DataField="txtInstitutionAddress" HeaderText="Institution Address" />
                                    <asp:BoundField DataField="popupServiceStartDate" HeaderText="Service Start Date" />
                                    <asp:BoundField DataField="popupServiceEndDate" HeaderText="Service End Date" />
                                    <asp:BoundField DataField="txtDesignationPrevious" HeaderText="Designation" />
                                    <asp:BoundField DataField="txtServiceDescription" HeaderText="Service Description" />
                                    <asp:BoundField DataField="txtLastGrossSalary" HeaderText="Gross Salary" />
                                    <asp:BoundField DataField="FileUploadExperiance" HeaderText="DocumentCode" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButtonDownloadAcademic" runat="server" Enabled="true" ImageUrl="~/Images/imageAttachment.jpg" CommandName="Download" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True" >
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderForProfessionalRecord" runat="server" TargetControlID="btnShowPopupProfessional" PopupControlID="PanelForProfessionalRecord"
                                CancelControlID="btnCancleProfessionalRecordPnael" BackgroundCssClass="ModalBackgroud">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PanelForProfessionalRecord" runat="server" BackColor="White" Height="400px" Width="785px">
                                <table style="width: 100%;" class="popupform">

                                    <tr>
                                        <td class="tblbig" colspan="3" style="height: 25px; text-align: center; background-color: gray; font-size: 20px; color: #FFFFFF;">Experiance History</td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label73" runat="server" Text="Name of Organization"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtNameofInstitution" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label74" runat="server" Text="Organization Address"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtInstitutionAddress" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label75" runat="server" Text="Service Start Date"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <ew:CalendarPopup ID="popupServiceStartDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="110px">
                                                <MonthHeaderStyle BackColor="#2A2965" ForeColor="White" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label76" runat="server" Text="Service End Date"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <ew:CalendarPopup ID="popupServiceEndDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="110px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label77" runat="server" Text="Designation"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtDesignationPrevious" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label78" runat="server" Text="Service Description"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtServiceDescription" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label79" runat="server" Text="Last Gross Salary"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtLastGrossSalary" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label139" runat="server" Text="Choose File"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:FileUpload ID="FileUploadExperiance" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">&nbsp;</td>
                                        <td class="auto-style18">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddProfessionalRecordSingle" runat="server" Text="Add" Width="125px" OnClick="btnAddProfessionalRecordSingle_Click" />
                                            <asp:Button ID="btnCancleProfessionalRecordPnael" runat="server" Text="Cancel" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">&nbsp;</td>
                                        <td class="auto-style18">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblForErrorMsgProfessionalRecord" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel9" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label10" Text="Asset Allocation" runat="server" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnAddAssetRecord" runat="server" Text="Add New Record" OnClick="btnAddAssetRecord_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6"></td>
                        <td class="auto-style14"></td>
                        <td class="auto-style10"></td>
                        <td class="auto-style7">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdAssetAllocation" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No record"
                                OnRowCommand="grdAssetAllocation_RowCommand" OnRowDeleting="grdAssetAllocation_RowDeleting" OnRowDataBound="grdAssetAllocation_RowDataBound" OnSelectedIndexChanged="grdAssetAllocation_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="txtAssetName" HeaderText="Asset Name" />
                                    <asp:BoundField DataField="txtAssetIDNo" HeaderText="Asset ID No" />
                                    <asp:BoundField DataField="txtModelNo" HeaderText="Model No" />
                                    <asp:BoundField DataField="txtAssetDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="ddlAssetReportingPerson" HeaderText="Reporting Person Id" />
                                    <asp:BoundField DataField="popupActiveDate" HeaderText="Issue Date" />
                                    <asp:BoundField DataField="popupInactiveDate" HeaderText="Inactive Date" />
                                    <asp:BoundField DataField="ddlStatusOfEmpForAssetAllocation" HeaderText="Status" />
                                    <asp:BoundField DataField="ddlStatusCodeOfEmpForAssetAllocation" HeaderText="StatusCode" />
                                    <asp:BoundField DataField="FileUploadAsset" HeaderText="DocumentCode" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButtonDownloadAcademic" runat="server" Enabled="true" ImageUrl="~/Images/imageAttachment.jpg" CommandName="Download" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderAssetAllocation" runat="server" TargetControlID="btnShowPopupAsset" PopupControlID="PanelForAssetAllocation"
                                CancelControlID="btnCancleAssetAllocationPopup" BackgroundCssClass="ModalBackgroud">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PanelForAssetAllocation" runat="server" BackColor="White" Height="565px" Width="785px">
                                <table style="width: 100%;" class="popupform">
                                    <tr>
                                        <td class="tblbig" colspan="3" style="height: 25px; text-align: center; background-color: gray; font-size: 20px; color: #FFFFFF;">Asset Allocation</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label88" runat="server" Text="Asset Name"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAssetName" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label89" runat="server" Text="Asset ID No"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAssetIDNo" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label90" runat="server" Text="Model No"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtModelNo" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label91" runat="server" Text="Description"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAssetDescription" runat="server" Height="50px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label92" runat="server" Text="Reporting Person"></asp:Label>
                                            &nbsp;ID</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="txtAssetReportingPerson" runat="server" AutoPostBack="True" OnTextChanged="txtAssetReportingPerson_TextChanged" Width="250px" AutoCompleteType="None"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="txtAssetReportingPerson_AutoCompleteExtender" runat="server"
                                                DelimiterCharacters=""
                                                Enabled="True"
                                                CompletionListCssClass="autocomplete_completionListElement"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                CompletionListItemCssClass="autocomplete_listItem"
                                                MinimumPrefixLength="1"
                                                ServiceMethod="GetEmpId"
                                                ServicePath="~/modules/Payroll/WebService.asmx"
                                                TargetControlID="txtAssetReportingPerson">
                                            </ajaxToolkit:AutoCompleteExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="PanelReportingPersonDetails" runat="server" Height="100%" ScrollBars="None" Width="100%">

                                                <asp:Panel ID="Panel13" runat="server" CssClass="cpHeaderContent" Height="15px" ScrollBars="None" Width="100%">
                                                    <asp:Label ID="Label52" runat="server" Text="EMPLOYEE DETAILS"></asp:Label>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel14" runat="server" Height="85px" Width="100%" ScrollBars="None">
                                                    <table style="width: 100%; text-align: left">
                                                        <tr>
                                                            <td style="width: 115px; text-align: left">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label105" runat="server" Text="Name&nbsp;"></asp:Label>
                                                            </td>
                                                            <td>:</td>
                                                            <td>
                                                                <asp:Label ID="lblReportingPersonName" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label133" runat="server" Text="Designation"></asp:Label>
                                                            </td>
                                                            <td>:</td>
                                                            <td>
                                                                <asp:Label ID="lblReportingPersonDesignation" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label135" runat="server" Text="Department"></asp:Label>
                                                            </td>
                                                            <td>:</td>
                                                            <td>
                                                                <asp:Label ID="lblReportingPersonDepartment" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label93" runat="server" Text="Issue Date"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="popupActiveDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="110px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label94" runat="server" Text="Inactive Date"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <ew:CalendarPopup ID="popupInactiveDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="110px" Nullable="True" SelectedDate="">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label95" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatusOfEmpForAssetAllocation" runat="server" Width="250px">
                                                <asp:ListItem Value="Y">Active</asp:ListItem>
                                                <asp:ListItem Value="N">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label140" runat="server" Text="Choose File"></asp:Label>
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadAsset" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddSingleAsstInformation" runat="server" Text="Add" Width="125px" OnClick="btnAddSingleAsstInformation_Click" />
                                            <asp:Button ID="btnCancleAssetAllocationPopup" runat="server" Text="Cancel" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblForErrorMSGOfAssetAllocation" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">&nbsp;</td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel12" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label96" runat="server" Text="Training Record" />
                                </asp:Panel>
                            </div>
                        </td>
                        <td style="text-align: center">&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnTraining" runat="server" Text="Add New Record" OnClick="btnTraining_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdTrainingRecord" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No record" OnRowCommand="grdTrainingRecord_RowCommand" OnRowDataBound="grdTrainingRecord_RowDataBound" OnRowDeleting="grdTrainingRecord_RowDeleting" OnSelectedIndexChanged="grdTrainingRecord_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ddlTrainingTitle" HeaderText="Training Topic Type" />
                                    <asp:BoundField DataField="txtTrainingTitleSpecification" HeaderText="Training Title" />
                                    <asp:BoundField DataField="txtTrainingBenefits" HeaderText="Training Content" />
                                    <asp:BoundField DataField="txtNameofInstitution" HeaderText="Name of Institution" />
                                    <asp:BoundField DataField="txtInstitutionAddress" HeaderText="Institution Address" />
                                    <asp:BoundField DataField="calendarTrainingStartDate" HeaderText="Start Date" />
                                    <asp:BoundField DataField="calendarTrainingEndDate" HeaderText="End Date" />
                                    <asp:BoundField DataField="txtDuration" HeaderText="Duration (Hours)" />
                                    <asp:BoundField DataField="txtTrainingFee" HeaderText="Training Fee" />
                                    <asp:BoundField DataField="ddlcertificateTitle" HeaderText="Certificate Achieved" />
                                    <asp:BoundField DataField="ddlfundTitle" HeaderText="Fund By" />
                                    <asp:BoundField DataField="ddlTrainingCode" HeaderText="Training Code" />
                                    <asp:BoundField DataField="ddlcertificateCode" HeaderText="Certificate Code" />
                                    <asp:BoundField DataField="ddlfundCode" HeaderText="Fund Code" />
                                    <asp:BoundField DataField="FileUploadTraining" HeaderText="DocumentCode" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButtonDownloadAcademic" runat="server" Enabled="true" ImageUrl="~/Images/imageAttachment.jpg" CommandName="Download" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" >

                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:CommandField>

                                </Columns>
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderTrainingRecord" runat="server" TargetControlID="btnShowPopupTrainig" PopupControlID="PanelTrainingRecord"
                                CancelControlID="Button3" BackgroundCssClass="ModalBackgroud">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="PanelTrainingRecord" runat="server" BackColor="White" Height="500px" Width="785px">
                                <table style="width: 100%;" class="popupform">

                                    <tr>
                                        <td class="auto-style4" colspan="3" style="text-align: center; background-color: gray; font-size: 20px; color: #FFFFFF;">Training Record</td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label108" runat="server" Text="Training Topic Type" Visible="False"></asp:Label>
                                        </td>
                                        <td class="auto-style18">&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTraining" runat="server" Width="255px" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label110" runat="server" Text="Training Title"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingTitle" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label111" runat="server" Text="Training Content"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingBenefits" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label100" runat="server" Text="Name of Institution"></asp:Label>
                                            /Conduct&nbsp; Person</td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingInstituteName" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label101" runat="server" Text="Institution Address"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTrainingInstituteAddress" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label102" runat="server" Text="Start Date"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <ew:CalendarPopup ID="calendarTrainingStartDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="220px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label103" runat="server" Text="End Date"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <ew:CalendarPopup ID="calendarTrainingEndDate" runat="server" CssClass="txtbox" Culture="English (United Kingdom)" Enabled="true" Width="220px">
                                                <MonthHeaderStyle BackColor="#2A2965" />
                                                <ButtonStyle CssClass="btn2" />
                                            </ew:CalendarPopup>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">
                                            <asp:Label ID="Label104" runat="server" Text="Duration (Hours)"></asp:Label>
                                        </td>
                                        <td class="auto-style18">:</td>
                                        <td>
                                            <asp:TextBox ID="txtDuration" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label106" runat="server" Text="Training Fee"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtTrainingFee" runat="server" Width="250px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label109" runat="server" Text="Certificate Achieved"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtCertificateAchieved" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label112" runat="server" Text="Fund By"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="txtFundBy" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label138" runat="server" Text="Choose File"></asp:Label>
                                        </td>
                                        <td class="auto-style4">:</td>
                                        <td class="auto-style4">
                                            <asp:FileUpload ID="FileUploadTraining" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">&nbsp;</td>
                                        <td class="auto-style18">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnAddTrainingRecord" runat="server" Text="Add" Width="125px" OnClick="btnAddTrainingRecord_Click" />
                                            <asp:Button ID="Button3" runat="server" Text="Cancel" Width="125px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style19">&nbsp;</td>
                                        <td class="auto-style18">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblMessageTrainingRecord" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <div style="margin-left: -7px; margin-right: -7px">
                                <asp:Panel ID="Panel10" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label131" runat="server" Text="Others Document" />
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style6">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <table style="width: 99%; text-align: left; height: 261px;">
                                <tr>
                                    <td class="auto-style6">
                                        <asp:Label ID="Label136" runat="server" Text="Choose File"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:FileUpload ID="file_upload" runat="server" class="multi" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnUploadDoc" runat="server" OnClick="btnUploadDoc_Click" Text="Upload" Width="125px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <uc1:ucOthersDocument ID="ucOthersDocument1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Button ID="btnSaveAllInformation" runat="server" CssClass="btn2" OnClick="btnSaveAllInformation_Click" OnClientClick="ShowConfirmBox(this,'Are you Sure Save Employee Information ?'); return false;" Text="Save Information" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtForUpdateTrans_Det" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                </table>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveAllInformation" />
            <asp:PostBackTrigger ControlID="btnNewEmployee" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="imgBtnProfileUpload" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="ucOthersDocument1" />
            <asp:PostBackTrigger ControlID="btnAddSingleAcademicRecord" />
            <asp:PostBackTrigger ControlID="grdAcademicRecords" />
            <asp:PostBackTrigger ControlID="btnAddTrainingRecord" />
            <asp:PostBackTrigger ControlID="grdTrainingRecord" />
            <asp:PostBackTrigger ControlID="btnAddProfessionalRecordSingle" />
            <asp:PostBackTrigger ControlID="grdProfessionalQualification" />
            <asp:PostBackTrigger ControlID="btnAddSingleAsstInformation" />
            <asp:PostBackTrigger ControlID="grdAssetAllocation" />
            <asp:PostBackTrigger ControlID="btnUploadDoc" />

        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Please Enter Only Numeric Value:");
                return false;
            }
            return true;
        }


        function setfocus(utxtbox) {

            utxtbox.value = utxtbox.value.toUpperCase();
        }

    </script>

</asp:Content>

