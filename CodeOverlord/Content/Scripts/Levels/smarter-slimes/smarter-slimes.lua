local level = {}

level.name = 'Slime Ainda Mais Esperto'
level.map = "Sprites/Rooms/revisao"
level.index = 8

level.dialog = {
	{ char = "pepper/normal", contents = 'Haha, ficou muito legal!' },
	{ char = "pepper/normal", contents = 'Mas eu tive uma pequena ideia agora: eu acho que seria interessante se chamássemos qualquer método de movimento com números negativos, ele andasse na direção contrária!' },
	{ char = "pepper/normal", contents = 'Por exemplo: se direita(-5) for chamado, o slime deveria andar 5 casas para a esquerda!' },
	{ char = "pepper/normal", contents = 'Bom, eu terminei de implementar as direções que faltavam, então será que você pode programar esse meio de lidar com números negativos?' },
	
	-- { char = "pepper/normal", contents = '' },
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

	if (slime.base.TimesRun > 0) then
		return 'lose'
	end

	local pos = slime.getPosition()

	if (pos.X == 9 and pos.Y == 2) then
		return 'win'
	end

end

return level


