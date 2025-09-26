using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    public class BulletExploder : Bullet {

        float x_orig;
        float y_orig;
        float fSpeed;

        float fCountdown;
        const float WAIT_COUNTDOWN_MAX = 2f;
        const float EXPLODE_COUNTDOWN_MAX = 0.5f;

        enum State { moving, wait, explode };
        State state;

        public BulletExploder(int init_x, int init_y) : base(init_x, init_x) {

            x = init_x;
            y = init_y;
            x_orig = init_x;
            y_orig = init_y;

            w = 24;
            h = 24;

            isAlive = true;
            fLifetime = 0f;

            fSpeed = Game1.BLOCK_SIZE * 16;
        }

        public override void Update(float deltaTime, Game1 game) {
            if (!isAlive) {
                return;
            }

            if (state == State.moving) {
                Block b = checkBlockCollision(game.listBlocks, 0, 0);
                if (b != null) {
                    fCountdown = WAIT_COUNTDOWN_MAX;
                    state = State.wait;
                    return;
                }
                
                x = x_orig + (fLifetime * fSpeed * vel_x);

                Enemy e = checkEnemyCollision(game.listEnemies);
                if (e != null) {
                    e.setDamage(1);
                    isAlive = false;
                }

            } else if (state == State.wait) {
                fCountdown -= deltaTime;
                if (fCountdown <= 0f) {
                    fCountdown = EXPLODE_COUNTDOWN_MAX;
                    w = Game1.BLOCK_SIZE * 2;
                    h = Game1.BLOCK_SIZE * 2;
                    x -= w / 2;
                    y -= h / 2;
                    state = State.explode;
                    return;
                }

            } else if (state == State.explode) {
                fCountdown -= deltaTime;
                if (fCountdown <= 0f) {
                    destroy();
                }

                Enemy e = checkEnemyCollision(game.listEnemies);
                if (e != null) {
                    e.setDamage(1);
                }

                List<Block> blocks = getCollidedBlocks(game.listBlocks, 0, 0);
                foreach(Block b in blocks) {
                    game.listBlocks.Remove(b);
                }
                



            }


            fLifetime += deltaTime;
        }

        public override void Draw(SpriteBatch sb, Dictionary<string, Texture2D> textures) {
            if (!isAlive) {
                return;
            }

            Color c = Color.Red;
            switch (state) {
                case State.moving:
                    c = Color.White;
                    break;
                case State.wait:
                    c = Color.Red;
                    break;
                case State.explode:
                    c = Color.Red;
                    break;
            }

            sb.Draw(textures["bullet"], new Rectangle((int)x, (int)Game1.SCREEN_HEIGHT - (int)y - h, w, h), new Rectangle(0, 0, 8, 8), c);


        }



    }

}
