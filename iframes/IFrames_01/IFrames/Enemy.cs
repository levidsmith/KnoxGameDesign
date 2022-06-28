using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IFrames {
    class Enemy : GameObject {

        private float vel_x;
        private float fLifeTime;

        public Enemy(string in_strName, Texture2D in_img, GameManager in_gamemanager) : base(in_strName, in_img, in_gamemanager) {
            vel_x = 64 * 2f;

        }

        public override void Update(GameTime gameTime) {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            fLifeTime += deltaTime;

            x += vel_x * deltaTime;
            if (x > 640 + w) {
                vel_x = -Math.Abs(vel_x);
            } else if (x < 0) {
                vel_x = Math.Abs(vel_x);
            }

            y = 200 + (64 * 2f * MathF.Sin(fLifeTime * MathF.PI * 2f));
            
        }

    }
}
