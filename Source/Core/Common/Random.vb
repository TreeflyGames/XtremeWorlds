Public Class Random
    Public Flicker As System.Random = New System.Random()

    Public Function NextDouble(minValue As Double, maxValue As Double) As Double
        If minValue > maxValue Then
            Throw New ArgumentException("minValue must be less than or equal to maxValue.")
        End If

        ' Generate a random double within the given range
        Dim randomValue As Double = Flicker.NextDouble()
        Return minValue + (randomValue * (maxValue - minValue))
    End Function

End Class
