local this = Slime()

local bussola = require "bússolaNova"

function this.update()

	local dir = bussola.getDirection()

	if (dir == "up") then
		moveUp()
	elseif (dir == "down") then
		moveDown()
	elseif (dir == "left") then
		moveLeft()
	elseif (dir == "right") then
		moveRight()
	end

end

return this
