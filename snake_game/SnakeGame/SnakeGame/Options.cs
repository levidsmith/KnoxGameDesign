//2026 - Levi D. Smith <developer@levidsmith.com>
//for KnoxGameDesign www.knoxgamedesign.org
namespace SnakeGame {
    public class Options {

        public enum OptionsState {  GET_PLAYERS, GET_SPEED, GET_SPEED_INCREASE };
        public OptionsState optionsstate;

        public int iPlayers;
        public int iSkillLevel;
        public float fSpeed;
        public bool isSpeedIncreased;

        public Options() {
            optionsstate = OptionsState.GET_PLAYERS;

        }

        public void setSpeed(int iChoice) {
            iSkillLevel = iChoice;
            switch(iChoice) {
                case 1:
                    fSpeed = 0.2f;
                    break;
                case 2:
                    fSpeed = 0.1f;
                    break;
                case 3:
                    fSpeed = 0.05f;
                    break;
            }
        }

    }
}
