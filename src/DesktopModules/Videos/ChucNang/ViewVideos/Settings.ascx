<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Modules.Videos.ChucNang.ViewVideos.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
         <div class="dnnFormItem">	       
	       <asp:Label ID="plPage" Text="Page"  runat="server" CssClass="Normal" />&nbsp;
		    <asp:DropDownList ID="drpPage" DataValueField="TabId" DataTextField="TabName" runat="server" />
        </div>
       
    </fieldset>


