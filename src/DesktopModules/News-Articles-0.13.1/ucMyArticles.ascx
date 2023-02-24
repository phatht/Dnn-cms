<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucMyArticles.ascx.vb" Inherits="Ventrian.NewsArticles.ucMyArticles" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="article" TagName="Header" Src="ucHeader.ascx" %>
<%@ Register TagPrefix="Ventrian" TagName="Listing" Src="Controls/Listing.ascx" %>
<article:Header ID="ucHeader1" SelectedMenu="myArticles" runat="server" MenuPosition="Top"></article:Header>


<div class="dnnForm" id="tabs-myarticles">
    <div class="dnnRight">
        <asp:CheckBox ID="chkShowAll" runat="server" AutoPostBack="true" ResourceKey="ShowAll" CssClass="Normal" Checked="true" />
    </div>
    <ul class="dnnAdminTabNav ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
        <li class="ui-state-default ui-corner-top <%= IsSelected(1) %>"><a href="<%= GetModuleLink("MyArticles", 1)%>"><%= LocalizeString("MyArticles") %></a></li>
        <li class="ui-state-default ui-corner-top <%= IsSelected(2) %>"><a href="<%= GetModuleLink("MyArticles", 2)%>"><%= LocalizeString("Unapproved") %></a></li>
        <li class="ui-state-default ui-corner-top <%= IsSelected(3) %>"><a href="<%= GetModuleLink("MyArticles", 3)%>"><%= LocalizeString("Approved")%></a></li>
    </ul>
    <div id="MyArticles" class="table-responsive">
        <asp:DataGrid ID="grdMyArticles" Width="100%" AutoGenerateColumns="false" EnableViewState="false" runat="server" Border="0" GridLines="Both" CssClass="table table-striped table-bordered">
            <%-- <headerstyle cssclass="dnnGridHeader" verticalalign="Top"/>
            <itemstyle cssclass="dnnGridItem" horizontalalign="Left" />
            <alternatingitemstyle cssclass="dnnGridAltItem" />--%>
            <Columns>
                <asp:TemplateColumn>
                    <ItemStyle Width="20" />
                    <ItemTemplate>
                        
                        <a href="<%# GetEditUrl(DataBinder.Eval(Container.DataItem, "ArticleID").ToString()) %>">
                            <i class="fa fa-pencil" aria-hidden="true"></i></a>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <HeaderStyle CssClass="NormalBold" />
                    <ItemStyle CssClass="Normal" />
                    <HeaderTemplate><%= LocalizeString("Title.Header") %></HeaderTemplate>
                    <ItemTemplate>
                        <a href="<%# GetArticleLink(Container.DataItem) %>"><%#Eval("Title")%></a>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="AuthorDisplayName" HeaderText="AuthorFullName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" ItemStyle-Width="150" />
                <asp:TemplateColumn ItemStyle-Width="125">
                    <HeaderStyle CssClass="NormalBold" />
                    <ItemStyle CssClass="Normal" />
                    <HeaderTemplate><%= LocalizeString("CreatedDate.Header") %></HeaderTemplate>
                    <ItemTemplate>
                        <%#GetAdjustedCreateDate(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn ItemStyle-Width="125">
                    <HeaderStyle CssClass="NormalBold" />
                    <ItemStyle CssClass="Normal" />
                    <HeaderTemplate><%= LocalizeString("PublishDate.Header") %></HeaderTemplate>
                    <ItemTemplate>
                        <%#GetAdjustedPublishDate(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <dnn:PagingControl ID="ctlPagingControl" runat="server" Visible="false"></dnn:PagingControl>

        <asp:PlaceHolder ID="phNoArticles" EnableViewState="false" runat="server" Visible="false">
            <div class="dnnFormMessage dnnFormInfo"><%= LocalizeString("NoArticlesMessage") %></div>
        </asp:PlaceHolder>
    </div>
</div>

<article:Header ID="Header1" SelectedMenu="myArticles" runat="server" MenuPosition="Bottom"></article:Header>
