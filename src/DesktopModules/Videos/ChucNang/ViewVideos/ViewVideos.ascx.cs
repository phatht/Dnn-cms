using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using Modules.Videos.Components;
using Modules.Videos.Model;
using Modules.Videos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNSkins = DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using System.Globalization;

namespace Modules.Videos.ChucNang.ViewVideos
{
    public partial class ViewVideos : VideosModuleBase
    {
        #region Khai Bao
       // protected readonly ILog log = LogManager.GetLogger(typeof(ViewVideos));
        VideoController controller = new VideoController();
        Int32 scope = -1;
        #endregion
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                Page.MaintainScrollPositionOnPostBack = true;
                //Get LastedVideo
                if (!Page.IsPostBack)
                {
                    if (Settings["DiplayTabId"] != null && Settings.Contains("DiplayTabId"))
                        scope = Convert.ToInt32(Settings["DiplayTabId"].ToString(), CultureInfo.InvariantCulture);
                    if (scope == -1)
                    {

                        // there is no media yet
                        DNNSkins.Skin.AddModuleMessage(this, GetLocalizedString("NoMediaMessage.Text"),
                                                       ModuleMessage.ModuleMessageType.BlueInfo);
                    }

                    var lastedVideo = LoadAllVideoVM(scope);
                    if (lastedVideo != null && lastedVideo.Count>0)
                    {
                        GetTypeVideoForPlay(lastedVideo.FirstOrDefault().videoID, scope);
                        //Load Carousel
                        cdcatalog.DataSource = lastedVideo.Take(20).ToList();
                        cdcatalog.DataBind();
                    }
                    else
                    {
                        // there is no media yet
                        DNNSkins.Skin.AddModuleMessage(this, GetLocalizedString("NoMediaMessage.Text"),
                                                       ModuleMessage.ModuleMessageType.BlueInfo);
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
        #region SuKien

        //Khi nguoi dung click video tren carousel
        protected void lbtPlayVideo_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _tabid = 0;
                if (Settings["DiplayTabId"] != null && Settings.Contains("DiplayTabId"))
                    _tabid = Convert.ToInt32(Settings["DiplayTabId"].ToString(), CultureInfo.InvariantCulture);
                var videoID = Int32.Parse((sender as ImageButton).CommandArgument);
                GetTypeVideoForPlay(videoID, _tabid);
                cdcatalog.DataSource = LoadAllVideoVM(_tabid).Take(20).ToList();
                cdcatalog.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);

            }
        }

        protected void lbtPlayVideo_Click1(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                Int32 _tabid = 0;
                if (Settings["DiplayTabId"] != null && Settings.Contains("DiplayTabId"))
                    _tabid = Convert.ToInt32(Settings["DiplayTabId"].ToString(), CultureInfo.InvariantCulture);

                var videoID = Int32.Parse((sender as ImageButton).CommandArgument);
                GetTypeVideoForPlay(videoID, _tabid);
                cdcatalog.DataSource = LoadAllVideoVM(_tabid).Take(20).ToList();
                cdcatalog.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);

            }
        }

        #endregion




        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        #region Phuong thuc
        //Phan loai kieu Video can hien thi
        public void GetTypeVideoForPlay(int videoID, int portalID)
        {
            try
            {
                Video video = controller.GetVideo(videoID, portalID);
                if (!string.IsNullOrEmpty(video.Src))
                {
                    switch (video.VideosType)
                    {
                        case 1:
                            video.Src = controller.GetFileUrlById(video.Src);
                            lblMp4.Text = "<video width = '" + video.width + "' height = '" + video.height + "' controls " + (video.AutoStart == true ? "autoplay " : " ") + "" + (video.VideosLoop == true ? "loop " : " ") + "><source src = '" + video.Src + "' type = 'video/mp4'></video>";
                            lblTitle.Text = video.Title;
                            lblTimeUpdate.Text = video.LastUpdatedDate.ToString("dd/MM/yyyy h:mm tt");
                            lblContent.Text = video.Description;
                            hiddenFieldYtb.Style.Add("display", "none");
                            hiddenFieldMp4.Style.Add("display", "block");
                            lblYtb.Text = "";
                            break;

                        case 2:
                            lblYtb.Text = video.Src;
                            lblTitle.Text = video.Title;
                            lblTimeUpdate.Text = video.LastUpdatedDate.ToString("dd/MM/yyyy h:mm tt");
                            lblContent.Text = video.Description;
                            hiddenFieldMp4.Style.Add("display", "none");
                            hiddenFieldYtb.Style.Add("display", "block");
                            lblMp4.Text = "";
                            break;

                        case 3:
                            lblMp4.Text = "<video width='" + video.width.ToString() + "' height='" + video.height.ToString() + "' controls " + (video.AutoStart == true ? "autoplay " : " ") + "" + (video.VideosLoop == true ? "loop " : " ") + "><source src = '" + video.Src + "' type = 'video/mp4'></video>";
                            lblTitle.Text = video.Title;
                            lblTimeUpdate.Text = video.LastUpdatedDate.ToString("dd/MM/yyyy h:mm tt");
                            lblContent.Text = video.Description;
                            hiddenFieldYtb.Style.Add("display", "none");
                            hiddenFieldMp4.Style.Add("display", "block");
                            lblYtb.Text = "";
                            break;
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);

            }
        }
        
        //Do du lieu tu Database vao VideoVM
        protected List<VideoVM> LoadAllVideoVM(int portalID)
        {
            IEnumerable<Video> listUser = controller.GetVideos(portalID);
            List<VideoVM> listUserVM = new List<VideoVM>();
            try
            {       
                foreach (var item in listUser)
                {
                    VideoVM x = new VideoVM();
                    x.videoID = item.VideoID;
                    x.tabID = item.TabId;
                    x.portalID = item.PortalID;
                    x.title = item.Title;
                    x.description = item.Description;
                    x.userName = UserController.GetUserById(PortalId, item.LastUpdatedBy).DisplayName;
                    x.lastUpdatedDate = item.LastUpdatedDate;
                    x.videosType = item.VideosType;

                    if (!string.IsNullOrEmpty(item.ImgVideo))
                    {
                        switch (item.VideosType)
                        {
                            case 1:                             
                                x.imageVideo = controller.GetFileUrlById(item.ImgVideo);
                                break;
                            case 2:
                                if (!item.ImgVideo.Contains("ytimg"))
                                {
                                    x.imageVideo = controller.GetFileUrlById(item.ImgVideo);
                                }
                                else
                                {
                                    x.imageVideo = item.ImgVideo;
                                };
                                break;
                            case 3:
                                x.imageVideo = controller.GetFileUrlById(item.ImgVideo);
                                break;
                        }
                    }
                    else
                    {
                        x.imageVideo = "/DesktopModules/Videos/Contents/img/Nothumnail.png";
                    }
                    listUserVM.Add(x);
                   
                }
                return listUserVM.OrderByDescending(x => x.lastUpdatedDate).ToList();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                return null;
            }
          
        }




        #endregion


    }
}