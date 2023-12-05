function AdjustMusicVolume(initialVolume)
    if initialVolume >= 60 then
        return initialVolume - 60
    else
        return 0
    end
end