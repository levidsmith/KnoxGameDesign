using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;

namespace PelletEatingDemo {
    internal class Player : Actor {
        public Player() {
            w = 32;
            h = 32;
            x = (Game1.SCREEN_WIDTH - w) / 2;
            y = (Game1.SCREEN_HEIGHT - h) / 2;
            fSpeed = 128f;
            direction = Direction.UP;
        }

        public override void move(float deltaTime) {
            if (direction == Direction.UP) {
                y -= fSpeed * deltaTime;
            } else if (direction == Direction.DOWN) {
                y += fSpeed * deltaTime;
            } else if (direction == Direction.LEFT) {
                x -= fSpeed * deltaTime;
            } else if (direction == Direction.RIGHT) {
                x += fSpeed * deltaTime;
            }
        }

        public void inputUp() {
            direction = Direction.UP;
        }

        public void inputDown() {
            direction = Direction.DOWN;
        }

        public void inputLeft() {
            direction = Direction.LEFT;
        }
        public void inputRight() {
            direction = Direction.RIGHT;
        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            float fRotation;
            if (direction == Direction.RIGHT) {
                fRotation = 0f;
            } else if (direction == Direction.DOWN) {
                fRotation = 0.5f * (float) Math.PI;
            } else if (direction == Direction.LEFT) {
                fRotation = 1.0f * (float)Math.PI;
            } else if (direction == Direction.UP) {
                fRotation = 1.5f * (float)Math.PI;
            } else {
                fRotation = 0f;
            }
                sb.Draw(textures["player"], new Rectangle((int)x, (int)y, (int)w, (int)h), new Rectangle(0, 0, 32, 32), Color.White, fRotation, new Vector2(16, 16), SpriteEffects.None, 0f);

        }

    }
}
