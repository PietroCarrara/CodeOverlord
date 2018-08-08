local level = {}

level.name = "Métodos"

level.dimensions = { x = 1300, y = 1300 }

level.map = "Sprites/Rooms/Basic Room/map"

level.dialog = {
	{ char = "pepper/studying", contents = "..." },
}

level.files = {
	{ path = "main.lua" },
	{ path = "slime.lua", readOnly = true },
}

function level.ready()

	slime1 = spawn('slime', 6, 6)

	setScript(slime1, 'Content/Scripts/Levels/methods/slime.lua')

end

function level.initialize()
	
	script = require "main"

end

function level.update()
	m = monsters()

	if (#m >= 1) then
		print(m[1].getName("aals", "bbs"))
	end
end

return level

