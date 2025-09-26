using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialWeapons {
    internal class LevelReader {

        public void readLevel(List<Block> listBlocks, List<Enemy> listEnemies) {
            using (Stream stream = TitleContainer.OpenStream("level.txt")) {
                using (StreamReader reader = new StreamReader(stream)) {
                    string strLine;
                    int iRow;
                    int iCol;

                    int iEnemyCount = 0;

                    iRow = 0;
                    while ((strLine = reader.ReadLine()) != null) {
                        iCol = 0;
                        foreach (char c in strLine) {
                            if (c == 'X') {
                                Block block = new Block(iCol * Game1.BLOCK_SIZE, ((Game1.SCREEN_HEIGHT / Game1.BLOCK_SIZE) - iRow) * Game1.BLOCK_SIZE, Game1.BLOCK_SIZE, Game1.BLOCK_SIZE);
                                listBlocks.Add(block);
                            } else if (c == 'E') {
                                Enemy enemy = new Enemy(iCol * Game1.BLOCK_SIZE, ((Game1.SCREEN_HEIGHT / Game1.BLOCK_SIZE) - iRow) * Game1.BLOCK_SIZE, Game1.BLOCK_SIZE, Game1.BLOCK_SIZE);
                                enemy.setEnemyType(iEnemyCount % 3);
                                listEnemies.Add(enemy);
                                iEnemyCount++;
                            }
                            iCol++;
                            }
                        iRow++;
                    }
                }

            }

        }
    }
}
