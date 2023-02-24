<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Modules.Videos.ChucNang.Videos.Edit" %>
<%@ Register TagName="url" TagPrefix="dnn" Src="~/controls/DnnUrlControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<link href="/DesktopModules/Videos/Contents/css/video.css" rel="stylesheet" />

<div>
<form class="form-horizontal" role="form">
    <div class="form-group">
         <label for="lblTitle" class="col-sm-2 control-label">Tiêu đề *</label>
        <div class="col-sm-10">
            <input type="text" runat="server" required="required" class="form-control" id="txtTitle" maxlength="500" placeholder="Nhập tiêu đề (tối đa 500 ký tự)">
            <asp:RequiredFieldValidator ID="valTitle" SetFocusOnError="false" ValidationGroup="valgSubmit" runat="server" ControlToValidate="txtTitle" Text="" ErrorMessage="Bắt buộc nhập !" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="form-group">
        <label for="lblDescription" class="col-sm-2 control-label">Mô tả *</label>
        <div class="col-sm-10">
            <textarea id="txtDescriptions" required="required" class="form-control" runat="server" maxlength="1000" rows="5" placeholder="Nhập tiêu đề (tối đa 1000 ký tự)"></textarea>
            <asp:RequiredFieldValidator ID="valDes" SetFocusOnError="false" ValidationGroup="valgSubmit" runat="server" ControlToValidate="txtDescriptions" Text="" ErrorMessage="Bắt buộc nhập !" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="form-group" style="text-align: center;margin:auto;">
         <label for="lblFile" class="col-sm-2 control-label">Video</label>
        <div class="col-sm-10">
        <asp:RadioButtonList ID="rdoType" runat="server"
            Font-Size="Larger" ForeColor="Black"
            TextAlign="Right" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">File</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">Embed Code</asp:ListItem>
            <asp:ListItem Value="3">Website URL</asp:ListItem>
        </asp:RadioButtonList>
            </div>
    </div>
    <div class="form-group" runat="server" id="hiddenFile" style="display: none">
        <label for="lblFileUpload" class="col-sm-2 control-label">Upload File</label>
        <div class="col-sm-10">
            <dnn:url ID="urlFile" runat="server" Width="445" ShowTabs="False" ShowUrls="False" UrlType="F" ShowTrack="False"
            ShowLog="False" Required="False" FileFilter="mp4"></dnn:url>   
        </div>
    </div>
         <div class="form-group" id="hiddenEmbed" runat="server">       
        <label for="lblEmbeds" class="col-sm-2 control-label">Embed Code Youtube</label>
        <div class="col-sm-10">
            <textarea id="txtEmbeds" class="form-control" runat="server" rows="5"></textarea>
        </div>
    </div>
    <div class="form-group" id="hiddenUrl" runat="server" style="display: none">  
        <label for="lblUrlCode" class="col-sm-2 control-label">Website Url</label>
        <div class="col-sm-10">
            <textarea id="txtUrls" class="form-control" runat="server" rows="5" placeholder="Url for MP4 file"></textarea>
        </div>
    </div>
    <div class="form-group">
        <label for="lblImageUpload" class="col-sm-2 control-label">Hình ảnh video</label>
        <div class="col-sm-10">
            <dnn:url ID="urlImageUpload" runat="server" Width="445" ShowTabs="False" ShowUrls="False" UrlType="F" ShowTrack="False"
                ShowLog="False" Required="False" FileFilter="gif,jpg,png,jpeg"></dnn:url>
        </div>
    </div>
    <div class="form-group" style="margin-left:18%;" runat="server" id="hiddenImageUpload">
         <asp:CheckBox ID="cblVideoLoop" runat="server" Text="Videos Loop" />
        <asp:CheckBox ID="cblAutoStart" runat="server" Text="Auto Start" />
    </div>
    <div class="form-group">
        <label for="lblWidth" class="col-sm-2 control-label">Rộng *</label>
        <div class="col-sm-10">
            <input type="text" runat="server" class="form-control" required="required" id="txtWidths">
            <asp:RequiredFieldValidator ID="valiWidth" SetFocusOnError="false" runat="server" ValidationGroup="valgSubmit" ControlToValidate="txtWidths" Text="" ErrorMessage="Bắt buộc nhập !" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="valWidth" runat="server" ControlToValidate="txtWidths" Type="Integer" ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Sai kiểu nhập!" />
        </div>
    </div>
    <div class="form-group">
    <label for="txtHeight" class="col-sm-2 control-label">Cao *</label>
        <div class="col-sm-10">
            <input type="text" runat="server" class="form-control" required="required" id="txtHeights">
            <asp:RequiredFieldValidator ID="valiHeight" SetFocusOnError="false" runat="server" ValidationGroup="valgSubmit" ControlToValidate="txtHeights" Text="" ErrorMessage="Bắt buộc nhập !" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="valHeight" runat="server" ControlToValidate="txtHeights" Type="Integer" ForeColor="Red" Operator="DataTypeCheck" ErrorMessage="Sai kiểu nhập!" />
        </div>
     </div>

    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="valgSubmit" OnClick="btnSubmit_Click"
        CssClass="button btn-text" Text="Cập nhật"/>
            <asp:LinkButton ID="btnCancel" runat="server" CssClass="button btn-text" Text="Bỏ qua" OnClick="btnCancel_Click" />
        </div>
    </div>
</form>
<asp:HiddenField ID="hdfvideoID" runat="server" Value="0" />
</div>

<script type="text/javascript">

    $(function () {
        var radios = $("[id*=rdoType] input[type=radio]");

        radios.change(function () {
            var rbvalue = $(this).val();

            if (rbvalue == "1") {
                $("#<%=hiddenFile.ClientID%>").css("display", "block");
                $("#<%=hiddenEmbed.ClientID%>").css("display", "none");
                $("#<%=hiddenUrl.ClientID%>").css("display", "none");
            } else if (rbvalue == "2") {
                $("#<%=hiddenFile.ClientID%>").css("display", "none");
                $("#<%=hiddenEmbed.ClientID%>").css("display", "block");
                $("#<%=hiddenUrl.ClientID%>").css("display", "none");
            } else {
                $("#<%=hiddenFile.ClientID%>").css("display", "none");
                $("#<%=hiddenEmbed.ClientID%>").css("display", "none");
                $("#<%=hiddenUrl.ClientID%>").css("display", "block");
            }
        });
    });


</script>