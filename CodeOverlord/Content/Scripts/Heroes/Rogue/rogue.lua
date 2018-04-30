--
-- rogue.lua
-- Copyright (C) 2018 pietro <pietro@the-arch>
--
-- Distributed under terms of the MIT license.
--

local this = {}
this = Rogue()

local pastAttacks = {}

function this.update()

	if (#monsters() > 0) then

		local target = monsters()[1]

		local dist = this.getPosition() - target.getPosition()

		if (math.abs(dist.X) + math.abs(dist.Y) <= 1) then
			attack(target, dist)
		else
			move(target)
		end
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

function attack(target, dist)

	local attackFuncName = "attack"

	if (not contains(pastAttacks, target)) then
		attackFuncName = "surpriseAttack"
		table.insert(pastAttacks, target)
	end

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

function contains(arr, val)
	for _, currVal in ipairs(arr) do
		if val == currVal then
			return true
		end
	end
end

return this
