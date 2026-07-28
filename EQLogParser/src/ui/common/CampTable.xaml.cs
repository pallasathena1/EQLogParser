using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace EQLogParser
{
    public partial class CampTable : UserControl
    {
        public CampTable()
        {
            InitializeComponent();
        }

        internal void SetFights(IEnumerable<Fight> fights)
        {
            var sessions = CampSessionManager.BuildSessions(
              fights.OrderBy(fight => fight.BeginTime));

            dataGrid.ItemsSource = sessions;
        }
    }
}