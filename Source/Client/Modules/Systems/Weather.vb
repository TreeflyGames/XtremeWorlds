Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Core

Module Weather

    Friend Const MaxWeatherParticles As Integer = 100
    Friend WeatherParticle(MaxWeatherParticles) As WeatherParticleRec

    Public Structure WeatherParticleRec
        Dim Type As Integer
        Dim X As Integer
        Dim Y As Integer
        Dim Velocity As Integer
        Dim InUse As Integer
    End Structure

    Friend FogOffsetX As Integer
    Friend FogOffsetY As Integer

    Friend CurrentWeather As Integer
    Friend CurrentWeatherIntensity As Integer
    Friend CurrentFog As Integer
    Friend CurrentFogSpeed As Integer
    Friend CurrentFogOpacity As Integer
    Friend CurrentTintR As Integer
    Friend CurrentTintG As Integer
    Friend CurrentTintB As Integer
    Friend CurrentTintA As Integer
    Friend DrawThunder As Integer

#Region "Functions"

    Sub ProcessWeather()
        Dim i As Integer, x As Integer

        If CurrentWeather > 0 And CurrentWeather < [Enum].Weather.Fog Then
            If CurrentWeather = [Enum].Weather.Rain Or CurrentWeather = [Enum].Weather.Storm Then
                PlayWeatherSound("Rain.ogg", True)
            End If

            x = Rand(1, 101 - CurrentWeatherIntensity)
            If x = 1 Then
                'Add a new particle
                For i = 0 To MaxWeatherParticles
                    If WeatherParticle(i).InUse = 0 Then
                        If Rand(1, 3) = 1 Then
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).Type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(8, 14)
                            WeatherParticle(i).X = (TileView.Left * 32) - 32
                            WeatherParticle(i).Y = ((TileView.Top * 32) + Rand(-32, ))
                        Else
                            WeatherParticle(i).InUse = 1
                            WeatherParticle(i).Type = CurrentWeather
                            WeatherParticle(i).Velocity = Rand(10, 15)
                            WeatherParticle(i).X = ((TileView.Left * 32) + Rand(-32, ResolutionWidth))
                            WeatherParticle(i).Y = (TileView.Top * 32) - 32
                        End If
                    End If
                Next
            End If
        Else
            StopWeatherSound()
        End If
        If CurrentWeather = [Enum].Weather.Storm Then
            x = Rand(1, 400 - CurrentWeatherIntensity)
            If x = 1 Then
                'Draw Thunder
                DrawThunder = Rand(15, 22)
                PlayExtraSound("Thunder.ogg")
            End If
        End If
        For i = 0 To MaxWeatherParticles
            If WeatherParticle(i).InUse = 1 Then
                If WeatherParticle(i).X > TileView.Right * 32 Or WeatherParticle(i).Y > TileView.Bottom * 32 Then
                    WeatherParticle(i).InUse = 0
                Else
                    WeatherParticle(i).X = WeatherParticle(i).X + WeatherParticle(i).Velocity
                    WeatherParticle(i).Y = WeatherParticle(i).Y + WeatherParticle(i).Velocity
                End If
            End If
        Next

    End Sub

#End Region

End Module