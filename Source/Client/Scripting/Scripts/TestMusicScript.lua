-- Adjusts the music volume with configurable parameters and robust handling.
-- @param initialVolume (number): The starting volume level (e.g., 0 to 100).
-- @param reduction (number, optional): The amount to subtract from the initial volume (default: 60).
-- @param minVolume (number, optional): The minimum allowed volume (default: 0).
-- @param maxVolume (number, optional): The maximum allowed volume (default: 100).
-- @param scaleFactor (number, optional): An optional multiplier to scale the adjusted volume (default: 1).
-- @return (number): The adjusted volume, clamped between minVolume and maxVolume.
function AdjustMusicVolume(initialVolume, reduction, minVolume, maxVolume, scaleFactor)
    -- Set default values for optional parameters
    reduction = reduction or 60
    minVolume = minVolume or 0
    maxVolume = maxVolume or 100
    scaleFactor = scaleFactor or 1

    -- Validate and sanitize initialVolume
    if type(initialVolume) ~= "number" or initialVolume ~= initialVolume then -- Check for NaN
        initialVolume = 0
    end
    if initialVolume < minVolume then
        initialVolume = minVolume
    end

    -- Calculate the adjusted volume based on the original logic
    local adjusted
    if initialVolume >= reduction then
        adjusted = initialVolume - reduction
    else
        adjusted = minVolume
    end

    -- Apply optional scaling for smoother or amplified adjustments
    adjusted = adjusted * scaleFactor

    -- Clamp the result between minVolume and maxVolume
    return math.min(math.max(adjusted, minVolume), maxVolume)
end
