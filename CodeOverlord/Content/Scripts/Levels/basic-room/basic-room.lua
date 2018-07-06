--
-- basic-room.lua
-- Copyright (C) 2018 pietro <pietro@the-arch>
--
-- Distributed under terms of the MIT license.
--

local level = {}

level.name = "Basic Room"

level.map = "Sprites/Rooms/Basic Room/map"

level.dialog = {
	{ char = "pepper/normal", contents = "Poxa vida... Se eu não acabar isso antes de o professor voltar eu vou estar ferrada..." },
	{ char = "pepper/scared", contents = "Agh! Quem é você? Eu estava tão distraída com as minhas tarefas que nem te vi entrando..." },
	{ char = "pepper/normal", contents = "Espera aí! Você deve ser o novo estagiário que o professor contratou! Que hora perfeita!" },
	{ char = "pepper/normal", contents = "Essas tarefas que o professor me passou são muito difíceis, então um par extra de mãos vão ser bem úteis." },
	{ char = "pepper/normal", contents = "Eu estou aprendendo 'magia orientada a objeto' com o professor, só que é muito diferente de magia estruturada..." },
	{ char = "pepper/normal", contents = "Mas não tema, fiel estagiário! Eu estou contando com você pra fazer meu dever de casa!" },
	{ char = "pepper/normal", contents = "Eu tenho que criar 3 objetos, mas não tá dando não! ... Espera aí, você não sabe magia orientada a objeto? Então vamos pelo começo..." },
	{ char = "pepper/normal", contents = "Primeiramente, para invocar um objeto, precisamos de um feitiço, uma 'receita' se preferir." },
	{ char = "pepper/normal", contents = "Nós chamamos essa receita de 'Classe', e usamos ela para moldar vários coisas, que chamamos de 'objetos'." },
	{ char = "pepper/normal", contents = "Dois objetos da mesma receita, ou do mesmo tipo, terão as mesmas propriedades, mas que podem ser de valores diferentes." },
	{ char = "pepper/normal", contents = "Por exemplo, se tivermos a Classe pessoa, duas pessoas terão Nomes, mas uma pode ser 'Alice' e outra 'Bob'." },
	{ char = "pepper/normal", contents = "Bom, voltando ao que importa: O meu dever de casa." },
	{ char = "pepper/normal", contents = "O professor me deu uma classe, e eu tenho que criar 3 objetos a partir dela, mas eu não estou conseguindo! Daria pra quebrar esse galho pra mim?" },
	{ char = "pepper/scared", contents = "Ah, e para invocar objetos, é só selecionar a classe no menu à direita e clicar com o botão direito." },

}

level.dimensions = { x = 1300, y = 1300 }

-- User can't create new files
-- Still to be implemented
level.lockFiles = true

-- Files provided by the level
level.files = {
	{ path = "object.lua", readOnly = true },
}

function level.update()
	
	-- Every update spanw a rogue
	local rogue = spawn("rogue")
	rogue.SetScript("Content/Scripts/Heroes/Rogue/rogue.lua")

	if (#monsters() >= 3) then
		return "win"
	else
		return "lose"
	end
end

return level
