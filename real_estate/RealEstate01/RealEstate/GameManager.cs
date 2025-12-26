using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class GameManager {
        public List<Space> spaces;
        public List<Player> players;
        public List<Die> dice;
        public Dictionary<int, string> propertyNameMap;

        public Player playerCurrent;

        public GameManager() {
            setup();
        }

        private void setup() {
            spaces = new List<Space>();
            int i;
            for (i = 0; i < 40; i++) {
                Space s = new Space();
                s.position = new Vector2((i / 10) * 400, (i % 10) * 64);
                spaces.Add(s);
            }
            for (i = 0; i < spaces.Count - 1; i++) {
                spaces[i].spaceNext = spaces[i + 1];
            }
            spaces[spaces.Count - 1].spaceNext = spaces[0];

            players = new List<Player>();
            for (i = 0; i < 6; i++) {
                Player p = new Player();
                p.strName = "P" + (i + 1);
                p.spaceCurrent = spaces[0];
                players.Add(p);
            }
            for (i = 0; i < players.Count - 1; i++) {
                players[i].playerNext = players[i + 1];
            }
            players[players.Count - 1].playerNext = players[0];



            dice = new List<Die>();
            for (i = 0; i < 2; i++) {
                dice.Add(new Die());
            }

            playerCurrent = players[0];

        }

        public void addProperty(string strName, int iPosition) {
            Property p = new Property();
            p.strName = strName;
            spaces[iPosition].property = p;

        }

        public void moveSpaces() {
            int iSpaces = dice[0].iRolledValue + dice[1].iRolledValue;
            while (iSpaces > 0) {
                playerCurrent.spaceCurrent = playerCurrent.spaceCurrent.spaceNext;
                iSpaces--;
            }
        }

        public void endTurn() {
            playerCurrent = playerCurrent.playerNext;
        }
    }
}
