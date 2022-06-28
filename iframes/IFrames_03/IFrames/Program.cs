﻿using System;

namespace IFrames {
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new GameManager())
                game.Run();
        }
    }
}
