using DotNetNuke.ComponentModel.DataAnnotations;
using System;
namespace Modules.Videos.Model
{
    [TableName("Videos")]
    //setup the primary key for table
    [PrimaryKey("VideoID", AutoIncrement = true)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("TabId")]
   
    public class Video
    {
        public int VideoID { get; set; }
        public int PortalID { get; set; }
        public int TabId { get; set; }
        public string Src { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;
        public bool VideosLoop { get; set; }
        public bool AutoStart { get; set; }
        public int VideosType { get; set; }
        public string ImgVideo { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}