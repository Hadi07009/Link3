<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frmGradeSetup.aspx.cs" Inherits="modules_HRMS_Payroll_frmGradeSetup" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="ew" Namespace="eWorld.UI.Compatibility" Assembly="eWorld.UI.Compatibility" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:MessageBox ID="MessageBox1" runat="server" />
    <asp:UpdatePanel ID="updpnl2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelLeaveHdr" runat="server" CssClass="cpHeaderContent" Width="100%">
                <asp:Label ID="lblleave" Text=" EMPLOYEE GRADE SETUP" runat="server" />
            </asp:Panel>
            <asp:Panel ID="PanelLeaveDet" runat="server" CssClass="cpBodyContent" Width="100%" Height="100%">
                <table style="width: 99%; text-align: left">
                    <tr>
                        <td colspan="3" style="text-align: left">
                            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server"
                                ActiveTabIndex="1"
                                Width="100%" ScrollBars="None" BackColor="WhiteSmoke">
                                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Grade Define">
                                    <ContentTemplate>
                                        <table style="width: 99%; text-align: left">
                                            <tr>
                                                <td style="width: 127px"></td>
                                                <td style="width: 9px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 127px">
                                                    <asp:Label ID="Label2" runat="server" Text="Grade Description"></asp:Label>
                                                </td>
                                                <td style="width: 9px">:</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGradeDefination" runat="server" Width="200px"></asp:TextBox>
                                                    <asp:Label ID="lblGradeCodeForUpdate" runat="server" Visible="False"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width: 127px">&nbsp;</td>
                                                <td style="width: 9px">&nbsp;</td>
                                                <td align="left">
                                                    <asp:Button ID="btnSaveGradeDefination" runat="server" OnClick="btnSaveGradeDefination_Click" Text="Save" Width="100px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 127px">&nbsp;</td>
                                                <td style="width: 9px">&nbsp;</td>
                                                <td align="left">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="grdLoadGradeDefination" runat="server" AutoGenerateColumns="False" OnRowCommand="grdLoadGradeDefination_RowCommand" OnRowDataBound="grdLoadGradeDefination_RowDataBound" OnRowDeleting="grdLoadGradeDefination_RowDeleting" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <%# Container.DisplayIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Grade Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGradeCode" runat="server" Text='<%# Bind("Grade_Def_Code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Grade Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGradeDescription" runat="server" Text='<%# Bind("Grade_Def_Desc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
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
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Grade Setup">
                                    <ContentTemplate>
                                        <table style="width: 99%; text-align: left">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="Panel1" runat="server" Width="600px">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 120px">
                                                                        <asp:Label ID="Label3" runat="server" Text="Select Grade"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>:</td>
                                                                <td colspan="2">
                                                                    <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 120px">
                                                                        <asp:Label ID="Label4" runat="server" Text="Select Formula"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>:</td>
                                                                <td colspan="2">
                                                                    <asp:DropDownList ID="ddlFormula" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFormula_SelectedIndexChanged" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width: 120px">
                                                                        <asp:Label ID="Label5" runat="server" Text="Selection Criteria"></asp:Label>
                                                                    </div>
                                                                </td>
                                                                <td>:</td>
                                                                <td colspan="2">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td align="left">
                                                                    <div style="width: 400px">
                                                                        <div style="width: 185px; float: left">
                                                                            <asp:RadioButtonList ID="rblSelectionCriteria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblSelectionCriteria_SelectedIndexChanged" Width="182px">
                                                                                <asp:ListItem Selected="True" Value="C">Specify Condition</asp:ListItem>
                                                                                <asp:ListItem Value="V">Enter Value</asp:ListItem>
                                                                                <asp:ListItem Value="W">Without Value</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                        <div style="width: 215px; float: right">
                                                                            <table style="width: 99%;">
                                                                                <tr>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <asp:TextBox ID="txtSelectionValue" runat="server" Width="200px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                                <td align="left">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="PanelForCondition" runat="server" Width="100%">
                                                        <table style="width: 100%;" width="100%">
                                                            <tr class="forConditionPortion">
                                                                <td style="width: 130px" align="right">
                                                                    <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="Formula"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text="Conditional Operator"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text="Value"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlConditionFormula1" runat="server" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlConditionOperator1" runat="server" Width="250px">
                                                                        <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                                                        <asp:ListItem>==</asp:ListItem>
                                                                        <asp:ListItem>!=</asp:ListItem>
                                                                        <asp:ListItem>&gt; </asp:ListItem>
                                                                        <asp:ListItem>&lt; </asp:ListItem>
                                                                        <asp:ListItem>&gt;=</asp:ListItem>
                                                                        <asp:ListItem>&lt;=</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtConditionalValue1" runat="server" Width="245px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td align="left">
                                                                    <asp:RadioButtonList ID="rblAndOr" runat="server" RepeatDirection="Horizontal" Width="125px">
                                                                        <asp:ListItem Selected="True" Value="A">AND</asp:ListItem>
                                                                        <asp:ListItem Value="O">OR</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style8"></td>
                                                                <td class="auto-style8"></td>
                                                                <td class="auto-style8">
                                                                    <asp:DropDownList ID="ddlConditionFormula2" runat="server" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="auto-style8">
                                                                    <asp:DropDownList ID="ddlConditionOperator2" runat="server" Width="250px">
                                                                        <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                                                        <asp:ListItem>==</asp:ListItem>
                                                                        <asp:ListItem>!=</asp:ListItem>
                                                                        <asp:ListItem>&gt; </asp:ListItem>
                                                                        <asp:ListItem>&lt; </asp:ListItem>
                                                                        <asp:ListItem>&gt;=</asp:ListItem>
                                                                        <asp:ListItem>&lt;=</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="auto-style8">
                                                                    <asp:TextBox ID="txtConditionalValue2" runat="server" Width="245px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td colspan="3">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td colspan="3">&nbsp;</td>
                                                            </tr>
                                                            <tr class="forConditionPortion">
                                                                <td align="right">
                                                                    <asp:Label ID="Label10" runat="server" Text="Outcome"></asp:Label>
                                                                </td>
                                                                <td>:</td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="Formula"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="Operator"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="Formula"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOutcomeFormula" runat="server" Width="250px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOutcomeOperator" runat="server" Width="250px">
                                                                        <asp:ListItem Selected="True" Value="-1">--- Please Select ---</asp:ListItem>
                                                                        <asp:ListItem>- </asp:ListItem>
                                                                        <asp:ListItem>* </asp:ListItem>
                                                                        <asp:ListItem>/ </asp:ListItem>
                                                                        <asp:ListItem>% </asp:ListItem>
                                                                        <asp:ListItem>=</asp:ListItem>
                                                                        <asp:ListItem>++ </asp:ListItem>
                                                                        <asp:ListItem>+ </asp:ListItem>
                                                                        <asp:ListItem>--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlOutcomeFormula2" runat="server" Width="250px">
                                                                    </asp:DropDownList>
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
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBoxPaySetup" runat="server" Text="Show in PaySetup Default" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBoxValueDefault" runat="server" Text="Show Value Default" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" Width="140px" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="140px" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="grdGetSelectedValue" runat="server" AutoGenerateColumns="False" OnPreRender="grdGetSelectedValue_PreRender" OnRowCommand="grdGetSelectedValue_RowCommand" OnRowDataBound="grdGetSelectedValue_RowDataBound" OnRowDeleting="grdGetSelectedValue_RowDeleting" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLine" runat="server" Text='<%# Bind("Line") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Formula Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFormulaValue" runat="server" Text='<%# Bind("FormulaValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FormulaText">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFormulaText" runat="server" Text='<%# Bind("FormulaText") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Selection Criteria Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSelectionCriteriaValue" runat="server" Text='<%# Bind("SelectionCriteriaValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Selection Criteria Text">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSelectionCriteriaText" runat="server" Text='<%# Bind("SelectionCriteriaText") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition Formula1 Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionFormula1Value" runat="server" Text='<%# Bind("ConditionFormula1Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition Formula1 Text">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionFormula1Text" runat="server" Text='<%# Bind("ConditionFormula1Text") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition Operator1 Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionOperator1Value" runat="server" Text='<%# Bind("ConditionOperator1Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition Value1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionValue1" runat="server" Text='<%# Bind("ConditionValue1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition AndOr Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionAndOrValue" runat="server" Text='<%# Bind("ConditionAndOrValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Condition AndOr Text">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConditionAndOrText" runat="server" Text='<%# Bind("ConditionAndOrText") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome Formula1 Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOutcomeFormula1Value" runat="server" Text='<%# Bind("OutcomeFormula1Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome Formula1 Text">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOutcomeFormula1Text" runat="server" Text='<%# Bind("OutcomeFormula1Text") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome Operator Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOutcomeOperatorValue" runat="server" Text='<%# Bind("OutcomeOperatorValue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome Formula2 Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOutcomeFormula2Value" runat="server" Text='<%# Bind("OutcomeFormula2Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outcome Formula2 Text">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOutcomeFormula2Text" runat="server" Text='<%# Bind("OutcomeFormula2Text") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paysetup Default">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaysetupDefault" runat="server" Text='<%# Bind("PaysetupDefault") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value Default">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueDefault" runat="server" Text='<%# Bind("ValueDefault") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Center" />
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
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 143px">&nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="TabContainer1$TabPanel1$btnAdd" />
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

