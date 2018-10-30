local level = {}

level.name = "Revisão Sintática"
level.map = "Sprites/Rooms/revisao"
level.index = 6

level.dialog = {
	{ char = "pepper/normal", contents = "Bom, lembra de quando eu falei que não era pra se assustar com a sintaxe? É, tem uma hora que cada um de nós tem que enfrentar seus demônios..." },
	{ char = "pepper/normal", contents = "Nesse código temos a estrutura de um 'for' e de um 'if'. Caso se esqueça de algum deles, volta para esta fase para se lembrar!" },
	{ char = "pepper/normal", contents = 'Mas agora a questão é a seguinte: aquele slime ali está muito apressado e quer chegar rápido naquele tesouro! Você consegue fazê-lo chegar lá em uma só chamada do método update? (Dica: a sintaxe do for não está ali à toa... mas nada impede você não usá-lo :v)' },
}

-- Files provided by the level
level.files = {
	{ path = "slime.lua" },
}

function level.ready()

	slime = spawn('slime', 1, 9)
end

function level.initialize()

	setScript(slime, 'slime.lua')

end

function level.update()

	local pos = slime.getPosition()

	if (slime.base.TimesRun > 0) then
		return 'lose'
	end

	if ((pos.X == 8 or pos.X == 9 or pos.X == 10) and (pos.Y == 1 or pos.Y == 2)) then
		return 'win'
	end

end

return level
