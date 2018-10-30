local level = {}

level.name = 'Modificadores de Acesso'
level.map = "Sprites/Rooms/revisao"
level.index = 4

level.dialog = {
	{ char = "pepper/normal", contents = 'E agora, um dos últimos motivos que separam os objetos das structs: os modificadores de acesso.' },
	{ char = "pepper/normal", contents = 'Nós temos funções e variáveis, mas uma coisa que é comum é nossos colegas de equipe fazerem besteira com nossos objetos (normalmente é por que não entenderam como usá-los, e não por maldade).' },
	{ char = "pepper/normal", contents = 'É, por exemplo, o caso do nosso colega, que tentou mudar o salário de um funcionário para um valor astronômico!' },
	{ char = "pepper/normal", contents = 'Para resolver coisas assim, nós definimos algumas limitações. Com os modificadores de acesso, nós podemos dizer quem pode e quem não pode acessar nossas variáveis e métodos.' },
	{ char = "pepper/normal", contents = 'No início, todos as coisas da nossa classe eram públicas: podem ser acessadas por qualquer um. Isso não é o ideal, já que não fornece muito controle para nós.' },
	{ char = "pepper/normal", contents = 'Uma solução para este problema é transformar o salário em privado: ele pode somente ser acessado de dentro da classe. Ou seja, somente os nossos métodos podem acessá-lo.' },
	{ char = "pepper/normal", contents = 'Em Lua, para tornar algo privado, declaramos ela fora da "caixa", como foi feito com a idade e o nome da pessoa. Assim, quando forem usar o objeto, o valor privado estará escondido do mundo de fora.' },
	{ char = "pepper/normal", contents = 'Se transformarmos o salário em privado, podemos impedir que o nosso colega modifique coisas que ele não deveria. Poderia quebrar esse galho pra mim?' },

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

	local p = new 'funcionario.lua'

	_, salario = p.info()

	if (salario ~= nil and p.salario == nil) then
		return 'win'
	else
		return 'lose'
	end

end

return level

