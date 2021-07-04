using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Helpers
{
    public class SyncMenager
    {
        private List<int> Subscribed { get; set; }
        private List<int> NotRefreshed { get; set; }

        public SyncMenager()
        {
            Subscribed = new List<int>();
            NotRefreshed = new List<int>();
        }

        public void CallForSync()
        {
            NotRefreshed = new List<int>();
            NotRefreshed.AddRange(Subscribed);
        }

        public int Subscribe()
        {
            int id;
            if (Subscribed.Count > 0)
            {
                id = Subscribed.Max() + 1;
            }
            else
            {
                id = 0;
            }
            Subscribed.Add(id);

            return id;
        }

        public bool IsNotSynced(int id)
        {
            if (NotRefreshed.Contains(id))
            {
                NotRefreshed.Remove(id);
                return true;
            }
            else
                return false;
        }

        public void Unsubscribe(int id)
        {
            Subscribed.Remove(id);
            NotRefreshed.Remove(id);
        }
    }
}