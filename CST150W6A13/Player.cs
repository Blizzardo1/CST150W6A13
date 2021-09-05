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
        private int _id;

        public string PlayerName => _name;

        public bool IsActive => Active;

        public Marker PlayerMarker => pMarker;

        public int PlayerId => _id;

        public void SetActive()
        {
            Active = true;
        }

        public void SetInactive()
        {
            Active = false;
        }

        public Player(string Name, Marker marker, int id)
        {
            _id = id;
            _name = Name;
            pMarker = marker;
        }
    }

}
