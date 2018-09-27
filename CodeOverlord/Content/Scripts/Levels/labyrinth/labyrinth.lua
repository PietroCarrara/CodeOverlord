local level = {}

level.name = "Labirinto"

level.files = {
	{ path = "bússola.lua", readOnly = true },
	{ path = "bússolaNova.lua" },
	{ path = "slime.lua", readOnly = true },
}

level.map = "Sprites/Rooms/labyrinth"

level.dialog = {
	{ char = "pepper/studying", contents = "Poxa vida... Se eu não acabar isso antes de o professor voltar eu vou estar ferrada..." },
}

-- Building level
function level.ready()
	slime = spawn('slime', 5, 11)

end

-- Starting level
function level.initialize()
	
	setScript(slime,'slime.lua')

end

-- Checking for win
function level.update()

	local pos = slime.getPosition()

	if (pos.X == 10 and pos.Y == 3) then
		return "win"
	end

end

return level
