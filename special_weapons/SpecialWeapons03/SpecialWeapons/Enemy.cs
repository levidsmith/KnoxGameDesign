using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace SpecialWeapons {
    public class Enemy {
        public float x, y;
        public int w, h;
        float vel_y;
        
        int iSpriteIndex;
        float fSpriteFrameTime;
        float fSpriteFrameTimeMax;

        float fMoveCountdown;
        float fMoveCountdownMax;

        float fFreezeCountdown;

        int iHealth;
        bool isAlive;

        public Enemy(int init_x, int init_y, int init_w, int init_h) {
            x = init_x;
            y = init_y;

            w = init_w;
            h = init_h;

            fSpriteFrameTime = 0f;
            fSpriteFrameTimeMax = 0.2f;
            iSpriteIndex = 0;

            fMoveCountdown = 0f;
            fMoveCountdownMax = 2f;

            vel_y = 2f * Game1.BLOCK_SIZE;

            iHealth = 1;
            isAlive = true;
            fFreezeCountdown = 0f;
        }

        public void Update(float deltaTime, Game1 game) {

            if (!isAlive) {
                return;
            }

            if (getIsFrozen()) {
                fFreezeCountdown -= deltaTime;
                return;
            }


            fSpriteFrameTime += deltaTime;
            if (fSpriteFrameTime > fSpriteFrameTimeMax) {
                iSpriteIndex += 1;
                if (iSpriteIndex > 1) {
                    iSpriteIndex = 0;
                }
                fSpriteFrameTime = 0f;
            }

            fMoveCountdown += deltaTime;
            if (fMoveCountdown > fMoveCountdownMax) {
                vel_y *= -1;
                fMoveCountdown = 0f;
            }

            y += vel_y * deltaTime;

        }

        public void setDamage(int iDamage) {
            iHealth -= iDamage;
            if (iHealth <= 0) {
                isAlive = false;

            }
        }

        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {

            if (!isAlive) {
                return;
            }

            Color c = new Color(255, 128, 128);
            if (getIsFrozen()) {
                c = Color.LightBlue;
            }

                sb.Draw(textures["enemy"], new Rectangle((int)x, Game1.SCREEN_HEIGHT - (int)y - (int)h, (int)w, (int)h), new Rectangle(0 + (16 * iSpriteIndex), 0, 16, 16), c);

        }

        public bool getIsFrozen() {
            if (fFreezeCountdown > 0f) {
                return true;
            } else {
                return false;
            }
        }

        public void startFreeze() {
            fFreezeCountdown = 5f;
        }

    }
}
