<%@ Control AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Containers.Container" %>
<%@ Register TagPrefix="dnn" TagName="TITLE" Src="~/Admin/Containers/Title.ascx" %>

<div class="contraC main">
    <h5 class="arrow"><dnn:TITLE runat="server" id="dnnTITLE"/></h5>
    <div id="ContentPane" runat="server" class="contentpane"></div>
</div>