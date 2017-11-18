using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class BackendTournamentFilter
    {
        private TournamentFilterGame gameAll = new TournamentFilterGame() { ID = -1, Name = "Alle" };
        private TournamentFilterEvent eventAll = new TournamentFilterEvent() { ID = -1, Name = "Alle" };
        public TournamentFilterGame GameSelected { get; set; }
        private List<TournamentFilterGame> _gameOptions;
        public List<TournamentFilterGame> GameOptions
        {
            get { return _gameOptions; }
            set
            {
                if (!_gameOptions.Contains(gameAll))
                    _gameOptions.Add(gameAll);
                var union = _gameOptions.Union(value).Distinct();
                _gameOptions = union.ToList();
            }
        }
        public TournamentFilterEvent EventSelected { get; set; }
        private List<TournamentFilterEvent> _eventOptions;
        public List<TournamentFilterEvent> EventOptions
        {
            get { return _eventOptions; }
            set
            {
                if (!_eventOptions.Contains(eventAll))
                    _eventOptions.Add(eventAll);
                var union = _eventOptions.Union(value).Distinct();
                _eventOptions = union.ToList();
            }
        }

        public BackendTournamentFilter()
        {
            GameSelected = gameAll;
            _gameOptions = new List<TournamentFilterGame>();
            EventSelected = eventAll;
            _eventOptions = new List<TournamentFilterEvent>();
        }

        public class TournamentFilterGame
        {
            public Int32 ID { get; set; }
            public String Name { get; set; }
        }

        public class TournamentFilterEvent
        {
            public Int32 ID { get; set; }
            public String Name { get; set; }
        }
    }
}
