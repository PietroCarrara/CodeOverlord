--
-- rogue.lua
-- Copyright (C) 2018 pietro <pietro@the-arch>
--
-- Distributed under terms of the MIT license.
--

local this = {}
this.base = Rogue()

local pastAttacks = {}

function this.update()
	
	moveLeft()

	if (true) then
		return
	end


	if (#monsters() > 0) then
		
		local target = monsters()[0]

		local dist = target.base.Position - this.base.Position

		if (math.abs(dist.X) + math.abs(dist.Y) <= 1) then
			attack(target, dist)
		else
			move()
		end
	end
end

function move()
	if (target.base.Position.X > this.base.Position.X) then
		moveRight()
	elseif (target.base.Position.X < this.base.Position.X) then
		moveLeft()
	elseif (target.base.Position.Y < this.base.Position.Y) then
		moveUp()
	elseif (target.base.Position.Y > this.base.Position.Y) then
		moveDown()
	end
end

function attack(target, dist)

	local attackFuncName = "attack"

	if (not contains(pastAttacks, target)) then
		attackFuncName = "surpriseAttack"
		pastAttacks.insert(target)
	end

	if (dist.X > 0) then
		attackFuncName = attackFuncName .. "Right()"
	elseif (dist.X < 0) then
		attackLeft()
		attackFuncName = attackFuncName .. "Left()"
	elseif (dist.Y > 0) then
		attackFuncName = attackFuncName .. "Down()"
	else
		attackFuncName = attackFuncName .. "Up()"
	end

	local attackFunc = load(attackFuncName)
	attackFunc()
end

function contains(arr, val)
	for _, currVal in ipairs(arr) do
		if val == currVal then
			return true
		end
	end
end

return this
