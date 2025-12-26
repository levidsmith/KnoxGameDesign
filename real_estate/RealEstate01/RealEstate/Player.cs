using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RealEstate {
    public class Player {
        public string strName;
        int iMoney;
        public Space spaceCurrent;
        public Player playerNext;

        public static Color[] colors = { Color.LightGreen, Color.CornflowerBlue, Color.Red, Color.MediumPurple, Color.Gold, Color.Cyan };

        public int rollDice(List<Die> dice) {
            Random rand = new Random();
            return rand.Next() % 12;

        }

    }
}
