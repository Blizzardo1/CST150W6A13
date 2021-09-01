using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CST150W6A13
{
    public class Player
    {
        private string _name;
        private bool Active;
        private Marker pMarker;

        public string PlayerName => _name;

        public bool IsActive => Active;

        public Marker PlayerMarker => pMarker;

        public void SetActive()
        {
            Active = true;
        }

        public void SetInactive()
        {
            Active = false;
        }

        public Player(string Name, Marker marker)
        {
            _name = Name;
            pMarker = marker;
        }
    }

}
