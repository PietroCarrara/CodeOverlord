local this = new 'Slime.lua'

function this.direita(vezes)
	for i=1,vezes do
		moveRight()
	end
end

function this.esquerda(vezes)
	for i=1,vezes do
		moveLeft()
	end
end

function this.cima(vezes)
	for i=1,vezes do
		moveUp()
	end
end

function this.baixo(vezes)
	for i=1,vezes do
		moveDown()
	end
end

return this

