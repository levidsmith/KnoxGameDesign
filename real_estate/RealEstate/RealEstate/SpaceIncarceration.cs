using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class SpaceIncarceration : Space {
        public string strName;
        public List<Player> playersIncarcerated;
        public Dictionary<Player, int> incarceratedRollCount;

        public SpaceIncarceration() {
            strName = "Brushy Mountain";
            playersIncarcerated = new List<Player>();
            incarceratedRollCount = new Dictionary<Player, int>();

        }

        public void addIncarceratedPlayer(Player player) {
            playersIncarcerated.Add(player);
            incarceratedRollCount[player] = 0;
        }

        public void removeIncarceratedPlayer(Player player) {
            playersIncarcerated.Remove(player);
        }

        public bool isPlayerIncarcerated(Player player) {
            if (playersIncarcerated.Contains(player)) {
                return true;
            } else {
                return false;
            }
        }

        public void incrementIncarceratedRolls(Player player) {
            incarceratedRollCount[player]++;

        }

        public int getIncarceratedRolls(Player player) {
            return incarceratedRollCount[player];
        }
    }
}
