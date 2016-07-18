<%@ Page Language="C#" MasterPageFile="~/masMain.master" AutoEventWireup="true" CodeFile="frm_com_inbox.aspx.cs" Inherits="frm_inbox" Title=""   ValidateRequest="false" %>
<%@ Register src="../../UserControls/ucSelfService.ascx" tagname="ucSelfService" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >

    <table class="tblmas" style="width: 100%" id="tblmaster" runat="server">
        <tr>
            <td style="height: 22px">
                &nbsp;</td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
       
        <tr>
            <td style="height: 22px">
                &nbsp;</td>
            <td style="height: 22px">
                &nbsp;</td>
            <td style="height: 22px">
                &nbsp;</td>
            <td style="height: 22px">
                &nbsp;</td>
        </tr>
       
        <tr>
            <td style="height: 0px; text-align: center">
                &nbsp;</td>
            <td colspan="3" style="height: 0px; text-align: center">
                <table style="width:100%;overflow:hidden;" >
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <uc1:ucSelfService ID="ucSelfService1" runat="server" />
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkmpr" runat="server" CssClass="btn3" 
                                 Visible="False" PostBackUrl="~/modules/commercial/frm_mpr_approval.aspx" >MPR APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkforword" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_mpr_routing.aspx"  Visible="False">MPR FORWORDING PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkpiinq" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_collection.aspx"  Visible="False" 
                               >PI INQUIRY PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkcs" runat="server" CssClass="btn3"  PostBackUrl="~/modules/commercial/frm_quo_approval.aspx"  
                                Visible="False">CS APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkpientry" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_initiate.aspx"  Visible="False">PI ENTRY PENDING</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkquo" runat="server" CssClass="btn3" 
                                Visible="False" 
                                PostBackUrl="~/modules/commercial/frm_quotation_entry.aspx">QUOTETION ENTRY PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkpiapproval" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_approval.aspx"   
                                Visible="False">PI &amp; LC APPROVAL PENDING</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnktender" runat="server" CssClass="btn3" 
                                Visible="False" PostBackUrl="~/modules/commercial/frm_tender_inquiry.aspx" 
                               >TENDER INQUIRY PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkconsignment" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_consignment_entry.aspx"   
                                Visible="False">CONSIGNMENT ENTRY PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnklpocreate" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_order_create.aspx"  Visible="False">LPO CREATION PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkcustentry" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_custom_clearing_entry.aspx"   
                                Visible="False">CUSTOM CLEARING ENTRY PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkreal" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_spo_realize_approval.aspx"  Visible="False">SPO REALIZATION PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkcustapp" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_custom_clearing_app.aspx"   
                                Visible="False">CUSTOM CLEARING APPROVAL PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkporevise" runat="server" CssClass="btn3"  PostBackUrl="~/modules/commercial/frm_po_revising_app.aspx"  Visible="False">PO REVISE APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkctgentry" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_ctg_info_entry.aspx"   
                                Visible="False">BOAT NOTE QUANTITY ENTRY PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkpocancel" runat="server" CssClass="btn3"  PostBackUrl="~/modules/commercial/frm_po_canceling_app.aspx"  Visible="False">PO CANCEL APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkserveyentry" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_fpi_survey_loan_mrr.aspx"   
                                Visible="False">FACTORY SURVEY ENTRY PENDING</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkpoclose" runat="server" CssClass="btn3" 
                            PostBackUrl="~/modules/commercial/frm_po_closing_app.aspx"                                   
                                Visible="False">PO CLOSE APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkProdappbulk" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/production/frm_prod_approval_bulk.aspx"   
                                Visible="False">PRODUCTION APPROVAL PENDING (BULK)</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkmprreturn" runat="server" CssClass="btn3" 
                                 Visible="False" PostBackUrl="~/modules/commercial/frm_mpr_return_forward.aspx" >MPR ITEM RETURN PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkProdappbag" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/production/frm_prod_approval_bag.aspx"   
                                Visible="False">PRODUCTION APPROVAL PENDING (BAG)</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnksr" runat="server" CssClass="btn3" 
                                 Visible="False" PostBackUrl="~/modules/inventory/frm_sr_approval.aspx" >SR APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkProdpostbulk" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/production/frm_prod_post_bulk.aspx"   
                                Visible="False">PRODUCTION POST PENDING (BULK)</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkpayreq" runat="server" CssClass="btn3" 
                                Visible="False" 
                                PostBackUrl="~/modules/commercial/frm_pay_request_app.aspx">PAYMENT REQUEST PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            <asp:LinkButton ID="lnkProdpostbag" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/production/frm_prod_post_bag.aspx"   
                                Visible="False">PRODUCTION POST PENDING (BAG)</asp:LinkButton>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkspo" runat="server" CssClass="btn3" PostBackUrl="~/modules/commercial/frm_spo_approval.aspx"  Visible="False">SPO APPROVAL PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkspocreate" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_spo_create_ini.aspx"  Visible="False">SPO CREATION PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnksporealini" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_spo_realize_ini.aspx"  Visible="False">SPO REALIZATION INITIATE PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            <asp:LinkButton ID="lnkadrassign" runat="server" CssClass="btn3" 
                                 PostBackUrl="~/modules/commercial/frm_adr_code_assign.aspx"  Visible="False" 
                                >ADDRESS CODE ASSIGN PENDING</asp:LinkButton>
                        </td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 1px;">
                            </td>
                        <td style="text-align: left; height: 1px; width: 301px;">
                            &nbsp;</td>
                        <td style="text-align: left; height: 1px; width: 16px;">
                            &nbsp;</td>
                        <td style="height: 1px; text-align: left;">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: left">
                &nbsp;</td>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: left">
                &nbsp;</td>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="tbl" style="height: 24px; text-align: left">
                &nbsp;</td>
            <td class="tbl" colspan="3" style="height: 24px; text-align: left">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 57px">
                &nbsp;</td>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
            <td style="height: 57px">
            </td>
        </tr>
    </table>
      
</asp:Content>

