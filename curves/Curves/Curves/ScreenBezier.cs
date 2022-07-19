//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Curves {
    class ScreenBezier : Screen {

        float fXMin = -4;
        float fXMax = 4;
        float fXCurrent;
        float fYCurrent;
        float fSpeed;

        int iCurrentFunction = 0;

        public ScreenBezier(GameManager in_gamemanager) : base(in_gamemanager) {
            fXCurrent = fXMin;
            fSpeed = 4f;
            

        }

        public override void Update(GameTime gameTime) {
            fXCurrent += fSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (fXCurrent > fXMax) {
                fXCurrent = fXMin;
                
            }

            switch(iCurrentFunction) {
                case 0:
                    fYCurrent = 0;
                    break;
                case 1:
                    fYCurrent = MathF.Pow(fXCurrent, 2f);
                    break;
                case 2:
                    fYCurrent = -MathF.Pow(fXCurrent, 2f);
                    break;
                case 3:
                    fYCurrent = -MathF.Pow(fXCurrent, 2f) + 4f; 
                    break;
                case 4:
                    fYCurrent = -MathF.Pow((fXCurrent - 2f), 2f) + 4f; ;
                    break;
            }

        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);


            float fSpriteOffset = 8f;
            sb.Begin();

            string strFunction = "";
            switch(iCurrentFunction) {
                case 0:
                    strFunction = "y = 0";
                    break;
                case 1:
                    strFunction = "y = x^2";
                    break;
                case 2:
                    strFunction = "y = -(x^2)";
                    break;
                case 3:
                    strFunction = "y = -(x^2) + 4";
                    break;
                case 4:
                    strFunction = "y = -((x - 2)^2) + 4";
                    break;
            }
            sb.DrawString(gamemanager.fonts["largefont"], "Function: " + strFunction, new Vector2(8, 32 * 2), Color.Black);

            sb.DrawString(gamemanager.fonts["largefont"], string.Format("x min = {0}, x max = {1}", fXMin, fXMax), new Vector2(8, 32 *3), Color.Black);
            sb.DrawString(gamemanager.fonts["largefont"], string.Format("speed {0} units/s", fSpeed), new Vector2(8, 32 * 4), Color.Black);

            sb.DrawString(gamemanager.fonts["largefont"], "Bezier Curve", new Vector2(8, 32 * 0), Color.Red);

            sb.Draw(gamemanager.sprites["player"], scalePoint(fXCurrent, fYCurrent) - new Vector2(fSpriteOffset, fSpriteOffset), Color.White);
            sb.End();
        }

        public override void doNext() {
            iCurrentFunction++;
            if (iCurrentFunction > 4) {
                iCurrentFunction = 0;
            }
        }
    }
}
