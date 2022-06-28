//using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace IFrames {
	public class GameObject {

		public float x;
		public float y;
		public int w;
		public int h;

		protected Texture2D img;
		public string strName;

		public GameManager gamemanager;

		public const int UNIT_SIZE = 64;

		public GameObject(string in_strName, Texture2D in_img, GameManager in_gamemanager) {
			strName = in_strName;
			img = in_img;
			gamemanager = in_gamemanager;
			w = 64;
			h = 64;
		}

		public void setPosition(int in_x, int in_y) {
			x = in_x;
			y = in_y;
		}

		public virtual void Update(GameTime gameTime) {
		}

		public virtual void Draw(SpriteBatch sb) {
			sb.Draw(img, new Rectangle((int)x, (int)y, w, h), Color.White);

		}

		public bool hasCollided(GameObject other) {
			bool hasCollided = true;
			if (x + w < other.x ||
				x > other.x + other.w ||
				y + h < other.y ||
				y > other.y + other.h) {
				hasCollided = false;
			}

			return hasCollided;
		}

	}

}