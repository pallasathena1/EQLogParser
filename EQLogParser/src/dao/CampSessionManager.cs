using System;
using System.Collections.Generic;
using System.Linq;

namespace EQLogParser
{
    internal sealed class CampSessionManager
    {
        public static CampSessionManager Instance { get; } =
          new CampSessionManager();

        public event EventHandler<CampSession> EventsNewCamp;
        public event EventHandler<CampSession> EventsUpdateCamp;
        public event EventHandler EventsCleared;

        private readonly List<CampSession> Sessions =
          new List<CampSession>();

        private CampSessionManager()
        {
            DataManager.Instance.EventsNewFight += HandleNewFight;
            DataManager.Instance.EventsUpdateFight += HandleUpdateFight;
            DataManager.Instance.EventsClearedActiveData += HandleCleared;
        }

        internal IReadOnlyList<CampSession> GetSessions() => Sessions;

        private void HandleNewFight(object sender, Fight fight)
        {
            AddFight(fight);
        }

        private void HandleUpdateFight(object sender, Fight fight)
        {
            CampSession session = Sessions
              .FirstOrDefault(item => item.Fights.Contains(fight));

            if (session == null)
            {
                return;
            }

            session.EndTime = Math.Max(session.EndTime, fight.LastTime);
            EventsUpdateCamp?.Invoke(this, session);
        }

        private void HandleCleared(object sender, bool cleared)
        {
            Sessions.Clear();
            EventsCleared?.Invoke(this, EventArgs.Empty);
        }

        private void AddFight(Fight fight)
        {
            if (fight == null || fight.IsInactivity)
            {
                return;
            }

            CampSession current =
              Sessions.Count > 0 ? Sessions[^1] : null;

            bool startNewSession =
              current == null ||
              ContextChanged(current, fight) ||
              fight.BeginTime - current.EndTime >= FightTable.GroupTimeout;

            if (startNewSession)
            {
                current = CreateSession(fight);
                Sessions.Add(current);

                EventsNewCamp?.Invoke(this, current);
            }
            else
            {
                current.Fights.Add(fight);
                current.EndTime = Math.Max(current.EndTime, fight.LastTime);

                EventsUpdateCamp?.Invoke(this, current);
            }
        }

        internal static List<CampSession> BuildSessions(
          IEnumerable<Fight> fights,
          double inactivityTimeoutSeconds = FightTable.GroupTimeout)
        {
            var sessions = new List<CampSession>();
            CampSession current = null;

            foreach (Fight fight in fights)
            {
                if (fight == null || fight.IsInactivity)
                {
                    continue;
                }

                bool startNewSession =
                  current == null ||
                  ContextChanged(current, fight) ||
                  fight.BeginTime - current.EndTime >= inactivityTimeoutSeconds;

                if (startNewSession)
                {
                    current = CreateSession(fight);
                    sessions.Add(current);
                }
                else
                {
                    current.Fights.Add(fight);
                    current.EndTime = Math.Max(current.EndTime, fight.LastTime);
                }
            }

            return sessions;
        }

        private static CampSession CreateSession(Fight fight)
        {
            var session = new CampSession
            {
                Zone = fight.Zone,
                ZoneShortName = fight.ZoneShortName,
                InstanceType = fight.InstanceType,
                Difficulty = fight.Difficulty,
                DifficultyName = fight.DifficultyName,
                PlayerLevel = fight.PlayerLevel,
                BeginTime = fight.BeginTime,
                EndTime = fight.LastTime
            };

            session.Fights.Add(fight);

            return session;
        }

        private static bool ContextChanged(
          CampSession session,
          Fight fight)
        {
            return
              !string.Equals(
                session.Zone,
                fight.Zone,
                StringComparison.OrdinalIgnoreCase) ||
              !string.Equals(
                session.ZoneShortName,
                fight.ZoneShortName,
                StringComparison.OrdinalIgnoreCase) ||
              !string.Equals(
                session.InstanceType,
                fight.InstanceType,
                StringComparison.OrdinalIgnoreCase) ||
              session.Difficulty != fight.Difficulty ||
              session.PlayerLevel != fight.PlayerLevel;
        }
    }
}