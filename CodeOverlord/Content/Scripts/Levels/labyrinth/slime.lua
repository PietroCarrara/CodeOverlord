local this = new 'Slime.lua'

local bussola = new "bússolaNova.lua"

function this.update()

	local dir = bussola.getDirecao()
	local posicao = this.getPosition()

	-- Pegamos a direção que a bússola dos deu
	posicao = direcaoToPonto(posicao, dir)

	-- Se a casa está disponível (não é uma parede, uma mesa...), movemos para ela
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
	-- Senão, pedimos uma nova direção
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
