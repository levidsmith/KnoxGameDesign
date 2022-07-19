//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Curves {
    class ScreenBezier : Screen {

        float fXCurrent;
        float fYCurrent;
        float fTime;
        List<Vector2> points;

        int iCurrentFunction = 0;

        public ScreenBezier(GameManager in_gamemanager) : base(in_gamemanager) {
            setupPoints();

        }

        private void setupPoints() {
            points = new List<Vector2>();
            switch(iCurrentFunction) {
                case 0:
                    points.Add(new Vector2(0f, 0f));
                    points.Add(new Vector2(2f, 4f));
                    break;
                case 1:
                    points.Add(new Vector2(0f, 0f));
                    points.Add(new Vector2(2f, 8f));
                    points.Add(new Vector2(4f, 0f));
                    break;
                case 2:
                    points.Add(new Vector2(0f, 0f));
                    points.Add(new Vector2(2.66f, 12f));
                    points.Add(new Vector2(5.33f, -12f));
                    points.Add(new Vector2(8f, 0f));
                    break;
            }

        }

        public override void Update(GameTime gameTime) {
            fTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fTime > 1f) {
                fTime = 0f;
            }

            switch(iCurrentFunction) {
                case 0:
                    fXCurrent = ((1f - fTime) * points[0].X) + (fTime * points[1].X);
                    fYCurrent = ((1f - fTime) * points[0].Y) + (fTime * points[1].Y);
                    break;
                case 1:
                    fXCurrent = (MathF.Pow((1f - fTime), 2) * points[0].X) +
                                (2f * (1f - fTime) * fTime * points[1].X) +
                                (MathF.Pow(fTime, 2) * points[2].X);
                    fYCurrent = (MathF.Pow((1f - fTime), 2) * points[0].Y) +
                                (2f * (1f - fTime) * fTime * points[1].Y) +
                                (MathF.Pow(fTime, 2) * points[2].Y);
                    break;
                case 2:
                    fXCurrent = (MathF.Pow((1f - fTime), 3) * points[0].X) +
                                (3f * MathF.Pow((1f - fTime), 2) * fTime * points[1].X) +
                                (3f * (1f - fTime) * MathF.Pow(fTime, 2) * points[2].X) +
                                (MathF.Pow(fTime, 3) * points[3].X);
                    fYCurrent = (MathF.Pow((1f - fTime), 3) * points[0].Y) +
                                (3f * MathF.Pow((1f - fTime), 2) * fTime * points[1].Y) +
                                (3f * (1f - fTime) * MathF.Pow(fTime, 2) * points[2].Y) +
                                (MathF.Pow(fTime, 3) * points[3].Y);
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
                    strFunction = "two control points";
                    break;
                case 1:
                    strFunction = "three control points";
                    break;
                case 2:
                    strFunction = "four control points";
                    break;
            }
            sb.DrawString(gamemanager.fonts["largefont"], "Function: " + strFunction, new Vector2(8, 32 * 2), Color.Black);


            sb.DrawString(gamemanager.fonts["largefont"], string.Format("time {0:0.00}", fTime), new Vector2(8, 32 * 3), Color.Black);

            int i;
            for (i = 0; i < points.Count; i++) {
                sb.DrawString(gamemanager.fonts["largefont"], string.Format("P[" + i + "]: {0}, {1}", points[i].X, points[i].Y), new Vector2(8, 32 * (4 + i)), Color.Black);
            }

            sb.DrawString(gamemanager.fonts["largefont"], "Bezier Curve", new Vector2(8, 32 * 0), Color.Red);

            sb.Draw(gamemanager.sprites["player"], scalePoint(fXCurrent, fYCurrent) - new Vector2(fSpriteOffset, fSpriteOffset), Color.White);
            sb.End();
        }

        public override void doNext() {
            iCurrentFunction++;
            if (iCurrentFunction > 2) {
                iCurrentFunction = 0;
            }

            setupPoints();

            fTime = 0f;
        }
    }
}
