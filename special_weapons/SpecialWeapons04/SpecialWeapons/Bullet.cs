using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class Bullet {
        protected float x, y;
        public float vel_x, vel_y;
        protected int w, h;

        protected float fLifetime;
        protected float fLifetimeMax;
        protected bool isAlive;

        public Bullet(int init_x, int init_y) {
            x = init_x;
            y = init_y;
            w = 24;
            h = 24;

            isAlive = true;

            fLifetime = 0f;
            fLifetimeMax = 1f;
        }

        public void setVelocity(float param_x, float param_y) {
            vel_x = param_x * 16f * Game1.BLOCK_SIZE;
            vel_y = param_y * 16f * Game1.BLOCK_SIZE;
        }


        public virtual void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }

            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

            Enemy e = checkEnemyCollision(game.listEnemies);
            if (e != null) {
                e.setDamage(1);
                
            }

            fLifetime += deltaTime;
            if (fLifetime > fLifetimeMax) {
                isAlive = false;
            }
        }


        public void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            if (!isAlive) {
                return;
            }
            
            sb.Draw(textures["bullet"], new Rectangle((int)x, (int)Game1.SCREEN_HEIGHT - (int)y - h, w, h), new Rectangle(0, 0, 8, 8), Color.White);
            

        }

        protected Enemy checkEnemyCollision(List<Enemy> listEnemies) {
            foreach (Enemy e in listEnemies) {
                if (collided(e, (int)x, (int)y)) {
                    return e;
                }

            }
            return null;
        }


        protected bool collided(Enemy e, int x1, int y1) {
            if (x1 + w <= e.x ||
                x1 >= e.x + e.w ||
                y1 + h <= e.y ||
                y1 >= e.y + e.h) {
                return false;
            } else {
                return true;
            }

        }


    }
}
