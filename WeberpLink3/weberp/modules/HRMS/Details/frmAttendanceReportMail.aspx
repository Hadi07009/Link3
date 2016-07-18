<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmAttendanceReportMail.aspx.cs" Inherits="modules_HRMS_Details_frmAttendanceReportMail" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:UpdatePanel ID="updpnl2" runat="server">
                    <ContentTemplate>--%>
                        <style type="text/css">
                .cpHeader {
                    color: white;
                    background-color: #719DDB;
                    font: bold 11px auto "Trebuchet MS", Verdana;
                    font-size: 12px;
                    cursor: pointer;
                    height: 18px;
                    padding: 4px;
                }

                .cpBody {
                    background-color: #DCE4F9;
                    font: normal 12px auto "Trebuchet MS";
                    border: 1px gray;
                    padding: 4px;
                    padding-top: 2px;
                    height: 0px;
                    overflow: hidden;
                }
                            .auto-style4 {
                                width: 53px;
                            }
            </style>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeader" Width="100%">
                <asp:Label ID="lblleave" Text="Attendance Mail" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBody" Width="100%" Height="100%">
                </asp:Panel>
                        <table style="width: 99%; text-align: left">
            <tr>
                <td class="auto-style4">
                                        <asp:Label ID="Label11" runat="server" Text="From Date"></asp:Label>
                                    </td>
                <td >
                    <asp:TextBox ID="txtdaterange1" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdaterange1_CalendarExtender3" runat="server" 
                                            Enabled="True" PopupButtonID ="ImageButton3" 
                                            TargetControlID="txtdaterange1" Format ="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton3" runat="server" 
                                            ImageUrl="~/Images/Calendar.ico" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label12" runat="server" Text="To Date"></asp:Label>
                                    </td>
                <td >
                                        <asp:TextBox ID="txtdaterange2" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdaterange2_CalendarExtender3" runat="server" 
                                            Enabled="True" PopupButtonID ="ImageButton4" 
                                            TargetControlID="txtdaterange2" Format ="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                        <asp:ImageButton ID="ImageButton4" runat="server" 
                                            ImageUrl="~/Images/Calendar.ico" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                                        <asp:Label ID="Label22" runat="server" Text="Select Department To Send Mail" style="font-weight: 700"></asp:Label>
                                    </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    <div style="overflow: scroll; height: auto; width: 400px;border-width: 1px;
                                                border-style: solid;
                                                border-color: black; ">
                    <asp:CheckBoxList runat="server" ID="ChkLisBoxDepartment"></asp:CheckBoxList>
                    </div>
                 </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                <asp:Button ID="btnAttendanceMail" runat="server" Text="Send Mail" Width="111px" 
                      onclick="btnAttendanceMail_Click" CssClass="btn2" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                <asp:Button ID="Button1" runat="server" Text="Report" Width="111px" 
                      onclick="Button1_Click" CssClass="btn2" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                                        <asp:Label ID="Label23" runat="server" Text="Email Individual Employee" style="font-weight: 700"></asp:Label>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    <asp:TextBox ID="txtempid" runat="server" Width="200px"></asp:TextBox>
                     <cc1:TextBoxWatermarkExtender ID="txtempid_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtempid" WatermarkText=" <-- Employee ID --> " >
                     </cc1:TextBoxWatermarkExtender>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    <asp:TextBox ID="txtemail" runat="server" Width="200px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtemail_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtemail" WatermarkText=" <-- Email Address --> " >
                     </cc1:TextBoxWatermarkExtender>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    <asp:TextBox ID="txtCc" runat="server" Width="200px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtCc_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtCc" WatermarkText=" <-- Email CC Address --> " >
                     </cc1:TextBoxWatermarkExtender>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                <asp:Button ID="btnAttendanceMail0" runat="server" Text="Send Mail" Width="111px" 
                      onclick="btnAttendanceMail0_Click" CssClass="btn2" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                                        <asp:Label ID="Label13" runat="server" Text="Email Configuration" style="font-weight: 700"></asp:Label>
                                    </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    <asp:TextBox ID="txtRefId" runat="server" Width="400px" Enabled="False" Visible="False"></asp:TextBox>
                                    </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label14" runat="server" Text="Department"></asp:Label>
                                    </td>
                <td >
                    <asp:DropDownList ID="ddlDept" runat="server" Width="400px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label15" runat="server" Text="Section"></asp:Label>
                                    </td>
                <td >
                    <asp:DropDownList ID="ddlSection" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label17" runat="server" Text="Location"></asp:Label>
                                    </td>
                <td >
                    <asp:DropDownList ID="ddlLocation" runat="server" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label16" runat="server" Text="Emp Type"></asp:Label>
                                    </td>
                <td >
                    <asp:DropDownList ID="ddlEmpType" runat="server" Width="400px">
                    </asp:DropDownList>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label24" runat="server" Text="Email From"></asp:Label>
                                    </td>
                <td >
                    <asp:TextBox ID="txtEmailFrom" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label18" runat="server" Text="Email To"></asp:Label>
                                    </td>
                <td >
                    <asp:TextBox ID="txtEmailTo" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label19" runat="server" Text="email Cc"></asp:Label>
                                    </td>
                <td >
                    <asp:TextBox ID="txtEmailCc" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label20" runat="server" Text="Subject"></asp:Label>
                                    </td>
                <td >
                    <asp:TextBox ID="txtSubject" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td  valign="top" class="auto-style4">
                                        <asp:Label ID="Label21" runat="server" Text="Email Body"></asp:Label>
                                    </td>
                <td  valign="top">
                    <asp:TextBox ID="txtBody" runat="server" Width="400px" Height="150px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                                        <asp:Label ID="Label10" runat="server" Text="Status"></asp:Label>
                                    </td>
                <td >
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="400px">
                    </asp:DropDownList>
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                <asp:Button ID="btnSave" runat="server" Text="Save Configuration" Width="130px" 
                      onclick="btnSave_Click" CssClass="btn2" />
                </td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td colspan="5">
                                        <asp:GridView ID="gdvConfig" runat="server" AutoGenerateColumns="False" 
                                            Width="100%" ShowFooter="True" OnRowDataBound="gdvConfig_RowDataBound" OnSelectedIndexChanged="gdvConfig_SelectedIndexChanged" 
                                            OnRowCommand="gdvConfig_RowCommand">
                                            
                                            <AlternatingRowStyle BackColor="#BFE4FF" />

                                            <Columns>
                                                
                                                <asp:BoundField DataField="Sl" HeaderText="SL #" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"  Width="50px" />
                                                </asp:BoundField>                                                                                           
                                                <asp:BoundField DataField="Dept" HeaderText="Department" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                                
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="Sect" HeaderText="Section" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                              
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                                </asp:BoundField>                                                  
                                                <asp:BoundField DataField="Office" HeaderText="Location" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="150px" />
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="EmpType" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="70px"/>
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="EmailFrom" HeaderText="Email From" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                                
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="110px"/>
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="EmailTo" HeaderText="Email To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                               
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="110px"/>
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="EmailCc" HeaderText="Email Cc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                              
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="110px"/>
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                              
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="150px"/>
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="EmailBody" HeaderText="Body" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                             
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="150px"/>                                                
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="Refid" HeaderText="Refid" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">                                             
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="50"/>                                                
                                                </asp:BoundField> 
                                                 <asp:BoundField DataField="TxtStatus" HeaderText="Status" />
                                                <asp:BoundField DataField="valueStatus" HeaderText="statusValue" />
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                        CommandName="EditLine" ID="btnEdit" runat="server" 
                                                            Text="Edit" Width="100px" CssClass="btn2"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                        CommandName="DeleteLine" ID="btnDelete" runat="server" 
                                                            Text="Delete" Width="100px" CssClass="btn2"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                               
                        </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4" >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
        </table>

                   <%-- </ContentTemplate>
                        <Triggers>--%>                             <%--<asp:PostBackTrigger ControlID="txtEmpId"/> --%>                        <%--</Triggers>
         </asp:UpdatePanel>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="false"
                GroupTreeStyle-BackColor="#6699FF" HasCrystalLogo="False" 
                HasDrillUpButton="False" HasGotoPageButton="False" HasSearchButton="False" 
                HasToggleGroupTreeButton="False"  Height="50px" 
                Width="350px" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:crystalreportsource>
</asp:Content>
