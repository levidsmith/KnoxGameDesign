using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class Bullet {
        float x, y;
        float vel_x, vel_y;
        int w, h;

        float fLifetime;
        float fLifetimeMax;
        bool isAlive;

        public Bullet(int init_x, int init_y, int init_direction) {
            x = init_x;
            y = init_y;
            w = 24;
            h = 24;

            vel_x = init_direction * 16f * Game1.BLOCK_SIZE;
            isAlive = true;

            fLifetime = 0f;
            fLifetimeMax = 1f;
        }


        public void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }

            x += vel_x * deltaTime;

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

        private Enemy checkEnemyCollision(List<Enemy> listEnemies) {
            foreach (Enemy e in listEnemies) {
                if (collided(e, (int)x, (int)y)) {
                    return e;
                }

            }
            return null;
        }


        private bool collided(Enemy e, int x1, int y1) {
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
