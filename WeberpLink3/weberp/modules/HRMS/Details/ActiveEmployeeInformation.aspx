<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masMain.master" CodeFile="ActiveEmployeeInformation.aspx.cs" Inherits="modules_HRMS_Details_ActiveEmployeeInformation" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="eWorld.UI.Compatibility" Namespace="eWorld.UI.Compatibility" TagPrefix="ew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc2:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updtPnl" runat="server">
        <ContentTemplate>
            <div style="overflow: hidden; overflow-x: hidden; overflow-y: hidden">

                <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                    <table style="width: 99%;">
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelEmployeeInformationHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <div>
                                        <asp:Label ID="lblEmployeeInformation" Text="Employee information" runat="server" onclick="closePanel('colps1','colps2','colps4','colps5');" />
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelForExpEmployeeInfo" runat="server">
                                    <table style="width: 100%;">
                                        <tr valign="top">
                                            <td style="width: 354px">
                                                <asp:GridView ID="grdTotalActiveEmployee" runat="server" AutoGenerateColumns="False" Width="350px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Active Employee">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalActiveEmployee" runat="server" Text='<%# Bind("totalActive") %>' Visible="False"></asp:Label>
                                                                <asp:LinkButton ID="LinkButtonTotalEmp" runat="server" Text='<%# Bind("totalActive") %>' OnClick="LinkButtonTotalEmp_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Male">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButtonForMale" runat="server" Text='<%# Bind("totalMale") %>' OnClick="LinkButtonForMale_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Female">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButtonForFemale" runat="server" Text='<%# Bind("totalFeMale") %>' OnClick="LinkButtonForFemale_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td>
                                                <asp:Chart ID="ChartTotalEmployee" runat="server" Height="300px" ImageType="Jpeg" Palette="Pastel" Style="margin-left: 50px; margin-right: 50px" Width="400px">
                                                    <Series>
                                                        <asp:Series Name="Team">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea IsSameFontSizeForAllAxes="True" Name="ChartArea1" ShadowColor="224, 224, 224">
                                                            <AxisX ArrowStyle="Lines" IsLabelAutoFit="False" LabelAutoFitStyle="StaggeredLabels">
                                                                <LabelStyle Angle="-50" />
                                                                <ScaleBreakStyle StartFromZero="Yes" />
                                                            </AxisX>
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" WallWidth="5" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="PanelForExpEmployeeInfo_CollapsiblePanelExtender"
                                    runat="server" Enabled="true"
                                    CollapseControlID="PanelEmployeeInformationHdr"
                                    ExpandControlID="PanelEmployeeInformationHdr"
                                    TargetControlID="PanelForExpEmployeeInfo"
                                    Collapsed="true"
                                    TextLabelID="lblEmployeeInformation"
                                    CollapsedText="Employee information"
                                    ExpandedText="Employee information"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ExpandedImage="~/Images/expand.jpg"
                                    CollapsedImage="~/Images/collapse.jpg"
                                    ExpandDirection="Vertical"
                                    BehaviorID="colps3">
                                </asp:CollapsiblePanelExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelDepartmentwiseHRD" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="lblDepartmentwiseEmployee" Text="Department wise employee" runat="server" onclick="closePanel('colps3','colps2','colps4','colps5');" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Panel ID="PanelDepartmentwiseBodey" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Panel ID="PanelDepartment" runat="server" Height="100%" Width="360px">
                                                    <table style="width: 99%; text-align: left">

                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="grdDepartment" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdDepartment_RowDataBound" ShowFooter="True" OnRowCommand="grdDepartment_RowCommand" Width="350px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SL">
                                                                            <ItemTemplate>
                                                                                <%# Container.DisplayIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Department Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDepartmentCode" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Department">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No of Employee">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNoofEmployee" runat="server" Text='<%# Bind("noOfEmployee") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                            <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPercentDepartment" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnExportDepartment" runat="server" OnClick="btnExportDepartment_Click" Text="Export to Excel" Width="125px" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:Chart ID="ChartDepartmentWise" runat="server" ImageType="Jpeg" Palette="Pastel" Style="margin-left: 50px; margin-right: 50px" Width="610px" Height="450px">
                                                    <Series>
                                                        <asp:Series Name="Team">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea IsSameFontSizeForAllAxes="True" Name="ChartArea1" ShadowColor="224, 224, 224">
                                                            <AxisX ArrowStyle="Lines" LabelAutoFitStyle="StaggeredLabels" IsLabelAutoFit="False">
                                                                <LabelStyle Angle="-50" />
                                                                <ScaleBreakStyle StartFromZero="Yes" />
                                                            </AxisX>
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" WallWidth="5" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="PanelDepartmentwiseBodey_CollapsiblePanelExtender"
                                    runat="server" Enabled="True"
                                    CollapseControlID="PanelDepartmentwiseHRD"
                                    ExpandControlID="PanelDepartmentwiseHRD"
                                    TargetControlID="PanelDepartmentwiseBodey"
                                    Collapsed="true"
                                    TextLabelID="lblDepartmentwiseEmployee"
                                    CollapsedText="Department wise employee"
                                    ExpandedText="Department wise employee"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ExpandedImage="~/Images/expand.jpg"
                                    CollapsedImage="~/Images/collapse.jpg"
                                    ExpandDirection="Vertical"
                                    BehaviorID="colps1">
                                </asp:CollapsiblePanelExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelOfficelocationwiseHRD" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="lblOfficelocationwise" Text="Office location wise employee" runat="server" onclick="closePanel('colps1','colps3','colps4','colps5');" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Panel ID="PanelOfficelocationwiseBody" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Panel ID="PanelOfficeLocatoin" runat="server" Height="100%" Width="360px">
                                                    <table style="width: 99%;">

                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="grdOfficelocation" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdOfficelocation_RowDataBound" ShowFooter="True" OnRowCommand="grdOfficelocation_RowCommand" Width="350px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SL">
                                                                            <ItemTemplate>
                                                                                <%# Container.DisplayIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Office Location Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOfficeLocationCode" runat="server" Text='<%# Bind("OfficeID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Office Location">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOfficeLocation" runat="server" Text='<%# Bind("Office") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No of Employee">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNoofEmployee" runat="server" Text='<%# Bind("noOfEmployee") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPercentDepartment" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnExportOfficeLocation" runat="server" OnClick="btnExportOfficeLocation_Click" Text="Export to Excel" Width="125px" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:Chart ID="ChartOfficeLocation" runat="server" ImageType="Jpeg" Palette="Pastel" Style="margin-left: 50px; margin-right: 50px" Width="610px" Height="450px">
                                                    <Series>
                                                        <asp:Series Name="Team">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea IsSameFontSizeForAllAxes="True" Name="ChartArea1" ShadowColor="224, 224, 224">
                                                            <AxisX ArrowStyle="Lines" LabelAutoFitStyle="StaggeredLabels" IsLabelAutoFit="False">
                                                                <LabelStyle Angle="-50" />
                                                                <ScaleBreakStyle StartFromZero="Yes" />
                                                            </AxisX>
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" WallWidth="5" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="PanelOfficelocationwiseBody_CollapsiblePanelExtender"
                                    runat="server" Enabled="True"
                                    CollapseControlID="PanelOfficelocationwiseHRD"
                                    ExpandControlID="PanelOfficelocationwiseHRD"
                                    TargetControlID="PanelOfficelocationwiseBody"
                                    Collapsed="true"
                                    TextLabelID="lblOfficelocationwise"
                                    CollapsedText="Office location wise employee"
                                    ExpandedText="Office location wise employee"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ExpandedImage="~/Images/expand.jpg"
                                    CollapsedImage="~/Images/collapse.jpg"
                                    ExpandDirection="Vertical"
                                    BehaviorID="colps2">
                                </asp:CollapsiblePanelExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelDesignationwiseEmployeeHRD" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="lblDesignationwiseEmployee" Text="Designation wise employee" runat="server" onclick="closePanel('colps1','colps2','colps3','colps5');" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Panel ID="PanelDesignationwiseEmployeeBody" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" valign="top" colspan="2">
                                                <asp:Panel ID="PanelDesignation" runat="server" Height="100%" Width="360px">
                                                    <table style="width: 99%;">

                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:GridView ID="grdDesignation" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdDesignation_RowDataBound" ShowFooter="True" OnRowCommand="grdDesignation_RowCommand" Width="350px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SL">
                                                                            <ItemTemplate>
                                                                                <%# Container.DisplayIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DesignationCode">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDesignationCode" runat="server" Text='<%# Bind("DesgID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Designation">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No of Employee">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNoofEmployee" runat="server" Text='<%# Bind("noOfEmployee") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                            <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPercentDepartment" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                                        </asp:TemplateField>
                                                                        <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnExportDesignation" runat="server" OnClick="btnExportDesignation_Click" Text="Export to Excel" Width="125px" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Chart ID="ChartDesignation" runat="server" ImageType="Jpeg" Palette="Pastel" Style="margin-left: 50px; margin-right: 50px" Width="610px" Height="450px">
                                                    <Series>
                                                        <asp:Series Name="Team" YValuesPerPoint="6">
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea IsSameFontSizeForAllAxes="True" Name="ChartArea1" ShadowColor="224, 224, 224">
                                                            <AxisX ArrowStyle="Lines" LabelAutoFitStyle="StaggeredLabels" IsLabelAutoFit="False">
                                                                <LabelStyle Angle="-50" />
                                                                <ScaleBreakStyle StartFromZero="Yes" />
                                                            </AxisX>
                                                            <Area3DStyle Enable3D="True" LightStyle="Realistic" WallWidth="5" />
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:CollapsiblePanelExtender ID="PanelDesignationwiseEmployeeBody_CollapsiblePanelExtender"
                                    runat="server" Enabled="True"
                                    CollapseControlID="PanelDesignationwiseEmployeeHRD"
                                    ExpandControlID="PanelDesignationwiseEmployeeHRD"
                                    TargetControlID="PanelDesignationwiseEmployeeBody"
                                    Collapsed="true"
                                    TextLabelID="lblDesignationwiseEmployee"
                                    CollapsedText="Designation wise employee"
                                    ExpandedText="Designation wise employee"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ExpandedImage="~/Images/expand.jpg"
                                    CollapsedImage="~/Images/collapse.jpg"
                                    ExpandDirection="Vertical"
                                    BehaviorID="colps4">
                                </asp:CollapsiblePanelExtender>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelSearchHRD" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="lblSearchHRD" Text="Search area" runat="server" onclick="closePanel('colps1','colps2','colps4','colps3');" />
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Panel ID="PanelSearchBody" runat="server">

                                    <table style="width: 99%; text-align: left">
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 96px">
                                                            <asp:Label ID="Label5" runat="server" Text="According To"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSearchArea" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchArea_SelectedIndexChanged" Width="400px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Operator"></asp:Label>
                                                        </td>
                                                        <td>:</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlConditionOperator" runat="server" Width="400px">
                                                                <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                                                <asp:ListItem>==</asp:ListItem>
                                                                <asp:ListItem>!=</asp:ListItem>
                                                                <asp:ListItem>&gt; </asp:ListItem>
                                                                <asp:ListItem>&lt; </asp:ListItem>
                                                                <asp:ListItem>&gt;=</asp:ListItem>
                                                                <asp:ListItem>&lt;=</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="PanelForAgeSearch" runat="server">
                                                    <table style="width: 100%; text-align: left">
                                                        <tr>
                                                            <td style="width: 96px">
                                                                <asp:Label ID="Label7" runat="server" Text="Age Value"></asp:Label>
                                                            </td>
                                                            <td>:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtForSearchAge" runat="server" Width="400px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Panel ID="PanelForSearchDate" runat="server">
                                                    <table style="width: 100%; text-align: left">
                                                        <tr>
                                                            <td style="width: 96px">
                                                                <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                                                            </td>
                                                            <td style="width: 1px">:</td>
                                                            <td>&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtFromDate" runat="server" autocomplete="off" Width="178px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: left">
                                                <asp:GridView ID="grdSearchData" runat="server" Width="95%">
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99px">
                                                <asp:Button ID="btnExportSearchData" runat="server" OnClick="btnExportSearchData_Click" Text="Export to Excel" Width="125px" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

                                </asp:Panel>

                                <asp:CollapsiblePanelExtender ID="PanelSearchBody_CollapsiblePanelExtender"
                                    runat="server" Enabled="True"
                                    CollapseControlID="PanelSearchHRD"
                                    ExpandControlID="PanelSearchHRD"
                                    TargetControlID="PanelSearchBody"
                                    Collapsed="true"
                                    TextLabelID="lblSearchHRD"
                                    CollapsedText="Search area"
                                    ExpandedText="Search area"
                                    AutoCollapse="False"
                                    AutoExpand="false"
                                    ExpandedImage="~/Images/expand.jpg"
                                    CollapsedImage="~/Images/collapse.jpg"
                                    ExpandDirection="Vertical"
                                    BehaviorID="colps5">
                                </asp:CollapsiblePanelExtender>

                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="PanelForDetails" runat="server" CssClass="cpHeaderContent" Width="100%">
                                    <asp:Label ID="Label4" Text="Details" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" align="left">
                                <asp:Panel ID="PanelForHeader" runat="server">
                                    <table style="width: auto;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblSelectionArea" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblSelectionValue" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblSelectedCondition" runat="server"></asp:Label>
                                            </td>
                                            <td>:</td>
                                            <td align="left">
                                                <asp:Label ID="lblSelectedEmployeeNumber" runat="server" Width="100px"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:Panel ID="PanelForSection" runat="server">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="height: 22px">
                                                                <asp:Label ID="Label9" runat="server" Text="Section"></asp:Label>
                                                            </td>
                                                            <td style="height: 22px">:</td>
                                                            <td style="height: 22px">
                                                                <asp:DropDownList ID="ddlSection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" Width="286px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <asp:Panel ID="PanelForParticularDept" runat="server">
                                    <asp:GridView ID="grdParticularDepartment" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdParticularDepartment_RowDataBound" ShowFooter="True" OnRowCommand="grdParticularDepartment_RowCommand" Width="500px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <%# Container.DisplayIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DesignationCode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignationCode" runat="server" Text='<%# Bind("DesgID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoofEmployee" runat="server" Text='<%# Bind("noOfEmployee") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentDepartment" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                            </asp:TemplateField>
                                            <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                        </Columns>

                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="PanelForParticularOffice" runat="server">
                                    <asp:GridView ID="grdParticularDesignation" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdParticularDesignation_RowDataBound" ShowFooter="True" Width="350px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <%# Container.DisplayIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartmentCode" runat="server" Text='<%# Bind("DeptID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("Dept") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoofEmployee" runat="server" Text='<%# Bind("noOfEmployee") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPercentDepartment" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Button ID="btnSeeMore" runat="server" OnClick="btnSeeMore_Click" Text="See More Details" Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnExportDetailsInformation" runat="server" OnClick="btnExportDetailsInformation_Click" Text="Export to Excel" Width="125px" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="grdDetails" runat="server" Width="100%" OnPageIndexChanging="grdDetails_PageIndexChanging" OnRowEditing="grdDetails_RowEditing">
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
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
                <script type="text/javascript">
                    function isNumberKey(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode
                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                            alert("Please Enter Only Numeric Value:");
                            return false;
                        }
                        return true;
                    }
                    $(document).ready(function () {
                        $("body").css("overflow", "hidden");
                        $("body").css("overflow-x", "hidden");
                        $("body").css("overflow-y", "hidden");
                    });

                    $(document).ready(function () {

                        $("body").css("overflow", "");

                    });

                    function closePanel(con1, con2, con3, con4) {
                        $find(con1)._doClose();
                        $find(con2)._doClose();
                        $find(con3)._doClose();
                        $find(con4)._doClose();
                        $find(con5)._doClose();
                        //alert('Hi, good morning!');
                    }
                </script>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportDetailsInformation" />
            <asp:PostBackTrigger ControlID="ddlSection" />
            <asp:PostBackTrigger ControlID="btnExportDepartment" />
            <asp:PostBackTrigger ControlID="btnExportOfficeLocation" />
            <asp:PostBackTrigger ControlID="btnExportDesignation" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnExportSearchData" />
            <asp:PostBackTrigger ControlID="btnSeeMore" />
            <asp:PostBackTrigger ControlID="ChartTotalEmployee" />
            <asp:PostBackTrigger ControlID="ChartDepartmentWise" />
            <asp:PostBackTrigger ControlID="ChartOfficeLocation" />
            <asp:PostBackTrigger ControlID="ChartDesignation" />
            <asp:PostBackTrigger ControlID="grdTotalActiveEmployee" />
            <asp:PostBackTrigger ControlID="grdDepartment" />
            <asp:PostBackTrigger ControlID="grdOfficelocation" />
            <asp:PostBackTrigger ControlID="grdDesignation" />
            <asp:PostBackTrigger ControlID="grdParticularDepartment" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
