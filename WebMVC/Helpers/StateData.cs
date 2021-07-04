using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Helpers
{
    public static class StateData
    {
        public static SyncMenager InitSyncMenager { get; set; }
        public static SyncMenager BMSyncMenager { get; set; }


        public static void Initialize()
        {
            InitSyncMenager = new SyncMenager();
            BMSyncMenager = new SyncMenager();
        }
    }
}