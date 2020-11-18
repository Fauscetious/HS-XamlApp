using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentProject.Models
{
    //Model was made based on json info provided by destiny.plumbing
    class Manifest
    {
        public class MobileGearAssetDataBases
        {
            public int version { get; set; }
            public string path { get; set; }
        }

        public class Response
        {
            public string version { get; set; }
            public string mobileAssetContentPath { get; set; }
            public List<MobileGearAssetDataBases> mobileGearAssetDataBases { get; set; }
            public Dictionary<string, string> mobileWorldContentPaths { get; set; }
            public string mobileClanBannerDatabasePath { get; set; }
            public Dictionary<string, string> mobileGearCDN { get; set; }
        }

        public class ManifestRootObject { 
    
            public Response Response { get; set; }
            public int ErrorCode { get; set; }
            public int ThrottleSeconds { get; set; }
            public string ErrorStatus { get; set; }
            public string Message { get; set; }
            public Dictionary<string, string> MessageData { get; set; }
        }
    }
}
