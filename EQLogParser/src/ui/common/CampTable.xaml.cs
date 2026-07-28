using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EQLogParser
{
    public partial class CampTable : UserControl
    {
        private readonly ObservableCollection<CampSession> Camps =
          new ObservableCollection<CampSession>();
        private bool NeedRefresh;
        private readonly DispatcherTimer UpdateTimer;
        public CampTable()
        {
            InitializeComponent();
            UpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1250)
            };

            UpdateTimer.Tick += (sender, e) =>
            {
                if (!NeedRefresh)
                {
                    return;
                }

                dataGrid.View?.Refresh();
                NeedRefresh = false;
            };

            UpdateTimer.Start();
            dataGrid.ItemsSource = Camps;

            CampSessionManager.Instance.EventsNewCamp += EventsNewCamp;
            CampSessionManager.Instance.EventsUpdateCamp += EventsUpdateCamp;
            CampSessionManager.Instance.EventsCleared += EventsCleared;

            // Include sessions that may have been created before this control loaded.
            foreach (CampSession camp in CampSessionManager.Instance.GetSessions())
            {
                Camps.Add(camp);
            }
        }

        private void EventsNewCamp(object sender, CampSession camp)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Camps.Add(camp);
            }));
        }

        private void EventsUpdateCamp(object sender, CampSession camp)
        {
            NeedRefresh = true;
        }

        private void EventsCleared(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Camps.Clear();
                NeedRefresh = false;
            }));
        }
    }
}