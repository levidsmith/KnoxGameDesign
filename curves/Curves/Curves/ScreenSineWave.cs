//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Curves {
    class ScreenSineWave : Screen {

        float fXMin = -8;
        float fXMax = 8;
        float fXCurrent;
        float fYCurrent;
        float fSpeed;

        int iCurrentFunction = 0;

        public ScreenSineWave(GameManager in_gamemanager) : base(in_gamemanager) {
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
                    fYCurrent = MathF.Sin(fXCurrent);
                    break;
                case 1:
                    fYCurrent = 4f * MathF.Sin(fXCurrent);
                    break;
                case 2:
                    fYCurrent = MathF.Sin(MathF.PI * fXCurrent);
                    break;
                case 3:
                    fYCurrent = 4f * MathF.Sin((MathF.PI / 4f) * fXCurrent);
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
                    strFunction = "y = sin(x)";
                    break;
                case 1:
                    strFunction = "y = 4 * sin(x)";
                    break;
                case 2:
                    strFunction = "y = sin(PI * x)";
                    break;
                case 3:
                    strFunction = "y = 4 * sin((PI / 4) * x)";
                    break;
            }
            sb.DrawString(gamemanager.fonts["largefont"], "Function: " + strFunction, new Vector2(8, 32 * 2), Color.Black);

            sb.DrawString(gamemanager.fonts["largefont"], string.Format("x min = {0}, x max = {1}", fXMin, fXMax), new Vector2(8, 32 *3), Color.Black);
            sb.DrawString(gamemanager.fonts["largefont"], string.Format("speed {0} units/s", fSpeed), new Vector2(8, 32 * 4), Color.Black);

            sb.DrawString(gamemanager.fonts["largefont"], "Sine Wave", new Vector2(8, 32 * 0), Color.Red);


            sb.Draw(gamemanager.sprites["player"], scalePoint(fXCurrent, fYCurrent) - new Vector2(fSpriteOffset, fSpriteOffset), Color.White);
            sb.End();
        }

        public override void doNext() {
            iCurrentFunction++;
            if (iCurrentFunction > 3) {
                iCurrentFunction = 0;
            }
        }
    }
}
