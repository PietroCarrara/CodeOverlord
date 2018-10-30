local this = new 'Slime.lua'

function this.movePara(x, y)
	local pos = this.getPosition()

	print(pos)

	print('Quero me mover para X =', x, 'e Y =', y)
end

function this.direita(vezes)
	for i=1,vezes do
		moveRight()
	end
	for i=1,vezes*-1 do
	    moveLeft()
	end
end

function this.esquerda(vezes)
	for i=1,vezes do
		moveLeft()
	end
	for i=1,vezes*-1 do
	    moveRight()
	end
end

function this.cima(vezes)
	for i=1,vezes do
		moveUp()
	end
	for i=1,vezes*-1 do
	    moveDown()
	end
end

function this.baixo(vezes)
	for i=1,vezes do
		moveDown()
	end
	for i=1,vezes*-1 do
	    moveUp()
	end
end

return this




