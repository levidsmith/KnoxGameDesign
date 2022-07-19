//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Curves {
    class ScreenPhysics : Screen {

        float fXMin = 0;
        float fXCurrent;
        float fYCurrent;
        float fYCurrentConv;
        
        float fGravity = -9.8f;
        float fInitVel = 19.6f;
        float fVelocity;
        float fLifetime = 0f;
        float fConv = 0.204082f;
        float deltaTime;

        int iCurrentFunction = 0;

        public ScreenPhysics(GameManager in_gamemanager) : base(in_gamemanager) {
            fXCurrent = fXMin;
            
            

        }

        public override void Update(GameTime gameTime) {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fLifetime += deltaTime;

            fXCurrent = fLifetime;
            switch (iCurrentFunction) {
                case 0:
                    fYCurrent = (fInitVel + (0.5f * fLifetime * fGravity)) * fLifetime;
                    break;
                case 1:
                    if (deltaTime > 0) {
                        fYCurrent = fYCurrent + (deltaTime * fVelocity) + (0.5f * fGravity * MathF.Pow(deltaTime, 2));
                        fVelocity = fVelocity + (deltaTime * fGravity);
                    }
                    

                    break;
            }

            fYCurrentConv = fYCurrent * fConv;

            if (fLifetime > 4f) {
                fLifetime = 0f;

                if (iCurrentFunction == 1) {
                    fYCurrent = 0f;
                    fVelocity = fInitVel;
                }
            }


        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);


            float fSpriteOffset = 8f;
            sb.Begin();

            string strFunction = "";
            switch(iCurrentFunction) {
                case 0:
                    strFunction = "Continuous";
                    break;
                case 1:
                    strFunction = "Discrete";
                    break;
            }
            sb.DrawString(gamemanager.fonts["largefont"], "Function: " + strFunction, new Vector2(8, 32 * 2), Color.Black);

            sb.DrawString(gamemanager.fonts["largefont"], string.Format("gravity = {0:0.0}, yConversionFactor = {1:0.000}", fGravity, fConv), new Vector2(8, 32 * 3), Color.Black);

            if (iCurrentFunction == 0) {
                sb.DrawString(gamemanager.fonts["largefont"], string.Format("lifetime = {0:0.0}, y = {1:0.0}, yConv = {2:0.0}", fLifetime, fYCurrent, fYCurrentConv), new Vector2(8, 32 * 4), Color.Black);
            } else if (iCurrentFunction == 1) {
                sb.DrawString(gamemanager.fonts["largefont"], string.Format("velocity = {0:0.0}, deltaTime = {1:0.0000}, y = {2:0.0}, yConv = {3:0.0}", fVelocity, deltaTime, fYCurrent, fYCurrentConv), new Vector2(8, 32 * 4), Color.Black);
            }


            sb.DrawString(gamemanager.fonts["largefont"], "Physics", new Vector2(8, 32 * 0), Color.Red);

            sb.Draw(gamemanager.sprites["player"], scalePoint(fXCurrent, fYCurrentConv) - new Vector2(fSpriteOffset, fSpriteOffset), Color.White);
            sb.End();
        }

        public override void doNext() {
            iCurrentFunction++;
            if (iCurrentFunction >=2) {
                iCurrentFunction = 0;
            }

            fLifetime = 0f;

            switch(iCurrentFunction) {
                case 1:
                    fYCurrent = 0f;
                    fVelocity = fInitVel;
                    break;
            }
        }
    }
}
