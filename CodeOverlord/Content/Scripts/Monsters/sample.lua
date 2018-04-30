-- Dumb slime

local this = Slime()

function this.update()

	if (#heroes() > 0) then
		
		local target = heroes()[1]

		local dist = this.getPosition() - target.getPosition()

		if (math.abs(dist.X) + math.abs(dist.Y) <= 1) then
			attack(dist)
		else
			move(target)
		end
	else
		passTurn()
	end
end

function move(target)
	if (target.getPosition().X > this.getPosition().X) then
		moveRight()
	elseif (target.getPosition().X < this.getPosition().X) then
		moveLeft()
	elseif (target.getPosition().Y < this.getPosition().Y) then
		moveUp()
	elseif (target.getPosition().Y > this.getPosition().Y) then
		moveDown()
	end
end

function attack(dist)

	local attackFuncName = "attack"

	if (dist.X > 0) then
		attackFuncName = attackFuncName .. "Right"
	elseif (dist.X < 0) then
		attackFuncName = attackFuncName .. "Left"
	elseif (dist.Y < 0) then
		attackFuncName = attackFuncName .. "Down"
	else
		attackFuncName = attackFuncName .. "Up"
	end

	_G[attackFuncName]();
end

return this
