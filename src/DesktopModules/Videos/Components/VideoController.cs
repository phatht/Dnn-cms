using DotNetNuke.Data;
using DotNetNuke.Services.FileSystem;
using Modules.Videos.Model;
using Modules.Videos.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Web;

namespace Modules.Videos.Components
{
    public class VideoController
    {
        #region Khai Bao    
        
        #endregion

        #region Phuong Thuc    

        public void CreateVideo(Video t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Video>();
                rep.Insert(t);
            }
        }

        public void DeleteVideo(int VideoId, int PortalId)
        {
            var t = GetVideo(VideoId, PortalId);
            DeleteVideo(t);
        }

        public void DeleteVideo(Video t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Video>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Video> GetVideos(int PortalId)
        {
            IEnumerable<Video> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Video>();
                t = rep.Get(PortalId);
            }
            return t;
        }
       
        public Video GetVideo(int VideoId, int PortalId)
        {
            Video t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Video>();
                t = rep.GetById(VideoId, PortalId);
            }
            return t;
        }

        internal VideoVM GetVideo(int videoID, object portaID)
        {
            throw new NotImplementedException();
        }

        public void UpdateVideo(Video t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Video>();
                rep.Update(t);
            }
        }


        //Excute File

        public string GetFileUrlById(string nameFile)
        {
            try
            {
                //Get ID cua File tu FileID duoc luu o csdl (VD: FileID=121)
                int idSrc = Int16.Parse(nameFile.Replace("FileID=", "").Trim());
                IFileInfo oFile = FileManager.Instance.GetFile(idSrc);
                FolderMappingInfo mapFolder = FolderMappingController.Instance.GetFolderMapping(oFile.FolderMappingID);
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + FolderProvider.Instance(mapFolder.FolderProviderType).GetFileUrl(oFile);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        public string GetFileUrlByName(string nameFile, int portalID)
        {
            string pathFile = @"/Portals/" + portalID + @"/" + nameFile;
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + pathFile;
        }
    }
}