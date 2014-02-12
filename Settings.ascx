<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="Settings.ascx.vb" Inherits="Connect.Modules.TwitterFeed.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

    <h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("AuthenticationSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblTokenKey" runat="server" />  
            <asp:TextBox ID="txtTokenKey" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblTokenSecret" runat="server" />
            <asp:TextBox ID="txtTokenSecret" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblConsumerKey" runat="server" />  
            <asp:TextBox ID="txtConsumerKey" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblConsumerSecret" runat="server" />
            <asp:TextBox ID="txtConsumerSecret" runat="server" />
        </div>
    </fieldset>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("DisplaySettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblDisplaymode" runat="server" />  
            <asp:DropDownList ID="drpDisplaymode" runat="server">
                <asp:ListItem Text="UserTimeLine" Value="U"></asp:ListItem>
                <asp:ListItem Text="CustomSearch" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblSearchFor" runat="server" />
            <asp:TextBox ID="txtSearchFor" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblCount" runat="server" />
            <asp:DropDownList ID="drpCount" runat="server">
            </asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblInterval" runat="server" />
            <asp:DropDownList ID="drpInterval" runat="server">
            </asp:DropDownList>
        </div>
        <div class="dnnFormItem">
            <dnn:Label ID="lblTemplate" runat="server" />  
            <asp:DropDownList ID="drpTemplate" runat="server">
            </asp:DropDownList>
        </div>

        <div class="dnnFormItem">
            <dnn:Label ID="lblRenderingMode" runat="server" />  
            <asp:DropDownList ID="drpRenderingMode" runat="server">
                <asp:ListItem Text="Client" Value="C"></asp:ListItem>
                <asp:ListItem Text="Server" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </div>


    </fieldset>