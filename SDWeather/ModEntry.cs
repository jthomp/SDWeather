/***************************************************************************************************************************************
 *                                                                                                                                     *
 * SDWeather                                                                                                                           *
 * By: Justin Thompson                                                                                                                 *
 * A tiny mod to show you today's weather and tomorrow's luck when the day starts.                                                     *
 * Copyright (C) Justin Thompson. All Rights Reserved.                                                                                 *
 *                                                                                                                                     *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,                                                 *
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.               *
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,                             *
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,                                                                  *
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                                             * 
 *                                                                                                                                     *
 ***************************************************************************************************************************************
*/

using System;
using StardewModdingAPI;
using StardewValley;

namespace SDWeather {
    class ModEntry : Mod {

        public override void Entry(IModHelper helper) {
            helper.Events.GameLoop.DayStarted += this.DayStarted;
        }

        private void DayStarted(object sender, EventArgs e) {
            string msg = $"{PrintWeatherForTomorrow()}. {PrintLuckForToday()}.";
            this.Monitor.Log(msg, LogLevel.Info);
            Game1.addHUDMessage(new HUDMessage(msg, 10000, true));
        }

        private string PrintWeatherForTomorrow() {
            return $"Weather Tomorrow: {Game1.weatherForTomorrow}";
        }

        private string PrintLuckForToday() {
            return $"Spirits Today: {DetermineLuckMessage(Game1.player.DailyLuck)}";
        }

        private string DetermineLuckMessage(double luck = 0.00) {
            string luckMsg = "";
            string annoyedMsg1 = "Somewhat annoyed";
            string annoyedMsg2 = "Somewhat mildly perturbed";

            switch ( luck ) {
                case double _ when luck > 0.07:
                    luckMsg = "Very happy";
                    break;
                case double _ when luck >= 0.02 && luck <= 0.07:
                    luckMsg = "Good humor";
                    break;
                case double _ when luck != 0 && luck >= -0.02 && luck <= 0.02:
                    luckMsg = "Neutral";
                    break;
                case double _ when luck == 0:
                    luckMsg = "Absolutely neutral";
                    break;
                case double _ when luck >= -0.07 && luck < -0.02:
                    Random r = new Random();
                    int rNum = r.Next(0, 10);
                    luckMsg = rNum < 5 ? annoyedMsg1 : annoyedMsg2;
                    break;
                case double _ when luck < -0.07:
                    luckMsg = "Very displeased";
                    break;
            }

            return luckMsg;
        }

    }
}
