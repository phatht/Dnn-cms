using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using Modules.Videos.Components;
using Modules.Videos.Model;
using Modules.Videos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Modules.Videos.ChucNang.Videos
{

    public partial class View : VideosModuleBase
    {
        #region Khai Bao
       // protected readonly ILog log = LogManager.GetLogger(typeof(View));
        VideoController controller = new VideoController();
        #endregion
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    var list = LoadAllVideoVM(TabId).ToList();
                    grvVideos.DataSource = list;
                    grvVideos.DataBind();

                    if (list.Count > 0)
                    {
                        btnAdd.Visible = false;
                    }
                    else
                    {
                        btnAdd.Visible = true;
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
        //Bind Data to VideoVM
        protected IEnumerable<VideoVM> LoadAllVideoVM(int portalID)
        {
            IEnumerable<Video> listVideo = controller.GetVideos(portalID);
            Collection<VideoVM> listVideoVM = new Collection<VideoVM>();
            try
            {
                foreach (var item in listVideo)
                {
                    VideoVM x = new VideoVM();
                    x.videoID = item.VideoID;
                    x.portalID = item.PortalID;
                    x.tabID = item.TabId;
                    x.title = item.Title;
                    x.description = item.Description;
                    x.userName = UserController.GetUserById(PortalId, item.LastUpdatedBy).DisplayName;
                    x.lastUpdatedDate = item.LastUpdatedDate;

                    listVideoVM.Add(x);
                }
                return listVideoVM.OrderByDescending(s => s.lastUpdatedDate).ToList();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                return null;
            }

          
        }
        #endregion



        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        #region Su kien

        //Tim kiem
        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    var list = LoadAllVideoVM(TabId).Where(x => x.title.ToLower().Contains(txtSearch.Text.ToLower().Trim()) || x.description.ToLower().Contains(txtSearch.Text.ToLower().Trim()) || x.userName.ToLower().Contains(txtSearch.Text.ToLower().Trim())).ToList();
                    grvVideos.DataSource = list;
                    grvVideos.DataBind();
                    if (list.Count > 0)
                    {
                        lblKetQuaSearch.Text = "Tìm được " + list.Count.ToString() + " kết quả";
                    }
                    else
                    {
                        lblKetQuaSearch.Text = "Không tìm thấy kết quả cho: " + txtSearch.Text;
                    }
                    
                }
                else
                {
                    grvVideos.DataSource = LoadAllVideoVM(TabId).ToList();
                    grvVideos.DataBind();                   
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
              
            }
        }
        //Phan Trang tren Giao dien
        protected void grvVideos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grvVideos.PageIndex = e.NewPageIndex;
                grvVideos.DataSource = LoadAllVideoVM(TabId).ToList();
                grvVideos.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
               
            }
        }

        //Them Video
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string miUrl = DotNetNuke.Common.Globals.NavigateURL(base.TabId, "Edit", String.Format("mid={0}", base.ModuleId));
                Response.Redirect(miUrl, true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
               
            }
        }
        protected void Create_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string miUrl = DotNetNuke.Common.Globals.NavigateURL(base.TabId, "Edit", String.Format("mid={0}", base.ModuleId));
                Response.Redirect(miUrl, true);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
               
            }
        }

        //Xoa Video
        protected void Delete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lb = (ImageButton)sender;
                HiddenField id = (HiddenField)lb.FindControl("hdfVideoID");
                HiddenField pageIndex = (HiddenField)lb.FindControl("hdfCurrentPageIndex");
                //Xoa file lien quan
                Video video = controller.GetVideo(Int16.Parse(id.Value), PortalId);
                switch (video.VideosType)
                {
                    case 1:
                        //Xoa Video
                        FileManager.Instance.DeleteFile(FileManager.Instance.GetFile(Int32.Parse(video.Src.Replace("FileID=", "").Trim())));
                        //Xoa hinh anh Video
                        if (!video.ImgVideo.Contains("Portals")) //Anh them tu nguoi dung
                        {
                            FileManager.Instance.DeleteFile(FileManager.Instance.GetFile(Int32.Parse(video.ImgVideo.Replace("FileID=", "").Trim())));
                        }
                        else //Anh tao ngau nhien tu video
                        {
                            try
                            {
                                var pathFile = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + @"/Portals/" + PortalId + @"/";
                                File.Delete(video.ImgVideo.Replace(pathFile, PortalSettings.HomeDirectoryMapPath));
                            }
                            catch
                            {
                                throw;
                            }
                        }
                        break;

                    case 2:
                        if (!video.ImgVideo.Contains("ytimg")) //Anh them tu nguoi dung
                        {
                            FileManager.Instance.DeleteFile(FileManager.Instance.GetFile(Int32.Parse(video.ImgVideo.Replace("FileID=", "").Trim())));
                        }
                        break;

                    case 3:
                        if (!video.ImgVideo.Contains("Portals")) //Anh them tu nguoi dung
                        {
                            FileManager.Instance.DeleteFile(FileManager.Instance.GetFile(Int32.Parse(video.ImgVideo.Replace("FileID=", "").Trim())));
                        }
                        else //Anh tao ngau nhien tu video
                        {
                            try
                            {
                                var pathFile = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + @"/Portals/" + PortalId + @"/";
                                File.Delete(video.ImgVideo.Replace(pathFile, PortalSettings.HomeDirectoryMapPath));
                            }
                            catch
                            {
                                throw;
                            }
                        }
                        break;
                }

                //Xoa tren csdl
                controller.DeleteVideo(Int16.Parse(id.Value.ToString()), PortalId);

                //Load lai danh sach
                grvVideos.DataSource = LoadAllVideoVM(PortalId).ToList();
                grvVideos.PageIndex = Int16.Parse(pageIndex.Value);
                grvVideos.DataBind();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                
            }
        }

        //Sua Video
        protected void Edit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton lb = (ImageButton)sender;
                HiddenField id = (HiddenField)lb.FindControl("hdfVideoID");
                string miUrl = DotNetNuke.Common.Globals.NavigateURL(base.TabId, "Edit", String.Format("mid={0}&VideoID={1}", base.ModuleId, id.Value));
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