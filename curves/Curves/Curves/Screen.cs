//2022 LD Smith - levidsmith.com
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Curves {
    class Screen {
        public const int PIXEL_SCALE = 80;
        protected GameManager gamemanager;


        public Screen(GameManager in_gamemanager) {
            gamemanager = in_gamemanager;
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Begin();
            sb.DrawString(gamemanager.fonts["largefont"], string.Format("{0}px = 1 unit", PIXEL_SCALE), new Vector2(8, 32), Color.Black);

            
            int i;
            for (i = -10; i <= 10; i++) {
                sb.DrawString(gamemanager.fonts["gridfont"], string.Format("{0}", i), scalePoint(i, 0), Color.Red);
            }

            for (i = -10; i <= 10; i++) {
                sb.DrawString(gamemanager.fonts["gridfont"], string.Format("{0}", i), scalePoint(0, i), Color.Green);
            }

            sb.End();
        }

        public virtual void Update(GameTime gameTime) {

        }

        public virtual void doNext() {

        }

        public Vector2 scalePoint(float in_x, float in_y) {
            Vector2 vect = Vector2.Zero;
            float fXOffset = GameManager.SCREEN_WIDTH / 2f;
            float fYOffset = GameManager.SCREEN_HEIGHT / 2f;
            vect = new Vector2((in_x * PIXEL_SCALE) + fXOffset, -1f * (in_y * PIXEL_SCALE) + fYOffset);

            return vect;
        }
    }
}
