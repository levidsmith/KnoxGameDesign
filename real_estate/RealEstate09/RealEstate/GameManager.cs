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


        public Mode modeCurrent;
        public Dictionary<string, Mode> modes;


        public enum GameState { StartTurn, LandOnSpace, EndTurn, GameOver, Mortgage, Incarcerated };
        public GameState gamestate;

        public GameManager() {
            setup();

            modes = new Dictionary<string, Mode>();
            Mode mode;

            mode = new ModeBoard();
            mode.strName = "board";
            mode.gamemanager = this;
            modes.Add(mode.strName, mode);

            mode = new ModeMortgage();
            mode.strName = "mortgage";
            mode.gamemanager = this;
            modes.Add(mode.strName, mode);

            mode = new ModeBuild();
            mode.strName = "build";
            mode.gamemanager = this;
            modes.Add(mode.strName, mode);

            mode = new ModeTrade();
            mode.strName = "trade";
            mode.gamemanager = this;
            modes.Add(mode.strName, mode);

            mode = new ModeAuction();
            mode.strName = "auction";
            mode.gamemanager = this;
            modes.Add(mode.strName, mode);


            modeCurrent = modes["board"];



        }

        private void setup() {
            spaces = new List<Space>();
            int i;
            for (i = 0; i < 40; i++) {
                Space s;
                if (i == 10) {
                    s = new SpaceIncarceration();
                } else if (i == 30) {
                    s = new SpaceArrested();
                } else {
                    s = new Space();
                }
                s.position = new Vector2((i / 10) * 400, (i % 10) * 64 + 64);
                spaces.Add(s);
            }
            for (i = 0; i < spaces.Count - 1; i++) {
                spaces[i].spaceNext = spaces[i + 1];
            }
            spaces[spaces.Count - 1].spaceNext = spaces[0];

            players = new List<Player>();
            int iPlayers = 3;
            for (i = 0; i < iPlayers; i++) {
                Player p = new Player();
                p.strName = "P" + (i + 1);
                p.iMoney = 1500;
                p.spaceCurrent = spaces[0];
                p.gamemanager = this;
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

        public void addPropertyResidential(int iPosition, int iPropertySet, string strName, int iPurchasePrice, int iRent, int iRent1House, int iRent2Houses, int iRent3Houses, int iRent4Houses, int iRent1Hotel, int iHouseCost, int iHotelCost) {
            PropertyResidential p;

            p = new PropertyResidential();

            p.iPropertySet = iPropertySet;
            p.strName = strName;
            p.iPurchasePrice = iPurchasePrice;
            p.iRent = iRent;
            p.iRent1House = iRent1House;
            p.iRent2Houses = iRent2Houses;
            p.iRent3Houses = iRent3Houses;
            p.iRent4Houses = iRent4Houses;
            p.iRent1Hotel = iRent1Hotel;
            p.iHouseCost = iHouseCost;
            p.iHotelCost = iHotelCost;
            p.gamemanager = this;
            spaces[iPosition].property = p;
        }

        public void addPropertyPark(int iPosition, string strName, int iPurchasePrice) {
            PropertyPark p;

            p = new PropertyPark();

            p.strName = strName;
            p.iPurchasePrice = iPurchasePrice;
            p.gamemanager = this;
            spaces[iPosition].property = p;
        }

        public void addPropertyDam(int iPosition, string strName, int iPurchasePrice) {
            PropertyDam p;

            p = new PropertyDam();

            p.strName = strName;
            p.iPurchasePrice = iPurchasePrice;
            p.gamemanager = this;
            spaces[iPosition].property = p;
        }


        public void moveSpaces() {
            int iSpaces = dice[0].iRolledValue + dice[1].iRolledValue;

            gamestate = GameState.LandOnSpace;

            if (dice[0].iRolledValue == dice[1].iRolledValue) {
                playerCurrent.iDoublesCount++;

                if (playerCurrent.iDoublesCount == 3) {
                    incarceratePlayer(playerCurrent);

                    return;
                }
            }


            while (iSpaces > 0) {
                playerCurrent.spaceCurrent = playerCurrent.spaceCurrent.spaceNext;
                iSpaces--;
            }


            Property property = playerCurrent.spaceCurrent.property;
            if (property != null) {


                Player propertyOwner = property.getPropertyOwner();
                if (propertyOwner != null &&
                    propertyOwner != playerCurrent) {

                    if (property.isMortgaged) {
                        strMessage = property.strName + " is mortgaged.  No rent paid";
                    } else {

                        int iCalculatedRent = property.calculateRent();
                        if (playerCurrent.iMoney >= iCalculatedRent) {
                            playerCurrent.iMoney -= iCalculatedRent;
                            propertyOwner.iMoney += iCalculatedRent;
                            strMessage = playerCurrent.strName + " paid $" + iCalculatedRent + " to " + propertyOwner.strName + " at " + property.strName;
                        } else {
                            strMessage = playerCurrent.strName + " unable to pay $" + iCalculatedRent + " at " + property.strName + ".  Eliminated from game.";
                            eliminatePlayer(playerCurrent);

                        }
                    }
                }
            }

            if (playerCurrent.spaceCurrent is SpaceArrested) {
                incarceratePlayer(playerCurrent);
            }
        }

        public void incarceratedRoll() {
            if (dice[0].iRolledValue == dice[1].iRolledValue) {
                getSpaceIncarceration().removeIncarceratedPlayer(playerCurrent);
            } else {
                getSpaceIncarceration().incrementIncarceratedRolls(playerCurrent);
                if (getSpaceIncarceration().getIncarceratedRolls(playerCurrent) == 3) {
                    playerCurrent.iMoney -= 50;
                    getSpaceIncarceration().removeIncarceratedPlayer(playerCurrent);
                }
            }
            gamestate = GameState.LandOnSpace;

        }


        public void incarceratedPay() {
            if (playerCurrent.iMoney >= 50) {
                playerCurrent.iMoney -= 50;
                getSpaceIncarceration().removeIncarceratedPlayer(playerCurrent);
                gamestate = GameState.LandOnSpace;
            }


        }

        public void incarceratedUseCard() {
            if (playerCurrent.iGetOutFreeCards > 0) {
                playerCurrent.iGetOutFreeCards--;
                getSpaceIncarceration().removeIncarceratedPlayer(playerCurrent);
                gamestate = GameState.LandOnSpace;
            }
        }



        public void endTurn() {
            gamestate = GameState.EndTurn;


            if (checkRollAgain()) {
                gamestate = GameState.StartTurn;
            } else {
                playerCurrent = playerCurrent.playerNext;
                if (!isPlayerIncarcerated(playerCurrent)) {
                    gamestate = GameState.StartTurn;
                } else {
                    gamestate = GameState.Incarcerated;
                }
            }
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

        public void auctionProperty(Property property) {
            if (property == null) {
                return;
            }

        }

        public int getPlayerIndex(Player player) {
            int i = 0;
            foreach (Player p1 in players) {
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

        public bool playerOwnsPropertySet(Player playerCompare, PropertyResidential propertyresidentialCompare) {
            foreach (Space s in spaces) {
                if (s.property != null && s.property is PropertyResidential) {
                    PropertyResidential propertySpace = (PropertyResidential)s.property;
                    if (propertySpace.iPropertySet == propertyresidentialCompare.iPropertySet &&
                        propertySpace.getPropertyOwner() != playerCompare) {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool isPlayerIncarcerated(Player player) {
            bool isIncarcerated = false;
            SpaceIncarceration spaceincarceration = getSpaceIncarceration();
            if (spaceincarceration != null) {
                isIncarcerated = spaceincarceration.isPlayerIncarcerated(player);
            }

            return isIncarcerated;

        }

        public SpaceIncarceration getSpaceIncarceration() {
            SpaceIncarceration spaceincarceration = null;
            foreach (Space space in spaces) {
                if (space is SpaceIncarceration) {
                    spaceincarceration = (SpaceIncarceration)space;
                    break;
                }
            }

            return spaceincarceration;

        }

        private void incarceratePlayer(Player player) {
            SpaceIncarceration spaceincarceration = getSpaceIncarceration();
            player.spaceCurrent = spaceincarceration;
            spaceincarceration.addIncarceratedPlayer(player);
            playerCurrent.iDoublesCount = 0;

            gamestate = GameState.LandOnSpace;

            /*
            playerCurrent = playerCurrent.playerNext;
            gamestate = GameState.StartTurn;
            */

        }

        private bool checkRollAgain() {
            if (dice[0].iRolledValue == dice[1].iRolledValue && !isPlayerIncarcerated(playerCurrent)) {
                return true;

            } else {
                return false;
            }

        }
    }

}
