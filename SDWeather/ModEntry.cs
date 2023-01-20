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
            this.Monitor.Log(msg, LogLevel.Debug);
            Game1.addHUDMessage(new HUDMessage(msg, Microsoft.Xna.Framework.Color.OrangeRed, 10000));
        }

        private string PrintWeatherForTomorrow() {
            return $"Weather Tomorrow: {DetermineWeatherMessage(Game1.weatherForTomorrow)}";
        }

        private string PrintLuckForToday() {
            return $"Spirits Today: {DetermineLuckMessage(Game1.player.DailyLuck)}";
        }

        private string DetermineWeatherMessage(int weather = 0) {
            string weatherMsg = "Sunny";

            switch (weather) {
                case 1:
                    weatherMsg = "Rain";
                    break;
                case 2:
                    weatherMsg = "Windy";
                    break;
                case 3:
                    weatherMsg = "Lightning";
                    break;
                case 4:
                    weatherMsg = "Festival";
                    break;
                case 5:
                    weatherMsg = "Snow";
                    break;
                case 6:
                    weatherMsg = "Wedding";
                    break;
                default:
                    break;
            }

            return weatherMsg;
        }

        private string DetermineLuckMessage(double luck = 0) {
            string luckMsg = "";

            if (luck > 0.07) {
                luckMsg = "Very happy";
            }

            if (luck >= 0.02 && luck <= 0.07) {
                luckMsg = "Good humor";
            }

            if (luck != 0 && luck >= -0.02 && luck <= 0.02) {
                luckMsg = "Neutral";
            }

            if (luck == 0) {
                luckMsg = "Absolutely neutral";
            }

            if (luck >= -0.07 && luck < -0.02) {
                string annoyedMsg1 = "Somewhat annoyed";
                string annoyedMsg2 = "Somewhat mildly perturbed";

                Random r = new Random();
                int rNum = r.Next(0, 10);
                luckMsg = rNum < 5 ? annoyedMsg1 : annoyedMsg2;
            }

            if (luck < -0.07) {
                luckMsg = "Very displeased";
            }

            return luckMsg;
        }

    }
}
