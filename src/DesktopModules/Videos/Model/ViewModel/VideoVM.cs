using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modules.Videos.Model.ViewModel
{
    public class VideoVM
    {
        public int videoID { get; set; }
        public int portalID { get; set; }
        public int tabID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public string userName { get; set; }

        public string  src { get; set; }
        public int videosType { get; set; }
        public int MyProperty { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public string imageVideo { get; set; }
    }
}