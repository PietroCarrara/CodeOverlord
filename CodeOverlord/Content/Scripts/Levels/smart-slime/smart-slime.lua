local level = {}

level.name = 'Slime Esperto'
level.map = "Sprites/Rooms/revisao"
level.index = 7

level.dialog = {
	{ char = "pepper/normal", contents = 'Ainda bem que você chegou a tempo! Eu estava escrevendo um pouco de código, mas eu fiquei muito cansada de ter que escrever 15 linhas de código ou fazer um "for" toda vez que eu quizer andar algumas casas...' },
	{ char = "pepper/normal", contents = 'Foi pra isso que eu criei a classe "slime-esperto"! Chega de código repetitivo, é só herdar dessa classe que seus slimes podem chamar "this.direita(15)" para andar 15 casas para a direita! Não é uma maravilha?' },
	{ char = "pepper/normal", contents = '... É uma pena que não saiu muito do papel... Eu já tenho o slime herdando do slime-esperto, porém ele ainda não é esperto... Mas os deuses ouviram minhas preces e me enviaram um programador para me ajudar!' },
	{ char = "pepper/normal", contents = 'Você consegue implementar os métodos "direita" e "cima" pra mim?' },
	
	-- { char = "pepper/normal", contents = 'Tive mais uma idéia mirabolante! MoveToPoint, ensinar a dar console.log em tudo' },
}

level.files = {
	{ path = 'slime-esperto.lua' },
	{ path = 'slime.lua', readOnly = true },
}

function level.ready()

	slime = spawn('slime', 2, 8)

end

function level.initialize()
	setScript(slime, 'slime.lua')
end

function level.update()

	local pos = slime.getPosition()

	if (pos.X == 9 and pos.Y == 2) then
		return 'win'
	end

end

return level

