using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.UI.WebControls;
using Modules.Videos.Components;
using Modules.Videos.Model;
using System;
using System.Text.RegularExpressions;

namespace Modules.Videos.ChucNang.Videos
{
    public partial class Edit : VideosModuleBase
    {
      //  protected readonly ILog log = LogManager.GetLogger(typeof(Edit));

        #region Khai Bao
        VideoController controller = new VideoController();
        #endregion
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Truyen gia tri ID can chinh sua(neu co)
                    if (!string.IsNullOrEmpty(Request.QueryString["VideoID"]))
                    {
                        //Hien thi noi dung Video can chinh sua(neu co)
                        hdfvideoID.Value = Request.QueryString["VideoID"];
                        LoadCurrentVideo(Int32.Parse(Request.QueryString["VideoID"]), TabId);
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

          
        }


        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        #region Phuong thuc
        //Hien thi noi dung Video can chinh sua()
        private void LoadCurrentVideo(int videoID,int portalID)
        {
            try
            {
                //Load du lieu len cac textbox neu dang chon che do Chinh Sua
                Video video = controller.GetVideo(videoID, portalID);
                txtTitle.Value = video.Title;
                txtDescriptions.Value = video.Description;
                txtWidths.Value = video.width.ToString();
                txtHeights.Value = video.height.ToString();
                cblAutoStart.Checked = video.AutoStart;
                cblVideoLoop.Checked = video.VideosLoop;

                //Da luu tren Portal hoac da duoc get tu Link Youtube
                if (!video.ImgVideo.Contains("ytimg")) 
                {((DnnUrlControl)urlImageUpload).Url = video.ImgVideo;}

                //Hien thi theo Video Type
                switch (video.VideosType)
                {
                    case 1:
                        rdoType.SelectedValue = "1";
                        ((DnnUrlControl)urlFile).Url = video.Src;
                        hiddenFile.Style.Add("display", "block");
                        hiddenUrl.Style.Add("display", "none");
                        hiddenEmbed.Style.Add("display", "none");
                        break;

                    case 2:
                        rdoType.SelectedValue = "2";
                        txtEmbeds.Value = video.Src;                 
                        hiddenFile.Style.Add("display", "none");
                        hiddenUrl.Style.Add("display", "none");
                        hiddenEmbed.Style.Add("display", "block");
                        break;

                    case 3:
                        rdoType.SelectedValue = "3";
                        txtUrls.Value = video.Src;
                        hiddenFile.Style.Add("display", "none");
                        hiddenUrl.Style.Add("display", "block");
                        hiddenEmbed.Style.Add("display", "none");
                        break;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }
        public static string GetYouTubeId(string url)
        {
            var regex = @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?|watch)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})";

            var match = Regex.Match(url, regex);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
                return string.Empty;
        }
        #endregion



        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        #region Su kien
        //Su kien Them moi va Chinh sua thong tin
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //Che do Chinh sua Video
                if (Int32.Parse(hdfvideoID.Value) > 0)
                {
                    Video video = controller.GetVideo(Int32.Parse(hdfvideoID.Value), TabId);
                    video.Title = txtTitle.Value;
                    video.Description = txtDescriptions.Value;
                    video.LastUpdatedBy = UserInfo.UserID;
                    video.AutoStart = cblAutoStart.Checked;
                    video.VideosLoop = cblVideoLoop.Checked;
                    video.height = Int32.Parse(txtHeights.Value);
                    video.width = Int32.Parse(txtWidths.Value);
                    video.VideosType = Int32.Parse(rdoType.SelectedItem.Value);

                    switch (video.VideosType)
                    {
                        case 1:
                            video.Src = ((DnnUrlControl)urlFile).Url.ToString();

                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }                            
                            else
                            {
                                video.ImgVideo = video.ImgVideo;
                            }
                            break;

                        case 2:
                            video.Src = txtEmbeds.Value;
                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }
                            //Neu ma Embed lay tu Youtube
                            else if (video.Src.Contains("youtube") && string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                string youtubid = GetYouTubeId(video.Src);
                                if (!string.IsNullOrEmpty(youtubid))
                                {
                                    video.ImgVideo = "https://i.ytimg.com/vi/" + youtubid  + @"/mqdefault.jpg";
                                }
                                else
                                {
                                    video.ImgVideo = video.ImgVideo;
                                }
                                
                            }
                            else
                            {
                                video.ImgVideo = video.ImgVideo;
                            }
                            break;

                        case 3:
                            video.Src = txtUrls.Value;
                          
                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }
                           
                            else
                            {
                                video.ImgVideo = video.ImgVideo;
                            }
                            break;
                    }
                    video.LastUpdatedDate = DateTime.Now;
                    controller.UpdateVideo(video);
                }
                //Che do Them moi Video
                else
                {
                    Video video = new Video();
                    video.PortalID = PortalId;
                    video.TabId = TabId;
                    video.Title = txtTitle.Value;
                    video.Description = txtDescriptions.Value;
                    video.LastUpdatedBy = UserInfo.UserID;
                    video.AutoStart = cblAutoStart.Checked;
                    video.VideosLoop = cblVideoLoop.Checked;
                    video.width = Int32.Parse(txtWidths.Value);
                    video.height = Int32.Parse(txtHeights.Value);
                    video.VideosType = Int32.Parse(rdoType.SelectedItem.Value);
                    switch (video.VideosType)
                    {
                        case 1:
                            video.Src = ((DnnUrlControl)urlFile).Url.ToString();
                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }                          
                            else
                            {
                                video.ImgVideo = "";
                            }
                            break;


                        case 2:
                            video.Src = txtEmbeds.Value;
                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }
                            //Neu ma Embed lay tu Youtube
                            else if (video.Src.Contains("youtube") && string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                string youtubid = GetYouTubeId(video.Src);
                                if (!string.IsNullOrEmpty(youtubid))
                                {
                                    video.ImgVideo = "https://i.ytimg.com/vi/" + youtubid + @"/mqdefault.jpg";
                                }
                                else
                                {
                                    video.ImgVideo = "";
                                }
                            }
                            else
                            {
                                video.ImgVideo = "";
                            }
                            break;


                        case 3:
                            video.Src = txtUrls.Value;

                            if (!string.IsNullOrEmpty(((DnnUrlControl)urlImageUpload).Url.ToString()))
                            {
                                video.ImgVideo = ((DnnUrlControl)urlImageUpload).Url.ToString();
                            }                          
                            else
                            {
                                video.ImgVideo = "";
                            }
                            break;
                    }
                    video.LastUpdatedDate = DateTime.Now;
                    controller.CreateVideo(video);
                }
                string miUrl = DotNetNuke.Common.Globals.NavigateURL(null, "View", null);
                Response.Redirect(miUrl, true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        //Su kien Huy Luu thong tin
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string miUrl = DotNetNuke.Common.Globals.NavigateURL(null, "View", null);
                Response.Redirect(miUrl, true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        #endregion
    }
}