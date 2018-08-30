local level = {}

level.name = "Objetos Instanciados"

level.files = {
	{ path = "slime.lua", readOnly = true },
	{ path = "main.lua" },
}

local scale = 50 
level.dimensions = { x = 20 * scale, y = 20 * scale }

level.map = "Sprites/Rooms/Basic Room/map"

level.dialog = {
	{ char = "pepper/normal", contents = "Agora faça a mesma coisa com getters e setters" },
}

level.counter = 0

-- Building level
function level.ready()
	slime = spawn('slime', 5, 11)
	slime2 = spawn('slime', 5, 12)

	level.counter = 0
end

-- Starting level
function level.initialize()
	
	setScript(slime, 'slime.lua')
	setScript(slime2, 'slime.lua')

	new 'main.lua'
end

-- Checking for win
function level.update()

	level.counter = level.counter + 1

	if level.counter <= 1 then
		main(slime, slime2)
	end

	if level.counter < 3 then 
		return
	end


	if slime.getName() == 'Michael' and slime2.getName() == 'Jackson' then
		return 'win'
	else
		return 'lose'
	end

end

return level
