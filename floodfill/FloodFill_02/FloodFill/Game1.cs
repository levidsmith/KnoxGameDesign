using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace FloodFill {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int SCREEN_WIDTH = 1280;
        const int SCREEN_HEIGHT = 720;
        const int CELL_SIZE = 16;
        int[,] iCells;
        int iTotalRows;
        int iTotalCols;
        List<Color> colors;
        int iSelectedColor;

        Texture2D imgCell;

        KeyboardState keyboardStatePrevious;
        MouseState mouseStatePrevious;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            iTotalRows = SCREEN_HEIGHT / CELL_SIZE;
            iTotalCols = SCREEN_WIDTH / CELL_SIZE;
            iCells = new int[iTotalRows, iTotalCols];

            colors = new List<Color>();
            colors.Add(new Color(255, 255, 255));
            colors.Add(new Color(237, 10, 63));
            colors.Add(new Color(255, 117, 56));
            colors.Add(new Color(252, 232, 131));
            colors.Add(new Color(28, 172, 120));
            colors.Add(new Color(0, 102, 255));
            colors.Add(new Color(146, 110, 174));
            colors.Add(new Color(180, 103, 77));
            colors.Add(new Color(0, 0, 0));

            iSelectedColor = 4;

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            imgCell = Content.Load<Texture2D>("cell");
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            handleInput();

            base.Update(gameTime);
        }

        private void handleInput() {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();


            //change color
            if (keyboardState.IsKeyDown(Keys.D0) && !keyboardStatePrevious.IsKeyDown(Keys.D0)) {
                iSelectedColor = 0;
            }
            if (keyboardState.IsKeyDown(Keys.D1) && !keyboardStatePrevious.IsKeyDown(Keys.D1)) {
                iSelectedColor = 1;
            }
            if (keyboardState.IsKeyDown(Keys.D2) && !keyboardStatePrevious.IsKeyDown(Keys.D2)) {
                iSelectedColor = 2;
            }
            if (keyboardState.IsKeyDown(Keys.D3) && !keyboardStatePrevious.IsKeyDown(Keys.D3)) {
                iSelectedColor = 3;
            }
            if (keyboardState.IsKeyDown(Keys.D4) && !keyboardStatePrevious.IsKeyDown(Keys.D4)) {
                iSelectedColor = 4;
            }
            if (keyboardState.IsKeyDown(Keys.D5) && !keyboardStatePrevious.IsKeyDown(Keys.D5)) {
                iSelectedColor = 5;
            }
            if (keyboardState.IsKeyDown(Keys.D6) && !keyboardStatePrevious.IsKeyDown(Keys.D6)) {
                iSelectedColor = 6;
            }
            if (keyboardState.IsKeyDown(Keys.D7) && !keyboardStatePrevious.IsKeyDown(Keys.D7)) {
                iSelectedColor = 7;
            }
            if (keyboardState.IsKeyDown(Keys.D8) && !keyboardStatePrevious.IsKeyDown(Keys.D8)) {
                iSelectedColor = 8;
            }
            if (keyboardState.IsKeyDown(Keys.C) && !keyboardStatePrevious.IsKeyDown(Keys.C)) {
                clearCells();
            }



            //fill cell
            if (mouseState.LeftButton == ButtonState.Pressed) {
                fillCell(mouseState.Y / CELL_SIZE, mouseState.X / CELL_SIZE);
            }

            //flood fill 
            if (mouseState.RightButton == ButtonState.Pressed && mouseStatePrevious.RightButton == ButtonState.Released) {
                int iSelectedRow = mouseState.Y / CELL_SIZE;
                int iSelectedCol = mouseState.X / CELL_SIZE;

                if (iSelectedRow >= 0 && iSelectedRow < iTotalRows && iSelectedCol >= 0 && iSelectedCol < iTotalCols) {
                    int iReplaceColor = iCells[iSelectedRow, iSelectedCol];

                    if (iSelectedColor != iReplaceColor) {
                        floodFill(iSelectedRow, iSelectedCol, iReplaceColor);
                    }
                }
            }


            keyboardStatePrevious = keyboardState;
            mouseStatePrevious = mouseState;
        }

        private void fillCell(int iRow, int iCol) {
            if (iRow >= 0 && iRow < iTotalRows && iCol >= 0 && iCol < iTotalCols) {
                iCells[iRow, iCol] = iSelectedColor;
            }

        }

        private void clearCells() {
            int iRow, iCol;
            for (iRow = 0; iRow < iTotalRows; iRow++) {
                for (iCol = 0; iCol < iTotalCols; iCol++) {
                    iCells[iRow, iCol] = 0;
                }
            }

        }


        private void floodFill(int iRow, int iCol, int iReplaceColor) {
            if (iRow >= 0 && iRow < iTotalRows && iCol >= 0 && iCol < iTotalCols) {
                if (iCells[iRow, iCol] == iReplaceColor) {
                    iCells[iRow, iCol] = iSelectedColor;
                    floodFill(iRow, iCol + 1, iReplaceColor);
                    floodFill(iRow, iCol - 1, iReplaceColor);
                    floodFill(iRow + 1, iCol, iReplaceColor);
                    floodFill(iRow - 1, iCol, iReplaceColor);
                }

            }

        }


        protected override void Draw(GameTime gameTime) {
            Color c;
            int h;

            //GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            base.Draw(gameTime);
            int iRow, iCol;
            for (iRow = 0; iRow < iTotalRows; iRow++) {
                for (iCol = 0; iCol < iTotalCols; iCol++) {
                    _spriteBatch.Draw(imgCell, new Rectangle(iCol * CELL_SIZE, iRow * CELL_SIZE, CELL_SIZE, CELL_SIZE),
                        colors[iCells[iRow, iCol]]);
                }
            }

            _spriteBatch.End();

        }

        private Color getColor(float hue, float sat, float val) {
            Color resultColor = Color.White;

            float r, g, b;

            float c, x, m;

            hue = hue % 360;

            c = val * sat;
            x = c *
                (1 - 
                Math.Abs(  ((hue / 60f)  % 2) - 1)
                );
            m = val - c;

            if (hue >= 0 && hue < 60f) {
                r = c;
                g = x;
                b = 0;
            } else if (hue >= 60 && hue < 120f) {
                r = x;
                g = c;
                b = 0;
            } else if (hue >= 120 && hue < 180f) {
                r = 0;
                g = c;
                b = x;
            } else if (hue >= 180 && hue < 240f) {
                r = 0;
                g = x;
                b = c;
            } else if (hue >= 240 && hue < 300f) {
                r = x;
                g = 0;
                b = c;
            } else if (hue >= 300 && hue < 360f) {
                r = c;
                g = 0;
                b = x;
            } else {
                r = 0;
                g = 0;
                b = 0;
            }

            resultColor = new Color((int) ((r + m) * 255), 
                                    (int) ((g + m) * 255), 
                                    (int) ((b + m) * 255)
                                    );
                return resultColor;
        }
    }
}
