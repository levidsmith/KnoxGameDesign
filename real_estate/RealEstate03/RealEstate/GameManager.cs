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

        public string strMessage = "";

        public enum GameState { StartTurn, LandOnSpace, EndTurn, GameOver };
        public GameState gamestate;

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
                p.iMoney = 1500;
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
            gamestate = GameState.StartTurn;

        }

        public void addProperty(string strName, int iPurchasePrice, int iRent, int iPosition) {
            Property p = new Property();
            p.strName = strName;
            p.iPurchasePrice = iPurchasePrice;
            p.iRent = iRent;
            p.gamemanager = this;
            spaces[iPosition].property = p;

        }

        public void moveSpaces() {
            int iSpaces = dice[0].iRolledValue + dice[1].iRolledValue;
            while (iSpaces > 0) {
                playerCurrent.spaceCurrent = playerCurrent.spaceCurrent.spaceNext;
                iSpaces--;
            }

            gamestate = GameState.LandOnSpace;

            Property property = playerCurrent.spaceCurrent.property;
            if (property != null) {
                Player propertyOwner = property.getPropertyOwner();
                if (propertyOwner != null &&
                    propertyOwner != playerCurrent) {
                    if (playerCurrent.iMoney >= property.iRent) {
                        playerCurrent.iMoney -= property.iRent;
                        propertyOwner.iMoney += property.iRent;
                        strMessage = playerCurrent.strName + " paid $" + property.iRent + " to " + propertyOwner.strName + " at " + property.strName;
                    } else {
                        strMessage = playerCurrent.strName + " unable to pay $" + property.iRent + " at " + property.strName + ".  Eliminated from game.";
                        eliminatePlayer(playerCurrent);

                    }
                }
            }
        }

        public void endTurn() {
            gamestate = GameState.EndTurn;
            playerCurrent = playerCurrent.playerNext;
            gamestate = GameState.StartTurn;
            strMessage = "";
        }

        public void purchaseProperty(Player player, Property property) {
            if (player == null || property == null) {
                return;
            }
            if (player.iMoney >= property.iPurchasePrice &&
                !property.isOwned()) {
                player.iMoney -= property.iPurchasePrice;
                player.properties.Add(property);
                strMessage = player.strName + " purchased " + property.strName;

            }
        }

        public int getPlayerIndex(Player player) {
            int i = 0;
            foreach(Player p1 in players) {
                if (player == p1) {
                    return i;
                }
                i++;
            }

            return -1;
        }

        public void eliminatePlayer(Player player) {
            //set the previous player's next player to this player's next player
            foreach (Player p1 in players) {
                if (p1.playerNext == player) {
                    p1.playerNext = player.playerNext;
                }
            }

            players.Remove(player);

            if (players.Count == 1) {
                gamestate = GameState.GameOver;
            }
        }
    }
}
