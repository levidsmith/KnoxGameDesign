using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RealEstate.Display {
    public class DisplayPlayerToken {
        public Player player;
        public int iMoveID;
        public Vector2 position;
        
        public int iTokenID;
        public Color colorToken;
        public Vector2 targetPosition;
        public float fSpeed;

        public DisplayPlayerToken() {
            iMoveID = 0;
            fSpeed = 8f;
            
        }

        public void Update(GameTime gameTime, DisplayModeBoard displaymodeboard) {
            /*
            if (iMoveID < player.spacesMoved.Count) {
                targetPosition = displaymodeboard.getSpacePosition(player.spacesMoved[iMoveID]);
            }

            if (position.X < targetPosition.X) {
                position.X += fSpeed;
            } else if (position.X > targetPosition.X) {
                position.X -= fSpeed;
            }

            if (position.Y < targetPosition.Y) {
                position.Y += fSpeed;
            } else if (position.Y > targetPosition.Y) {
                position.Y -= fSpeed;
            }
            */

            targetPosition = displaymodeboard.getSpacePosition(player.spaceCurrent);

            if (Vector2.Distance(position, targetPosition) <= fSpeed) {
                position = targetPosition;

            } else {
                position = Vector2.Lerp(position, targetPosition, fSpeed / Vector2.Distance(position, targetPosition));

                /*
                if (position.X < targetPosition.X) {
                    position.X += fSpeed;
                } else if (position.X > targetPosition.X) {
                    position.X -= fSpeed;
                }

                if (position.Y < targetPosition.Y) {
                    position.Y += fSpeed;
                } else if (position.Y > targetPosition.Y) {
                    position.Y -= fSpeed;
                }
                */
            }



        }

        public Texture2D getSpriteToken(DisplayManager displaymanager) {
            Texture2D sprToken = null;
            switch (iTokenID) {
                case 0:
                    sprToken = displaymanager.sprites["sprToken01"];
                    break;
                case 1:
                    sprToken = displaymanager.sprites["sprToken02"];
                    break;
                case 2:
                    sprToken = displaymanager.sprites["sprToken03"];
                    break;
                case 3:
                    sprToken = displaymanager.sprites["sprToken04"];
                    break;
                case 4:
                    sprToken = displaymanager.sprites["sprToken05"];
                    break;
                case 5:
                    sprToken = displaymanager.sprites["sprToken06"];
                    break;
            }
            return sprToken;



        }

        public Color getTokenColor() {
            return Player.colors[iTokenID];
        }


    }
}
