local level = {}

level.name = 'Slime Praticamente Vivo'
level.map = "Sprites/Rooms/revisao"
level.index = 9

level.dialog = {
	{ char = "pepper/normal", contents = 'Caramba, esses slimes estão ficando muito espertos! Eu não duvido que a inteligência deles supere a minha...' },
	{ char = "pepper/normal", contents = 'Mas... então... Enquanto você programava eu meio que tive outra ideia... hehe...' },
	{ char = "pepper/normal", contents = 'E se, ao invés de descrevermos passo a passo o caminho que o slime deve tomar, nós simplesmente disséssemos que queremos ir para o ponto X, Y? Eu acho que isso classificaria o slime como praticamente um ser vivo, de tão esperto!' },
	{ char = "pepper/normal", contents = 'O código do slime-esperto ali já lida com números negativos, então só falta implementar essa última parte!' },
	{ char = "pepper/normal", contents = 'Eu já tinha começado a esquematizar alguma coisa, mas você acabou as outras fases rápido demais, hehe...' },
	{ char = "pepper/normal", contents = 'Ah, e aqui vão algumas dicas: de vez enquando, não sabemos como é um objeto que estamos recebendo. Pra resolver isso, simplesmente printamos ele na tela, como é o caso ali no movePara, com o objeto da posição do slime.' },
	{ char = "pepper/normal", contents = 'Quando fazemos isso, uma coisa no estilo "{ nome: \'João\' }", significando que aquele objeto tem uma variável "nome" com o valor "João".' },
	{ char = "pepper/normal", contents = 'Rode uma vez o código para ver como é! Você não vai passar de fase assim, mas não tenha medo! É falhando que se aprende (e isso vale em dobro quando falamos de programação, hehe...)' },
	{ char = "pepper/normal", contents = 'Ah, e um último lebrete! Quando falamos de posições, o X=1,Y=1 é o canto superior esquerdo da tela, e o X=15,Y=9 é o inferior direito!' },
	{ char = "pepper/normal", contents = 'Boa sorte!' },
	
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



