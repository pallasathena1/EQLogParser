using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace EQLogParser
{
    public partial class CampTable : UserControl
    {
        private const string MinimumDurationSetting =
            "CampMinimumDurationSeconds";

        private readonly ObservableCollection<CampSession> Camps =
            new ObservableCollection<CampSession>();

        private bool NeedRefresh;
        private bool IsInitializing = true;
        private double MinimumDurationSeconds;

        private readonly DispatcherTimer UpdateTimer;

        public CampTable()
        {
            InitializeComponent();

            dataGrid.ItemsSource = Camps;

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

            CampSessionManager.Instance.EventsNewCamp += EventsNewCamp;
            CampSessionManager.Instance.EventsUpdateCamp += EventsUpdateCamp;
            CampSessionManager.Instance.EventsCleared += EventsCleared;

            // Include sessions created before this control loaded.
            foreach (CampSession camp in
                     CampSessionManager.Instance.GetSessions())
            {
                Camps.Add(camp);
            }

            LoadMinimumDurationSetting();
            ApplyDurationFilter();

            IsInitializing = false;
        }

        private void LoadMinimumDurationSetting()
        {
            string setting = ConfigUtil.GetSetting(
                MinimumDurationSetting,
                "0");

            if (!double.TryParse(
                    setting,
                    out MinimumDurationSeconds))
            {
                MinimumDurationSeconds = 0;
            }

            minimumDurationBox.SelectedIndex =
                MinimumDurationSeconds switch
                {
                    300 => 1,
                    900 => 2,
                    _ => 0
                };
        }

        private void MinimumDurationChanged(
     object sender,
     SelectionChangedEventArgs e)
        {
            // SelectionChanged can fire while InitializeComponent is still
            // constructing the controls.
            if (IsInitializing || dataGrid == null)
            {
                return;
            }

            if (sender is not ComboBox comboBox ||
                comboBox.SelectedItem is not ComboBoxItem selectedItem ||
                !double.TryParse(
                    selectedItem.Tag?.ToString(),
                    out double seconds))
            {
                return;
            }

            MinimumDurationSeconds = seconds;

            ConfigUtil.SetSetting(
                MinimumDurationSetting,
                MinimumDurationSeconds.ToString());

            ApplyDurationFilter();
        }

        private void ApplyDurationFilter()
        {
            if (dataGrid?.View == null)
            {
                return;
            }

            dataGrid.View.Filter = item =>
            {
                return item is CampSession camp &&
                       camp.DurationSeconds >=
                       MinimumDurationSeconds;
            };

            dataGrid.View.Refresh();
        }

        private void EventsNewCamp(
            object sender,
            CampSession camp)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Camps.Add(camp);
                NeedRefresh = true;
            }));
        }

        private void EventsUpdateCamp(
            object sender,
            CampSession camp)
        {
            NeedRefresh = true;
        }

        private void EventsCleared(
            object sender,
            EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Camps.Clear();
                NeedRefresh = false;
            }));
        }
    }
}