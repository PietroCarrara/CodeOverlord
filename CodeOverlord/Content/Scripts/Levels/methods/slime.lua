local this = Slime()

local names = {"João", "Joestar", "Gustavão", "Graffiti"}

this.name = names[math.random(#names)]

this.getName = function(a, b)
	return b
end

function this.update()
	passTurn()
end

return this
