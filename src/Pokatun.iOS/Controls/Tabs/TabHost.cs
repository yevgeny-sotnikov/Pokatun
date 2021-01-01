using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Foundation;
using UIKit;

namespace Pokatun.iOS.Controls.Tabs
{
    [DesignTimeVisible(true)]
    [Register(nameof(TabHost))]
    public sealed class TabHost : UIView
    {
        private IList<HotelProfileTab> _tabs;
        private HotelProfileTab _selectedTab;
        private IDictionary<nint, UIView> _tabContents;

        public TabHost(IntPtr handle) : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _tabs = FindTabs();
            _tabContents = new Dictionary<nint, UIView>(_tabs.Count);

            _selectedTab = _tabs.FirstOrDefault(t => t.Selected);

            if (_selectedTab == null)
            {
                _selectedTab = _tabs.First();
                _selectedTab.Selected = true;
            }    

            foreach (HotelProfileTab tab in _tabs)
            {
                UIView view = ViewWithTag(tab.TabContentViewTag);
                _tabContents.Add(tab.TabContentViewTag, view);

                view.Hidden = tab != _selectedTab;
            }

            foreach (HotelProfileTab tab in _tabs)
            {
                tab.Tapped += TabTapped;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (HotelProfileTab tab in _tabs)
                {
                    tab.Tapped -= TabTapped;
                }
            }

            base.Dispose(disposing);
        }

        public void TabTapped(object sender, EventArgs e)
        {
            HotelProfileTab tab = (HotelProfileTab)sender;

            if (tab.Selected)
            {
                return;
            }

            _selectedTab.Selected = false;
            tab.Selected = true;

            _tabContents[_selectedTab.TabContentViewTag].Hidden = true;
            _tabContents[tab.TabContentViewTag].Hidden = false;

            _selectedTab = tab;
        }

        private IList<HotelProfileTab> FindTabs(UIView view = null)
        {
            List<HotelProfileTab> tabs = new List<HotelProfileTab>();

            view = view ?? this;

            foreach (UIView subview in view.Subviews)
            {
                HotelProfileTab tab = subview as HotelProfileTab;

                if (tab != null)
                {
                    tabs.Add(tab);
                }
                else
                {
                    tabs.AddRange(FindTabs(subview));
                }
            }

            return tabs;
        }
    }
}
