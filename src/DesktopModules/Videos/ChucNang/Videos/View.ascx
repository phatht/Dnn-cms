<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Modules.Videos.ChucNang.Videos.View" %>
<asp:UpdatePanel ID="UpdatePanelContent" runat="server">
    <ContentTemplate>
        <div class="dnnFormItem">
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Tìm kiếm..." />
            <asp:LinkButton ID="btnTimKiem" runat="server" Text="Tìm" OnClick="btnTimKiem_Click" CssClass="button btn-text"/>
        </div>
        <asp:Label ID="lblKetQuaSearch" runat="server" ForeColor="Blue" Font-Size="Larger"></asp:Label>

        <asp:GridView ID="grvVideos" runat="server"
            AutoGenerateColumns="False"
            ShowFooter="True" DataKeyNames="VideoID"
            AllowPaging="True"
            OnPageIndexChanging="grvVideos_PageIndexChanging"
            PageSize="10" Style="border-width: 0;"
            CssClass="table table-striped table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="Không có dữ liệu">
            <PagerStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="STT" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tiêu đề">
                    <ItemTemplate>
                        <asp:Label Text='<%#string.Concat(Eval("title").ToString().PadRight(140).Substring(0,50).TrimEnd(),"...")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mô tả ngắn">
                    <ItemTemplate>
                        <asp:Label Text='<%#string.Concat(Eval("description").ToString().PadRight(140).Substring(0,100).TrimEnd(),"..." )%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Người đăng" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label Text='<%#Eval("userName").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cập nhật cuối" ItemStyle-Width="110px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%-- <asp:Label Text='<%#Eval("lastUpdatedDate","{0:hh.mm.ss tt}").ToString() %>' runat="server" />,<br />--%>
                        <asp:Label Text='<%#Eval("lastUpdatedDate","{0:dd/MM/yyyy}").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="ibtnEdit" OnClick="Edit_Click" ToolTip="Chỉnh sửa" ImageUrl="~/DesktopModules/Videos/Contents/img/edit.png" Width="20px" Height="20px" />
                        <asp:ImageButton runat="server" ID="ibtnDelete" OnClick="Delete_Click" OnClientClick="return confirm('Bạn có thực sự muốn xóa');" ToolTip="Xóa" ImageUrl="~/DesktopModules/Videos/Contents/img/delete.png" Width="20px" Height="20px" />
                        <asp:HiddenField runat="server" ID="hdfVideoID" Value='<%#Eval("VideoID") %>' />
                        <asp:HiddenField runat="server" ID="hdfCurrentPageIndex" Value='<%# grvVideos.PageIndex.ToString()%>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ID="ibtnCreate" runat="server" ToolTip="Thêm mới" ImageUrl="~/DesktopModules/Videos/Contents/img/add.png" OnClick="Create_Click" CommandName="Add" Width="20px" Height="20px" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grvVideos" />
    </Triggers>
</asp:UpdatePanel>


<asp:LinkButton ID="btnAdd" runat="server"     OnClick="btnAdd_Click" r Text="Thêm" CssClass="button btn-text" Visible="false" />
