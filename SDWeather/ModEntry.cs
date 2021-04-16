using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace SDWeather {
    class ModEntry : Mod {

        public override void Entry(IModHelper helper) {
            helper.Events.GameLoop.DayStarted += this.DayStarted;
        }

        private void DayStarted(object sender, EventArgs e) {
            string msg = $"{PrintWeatherForTomorrow()}. {PrintLuck()}";
            this.Monitor.Log(msg, LogLevel.Debug);
            Game1.showGlobalMessage(msg);
        }

        private string PrintWeatherForTomorrow() {
           return $"Tomorrow's Weather: {DetermineWeatherMessage(Game1.weatherForTomorrow)}";
        }

        private string PrintLuck() {
            return $"Today's Luck: {DetermineLuckMessage(Game1.player.DailyLuck)}";
        }

        private string DetermineWeatherMessage(int weather) {
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

        private string DetermineLuckMessage(double luck) {
            string luckMsg = "";

            if (luck > 0.07) {
                luckMsg = "Very happy.";
            }

            if (luck >= 0.02 && luck <= 0.07) {
                luckMsg = "Good humor.";
            }

            if (luck != 0 && luck >= -0.02 && luck <= 0.02) {
                luckMsg = "Neutral.";
            }

            if (luck == 0) {
                luckMsg = "Absolutely neutral.";
            }

            if (luck >= -0.07 && luck < -0.02) {
                string annoyedMsg1 = "Somewhat annoyed.";
                string annoyedMsg2 = "Somewhat mildly perturbed.";

                Random r = new Random();
                int rNum = r.Next(0, 10);
                luckMsg = rNum < 5 ? annoyedMsg1 : annoyedMsg2;
            }

            if (luck < -0.07) {
                luckMsg = "Very displeased.";
            }

            return luckMsg;
        }

    }
}
