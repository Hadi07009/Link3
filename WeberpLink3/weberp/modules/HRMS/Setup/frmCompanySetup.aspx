<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmCompanySetup.aspx.cs" Inherits="modules_HRMS_Setup_frmCompanySetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <link href="../CSS/grayStyleBasic.css" rel="stylesheet" />


            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                COMPANY SETUP
            </asp:Panel>
            <asp:Panel ID="PanelCompanySet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Company ID"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="250px" onkeypress="return IsMaxLength(this, 3);"></asp:TextBox>
                            <ajaxToolkit:TextBoxWatermarkExtender ID="txtCompanyID_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtCompanyID" WatermarkCssClass="Watermark" WatermarkText=" &lt;-- 3 Characters Only --&gt; ">
                            </ajaxToolkit:TextBoxWatermarkExtender>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Company Name"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="500px"></asp:TextBox>
                        </td>
                        <td rowspan="10" align="left" style="text-align: left; vertical-align: top; padding: 0">
                            <table style="width: 100%; height: 184px;">
                                <tr>
                                    <td align="center" style="text-align: left; vertical-align: top; padding: 0" colspan="4">
                                        <asp:Label ID="lblImage" runat="server" BackColor="Gray" BorderColor="Black" ForeColor="Red" Height="150px" Style="text-align: center; vertical-align: middle;" Width="150px"
                                            Font-Bold="True"
                                            Font-Italic="True" Font-Size="X-Large">                                              
                                                <br /> Logo
                                            <br />  Not <br />  Available 
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Logo"></asp:Label>
                                    </td>
                                    <td>:</td>
                                    <td>
                                        <asp:FileUpload ID="LogoUpload" runat="server" Width="185px" onchange="validateFileSize();" />
                                        <asp:Button ID="btnImageUpload" runat="server" OnClick="btnImageUpload_Click" Text="Upload" />
                                        <br />
                                        <div id="dvMsg" style="background-color: Red; color: White; width: 190px; padding: 3px; display: none;">
                                            Maximum size allowed is 500 kb
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:RegularExpressionValidator ID="RevImg" runat="server" ControlToValidate="LogoUpload" ErrorMessage="Invalid File!(only  .gif, .jpg, .jpeg, .bmp, .png  Files are supported)" ForeColor="Red" ValidationExpression="^.+(.jpg|.JPG|.gif|.GIF|.jpeg|JPEG| .bmp|BMP| .png|PNG)$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Address 1"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" Width="495px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Address 2"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" Width="495px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Address 3"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtAddress3" runat="server" TextMode="MultiLine" Width="495px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 38px">
                            <asp:Label ID="Label7" runat="server" Text="Contact Person Address"></asp:Label>
                        </td>
                        <td style="height: 38px">:</td>
                        <td style="height: 38px">
                            <asp:TextBox ID="txtContactPersonAddress" runat="server" TextMode="MultiLine" Width="495px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Contact Person Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtContactPersonEmail" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Phone Number"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Fax"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtFax" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="URL"></asp:Label>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="txtURL" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="TIN"></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTIN" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Reg. No"></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtRegNo" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="VAT No"></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtVATNo" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Insurance "></asp:Label>
                        </td>
                        <td>:</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtInsurance1" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <asp:Button ID="btnCompanySetup" runat="server" OnClick="btnCompanySetup_Click" Text="Save" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grdShowAllCompany" runat="server" AutoGenerateColumns="False" Width="100%"
                                OnRowCommand="grdShowAllCompany_RowCommand" OnRowDeleting="grdShowAllCompany_RowDeleting"
                                OnRowDataBound="grdShowAllCompany_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <%# Container.DisplayIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyID" runat="server" Text='<%# Bind("CompanyId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress1" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 2">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress2" runat="server" Text='<%# Bind("Address2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address 3">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAddress3" runat="server" Text='<%# Bind("Address3") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person Address">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPersonAddress" runat="server" Text='<%# Bind("ContPer1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact Person Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactPersonEmail" runat="server" Text='<%# Bind("ContPer2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhoneNumber" runat="server" Text='<%# Bind("Phone1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFax" runat="server" Text='<%# Bind("Fax1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="URL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblURL" runat="server" Text='<%# Bind("Url1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TIN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTIN" runat="server" Text='<%# Bind("TIN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reg. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("RegNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VAT No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVATNo" runat="server" Text='<%# Bind("VATNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Insurance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInsurance" runat="server" Text='<%# Bind("Insurance1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Logo">
                                        <ItemTemplate>
                                            <img src='data:image/jpg;base64,<%# Eval("CompanyLogo") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("CompanyLogo")) : string.Empty %>' alt="No image" height="30" width="60" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LogoValue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLogo" runat="server" Text='<%# Eval("CompanyLogo") != System.DBNull.Value ? Convert.ToBase64String((byte[])Eval("CompanyLogo")) : string.Empty %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImageUpload" />
            <asp:PostBackTrigger ControlID="btnCompanySetup" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function validateFileSize() {
            var uploadControl = document.getElementById("<%= LogoUpload.ClientID %>")
            if (uploadControl.files[0].size > 524288) {
                document.getElementById('dvMsg').style.display = "block";

                return false;
            }
            else {
                document.getElementById('dvMsg').style.display = "none";
                return true;
            }
        }
        function IsMaxLength(obj, MaxLen) {
            return (obj.value.length < MaxLen);
        }
    </script>
</asp:Content>

