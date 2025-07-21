using System;
using System.Reflection.Metadata;

namespace Client
{

    public class Weather
    {

        #region Functions

        public static void ProcessWeather()
        {
            int i;
            int x;

            if (GameState.CurrentWeather > 0 & GameState.CurrentWeather < (int)Core.WeatherType.Fog)
            {
                if (GameState.CurrentWeather == (int)Core.WeatherType.Rain | GameState.CurrentWeather == (int)Core.WeatherType.Storm)
                {
                    Sound.PlayWeatherSound("Rain.ogg", true);
                }

                x = GameLogic.Rand(1, Core.Constant.MAX_WEATHER_PARTICLES - GameState.CurrentWeatherIntensity);
                if (x == 1)
                {
                    // Add a new particle
                    for (i = 0; i < Core.Constant.MAX_WEATHER_PARTICLES; i++)
                    {
                        if (GameState.WeatherParticle[i].InUse == 0)
                        {
                            if (GameLogic.Rand(1, 3) == 1)
                            {
                                GameState.WeatherParticle[i].InUse = 1;
                                GameState.WeatherParticle[i].Type = GameState.CurrentWeather;
                                GameState.WeatherParticle[i].Velocity = GameLogic.Rand(8, 14);
                                GameState.WeatherParticle[i].X = (int)Math.Round(GameState.TileView.Left * 32d - 32d);
                                GameState.WeatherParticle[i].Y = (int)Math.Round(GameState.TileView.Top * 32d + GameLogic.Rand(-32));
                            }
                            else
                            {
                                GameState.WeatherParticle[i].InUse = 1;
                                GameState.WeatherParticle[i].Type = GameState.CurrentWeather;
                                GameState.WeatherParticle[i].Velocity = GameLogic.Rand(10, 15);
                                GameState.WeatherParticle[i].X = (int)Math.Round(GameState.TileView.Left * 32d + GameLogic.Rand(-32, GameState.ResolutionWidth));
                                GameState.WeatherParticle[i].Y = (int)Math.Round(GameState.TileView.Top * 32d - 32d);
                            }
                        }
                    }
                }
            }
            else
            {
                Sound.StopWeatherSound();
            }

            if (GameState.CurrentWeather == (int)Core.WeatherType.Storm)
            {
                x = GameLogic.Rand(1, 400 - GameState.CurrentWeatherIntensity);
                if (x == 1)
                {
                    GameState.DrawThunder = GameLogic.Rand(15, 22);
                    Sound.PlayExtraSound("Thunder.ogg");
                }
            }

            for (i = 0; i < Core.Constant.MAX_WEATHER_PARTICLES; i++)
            {
                if (GameState.WeatherParticle[i].InUse == 1)
                {
                    if (GameState.WeatherParticle[i].X > GameState.TileView.Right * 32d | GameState.WeatherParticle[i].Y > GameState.TileView.Bottom * 32d)
                    {
                        GameState.WeatherParticle[i].InUse = 0;
                    }
                    else
                    {
                        GameState.WeatherParticle[i].X = GameState.WeatherParticle[i].X + GameState.WeatherParticle[i].Velocity;
                        GameState.WeatherParticle[i].Y = GameState.WeatherParticle[i].Y + GameState.WeatherParticle[i].Velocity;
                    }
                }
            }

        }

        #endregion

    }
}