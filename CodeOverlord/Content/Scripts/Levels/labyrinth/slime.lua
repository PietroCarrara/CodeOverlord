local this = Slime()

local bussola = new "bússolaNova.lua"

function this.update()

	local dir = bussola.getDirecao()
	local posicao = this.getPosition()

	posicao = direcaoToPonto(posicao, dir)

	if gridIsAvailable(posicao.X, posicao.Y) then
		if (dir == "up") then
			moveUp()
		elseif (dir == "down") then
			moveDown()
		elseif (dir == "left") then
			moveLeft()
		elseif (dir == "right") then
			moveRight()
		end
	else
		bussola.renovarCaminho()
	end
end

function direcaoToPonto(ponto, dir)
	if (dir == "up") then
		return { X = ponto.X, Y = ponto.Y - 1 }
	elseif (dir == "down") then
		return { X = ponto.X, Y = ponto.Y + 1 }
	elseif (dir == "left") then
		return { X = ponto.X - 1, Y = ponto.Y }
	elseif (dir == "right") then
		return { X = ponto.X + 1, Y = ponto.Y }
	end
end

return this
