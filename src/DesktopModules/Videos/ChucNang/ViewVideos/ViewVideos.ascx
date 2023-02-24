<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewVideos.ascx.cs" Inherits="Modules.Videos.ChucNang.ViewVideos.ViewVideos" %>
<link rel="stylesheet" href="/DesktopModules/Videos/Contents/css/swiper.min.css">
<%--<link href="/DesktopModules/Videos/Contents/css/carousel.css" rel="stylesheet" />--%>
<link href="/DesktopModules/Videos/Contents/css/video.css" rel="stylesheet" />
<asp:UpdatePanel ID="UpdatePanelContent" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-lg-6 col-md-12 col-sm-12">
                <div id="hiddenFieldYtb" runat="server">
                    <asp:Label runat="server" ID="lblYtb"></asp:Label>
                </div>
                <div id="hiddenFieldMp4" runat="server">
                    <asp:Label runat="server" ID="lblMp4"></asp:Label>
                </div>
            </div>
            <div class="col-lg-6 col-md-12 col-sm-12" >
                <h2 class="MainTitle">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label></h2>
                <p style="color: darkgrey">
                    <asp:Label runat="server" ID="lblTimeUpdate"></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" ID="lblContent"></asp:Label>
                </p>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cdcatalog" />
    </Triggers>
</asp:UpdatePanel>
<br />
<div class="bx-example1-style">
    <ul class="bx-example1">
        <asp:Repeater ID="cdcatalog" runat="server">
            <ItemTemplate>
                <li>
                    <asp:ImageButton runat="server" ID="lbtPlayVideo" CssClass="imghover" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"videoID").ToString() %>' OnClick="lbtPlayVideo_Click1" ImageUrl='<%#DataBinder.Eval(Container.DataItem,"imageVideo").ToString() %>' Width="95%" Height="150px" onError="this.onerror=null;this.src='/DesktopModules/Videos/Contents/img/SadFace.png';" />
                    <p id="pTimeOfTitleInSwiper"><%# (Convert.ToDateTime(Eval("lastUpdatedDate"))).ToString("dd/MM/yyyy h:mm tt")  %></p>
                    <p id="pContentOfTitleInSwiper"><%#string.Concat(Eval("title").ToString().PadRight(140).Substring(0,50).TrimEnd(),"...")%></p>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
