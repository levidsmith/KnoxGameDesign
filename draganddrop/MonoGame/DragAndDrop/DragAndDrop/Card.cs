using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace DragAndDrop {
    internal class Card {

        Vector2 position;
        int w, h;
        public Texture2D imgCard;

        public bool isDragged;
        public Vector2 dragOffset;

        public Card() {
            position = new Vector2(0, 0);

            float fScale = 1f / 2.5f;
            w = (int) (250f * fScale);
            h = (int) (350f * fScale);

            isDragged = false;

        }
        public void pointerPressed(Vector2 pointerPosition) {
            checkPressed(pointerPosition);
        }

        public void pointerUp(Vector2 pointerPosition) {
            stopDrag();

        }
        public void pointerDown(Vector2 pointerPosition) {
            if (isDragged) {
                position = pointerPosition + dragOffset;
            }
        }
        private void checkPressed(Vector2 pointerPosition) {
            if (pointerPosition.X >= position.X &&
                pointerPosition.X < position.X + w &&
                pointerPosition.Y >= position.Y &&
                pointerPosition.Y < position.Y + h) {
                startDrag(position - pointerPosition);

            }
        }
        private void startDrag(Vector2 offset) {
            isDragged = true;
            dragOffset = offset;
        }
        public void stopDrag() {
            isDragged = false;
        }
        public void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(imgCard, new Rectangle((int)position.X, (int)position.Y, w, h), Color.White);

        }
    }
}
