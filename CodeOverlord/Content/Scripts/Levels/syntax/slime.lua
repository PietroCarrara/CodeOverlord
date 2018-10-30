local this = new 'Slime.lua'

-- Função chamada toda vez
-- que for nossa vez de agir
function this.update()

	-- Imprimir os números pares de 1 até 10
	for i=1,10 do
		if (i % 2 == 0) then
			print(i)
		end
	end

	-- Mover pra cima e depois pra direita
	moveUp()
	moveRight()
end

return this
