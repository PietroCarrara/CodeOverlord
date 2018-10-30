local level = {}

level.name = 'Herança'
level.map = "Sprites/Rooms/revisao"
level.index = 3

level.dialog = {

	{ char = "pepper/normal", contents = 'Ok, temos uma classe de pessoa bem interessante, mas e se for preciso especializá-la um pouco mais? Por exemplo, se o nosso sistema precisar lidar com pessoas comuns E com funcionários de uma empresa?' },
	{ char = "pepper/normal", contents = 'Os funcionários tem tudo que uma pessoa normal tem (nome e idade), mas eles também tem salário. Nós vamos criar outra classe (funcionário), mas não queremos ter que copiar e colar o código da pessoa, pois isso é muito propenso a erros! Se no futuro tivermos que mudar qualquer coisa na pessoa, teremos que nos lembrar também de mudar no funcionário separadamente!' },
	{ char = "pepper/normal", contents = 'Felizmente, a orientação a objeto nos fornece uma coisa para fazer justamente isso: a herança. Quando dizemos que uma classe herda de outra, ela faz tudo que essa outra faz. Então se o funcionário herdar de pessoa, ele terá nome e idade, e adicionar uma informação extra: seu salário. ' },
	{ char = "pepper/normal", contents = 'Em Lua, para fazermos herança, ao invés de dizer que a nossa "caixa" começa vazia ({}) dizemos que ela começa com outra classe (new "classeMae.lua"). Você poderia fazer o funcionário herdar de pessoa?' },
	

	-- { char = "pepper/normal", contents = '' },
}

level.files = {
	{ path = "pessoa.lua", readOnly = true },
	{ path = "funcionario.lua" },
	{ path = "main.lua", readOnly = true },
}

function level.update()
	
	new 'main.lua'

	main()

	local f = new 'funcionario.lua'

	if f.envelhecer == nil then
		return 'loseFuncionário não herda de pessoa!'
	end

	if (f.nome ~= nil and f.salario ~= nil and f.idade ~= nil) then
		return 'win'
	else
		return 'loseFuncionário não herda de pessoa!'
	end

end

return level


