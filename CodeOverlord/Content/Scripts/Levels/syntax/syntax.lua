local level = {}

level.name = "Revisão Sintática"

level.map = "Sprites/Rooms/revisao"

level.dialog = {
	{ char = "pepper/normal", contents = "Bom, vamos começar por uma revisão básica..." },
	{ char = "pepper/normal", contents = "Aqui no laboratório usamos a linguagem Lua, que além de ser brasileira é orientada a objetos!" },
	{ char = "pepper/normal", contents = "Esse código aberto no editor mostra algumas coisas básicas sobre a linguagem:" },
	{ char = "pepper/normal", contents = "Vemos como fazer um for e um if, além de como declarar funções." },
	{ char = "pepper/normal", contents = "Veja que quando declaramos o contador do nosso for ou quando declaramos funções, não expressamos tipos!" },
	{ char = "pepper/normal", contents = "Isso por que em Lua, variáveis podem guardar qualquer coisa, sem serem limitadas por tipos." },
	{ char = "pepper/normal", contents = "Mas não deixe isso embolar muito sua cabeça, o importante agora é focar nos elementos da sintaxe." },
	{ char = "pepper/normal", contents = "Caso venha se esquecer deles, volte para essa fase ou dê uma olhada online, eu tenho certeza de que vai conseguir achar o que estivar procurando!" },
	{ char = "pepper/normal", contents = "Aquele slime ali no canto está se remoendo para começar sua viagem, então é só clicar em começar! Te vejo nas outras fases (mas não espere que elas sejam tão fáceis quanto essa!)" },
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
