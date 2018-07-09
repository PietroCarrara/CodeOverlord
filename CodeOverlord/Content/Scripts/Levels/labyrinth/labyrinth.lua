local level = {}

level.name = "Labirinto"

level.files = {
	{ path = "bússola.lua", readOnly = true },
	{ path = "bússolaNova.lua" },
	{ path = "slime.lua", readOnly = true },
}

level.dimensions = { x = 1300, y = 1300 }

level.map = "Sprites/Rooms/Basic Room/map"

level.dialog = {
	{ char = "pepper/studying", contents = "Poxa vida... Se eu não acabar isso antes de o professor voltar eu vou estar ferrada..." },
	{ char = "pepper/scared", contents = "Agh! Quem é você? Eu estava tão distraída com as minhas tarefas que nem te vi entrando..." },
	-- { char = "pepper/normal", contents = "Espera aí! Você deve ser o novo estagiário que o professor contratou! Que hora perfeita!" },
	-- { char = "pepper/normal", contents = "Essas tarefas que o professor me passou são muito difíceis, então um par extra de mãos vão ser bem úteis." },
	-- { char = "pepper/normal", contents = "Eu estou aprendendo 'magia orientada a objeto' com o professor, só que é muito diferente de magia estruturada..." },
	-- { char = "pepper/normal", contents = "Mas não tema, fiel estagiário! Eu estou contando com você pra fazer meu dever de casa!" },
	-- { char = "pepper/normal", contents = "Eu tenho que criar 3 objetos, mas não tá dando não! ... Espera aí, você não sabe magia orientada a objeto? Então vamos pelo começo..." },
	-- { char = "pepper/normal", contents = "Primeiramente, para invocar um objeto, precisamos de um feitiço, uma 'receita' se preferir." },
	-- { char = "pepper/normal", contents = "Nós chamamos essa receita de 'Classe', e usamos ela para moldar vários coisas, que chamamos de 'objetos'." },
	-- { char = "pepper/normal", contents = "Dois objetos da mesma receita, ou do mesmo tipo, terão as mesmas propriedades, mas que podem ser de valores diferentes." },
	-- { char = "pepper/normal", contents = "Por exemplo, se tivermos a Classe pessoa, duas pessoas terão Nomes, mas uma pode ser 'Alice' e outra 'Bob'." },
	-- { char = "pepper/normal", contents = "Bom, voltando ao que importa: O meu dever de casa." },
	-- { char = "pepper/normal", contents = "O professor me deu uma classe, e eu tenho que criar 3 objetos a partir dela, mas eu não estou conseguindo! Daria pra quebrar esse galho pra mim?" },
	-- { char = "pepper/scared", contents = "Ah, e para invocar objetos, é só selecionar a classe no menu à direita e clicar com o botão direito." },

}

local slime = nil

function level.ready()

	slime = spawn('slime', 0, 0)

end

function level.initialize()
	
	slime.setScript('Content/Scripts/Levels/labyrinth/slime.lua')

end

function level.update()

	local pos = slime.gridPos

	if (pos.X == 1 and pos.Y == 0) then
		return "win"
	end

end

return level