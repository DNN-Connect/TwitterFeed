<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="View.ascx.vb" Inherits="Connect.Modules.TwitterFeed.View" %>


<asp:Panel ID="pnlSettingsIncomplete" runat="server" Visible="false">
    <p><asp:Literal ID="lblSettingsIncomplete" runat="server"></asp:Literal></p>
</asp:Panel>

<asp:Repeater ID="rptFeed" runat="server">
    <HeaderTemplate></HeaderTemplate>
    <ItemTemplate></ItemTemplate>
    <AlternatingItemTemplate></AlternatingItemTemplate>
    <FooterTemplate></FooterTemplate>
</asp:Repeater>

<div id="pnlClientSideTweets" class="ConnectTweetContainer" runat="server"></div>
