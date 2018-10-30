local level = {}

level.name = 'Aprofundando na Classe'
level.map = "Sprites/Rooms/revisao"
level.index = 2

level.dialog = {
		{ char = "pepper/normal", contents = 'Bom, agora sabemos o básico sobre classes, elas ainda não são muito diferentes de structs. Então vamos ver algumas coisas que classes e objetos fazem que deixam as structs no chinelo.' },
	{ char = "pepper/normal", contents = 'Uma boa maneira de resumir as diferenças é: "structs só guardam dados, enquanto que objetos guardam E manipulam esses dados."' },
	{ char = "pepper/normal", contents = 'Como os objetos manipulam seus dados? É simples: eles podem ter funções dentro de si.' },
	{ char = "pepper/normal", contents = 'Eu dei uma melhorada na classe de pessoa, pra incluir algumas funções. Agora a pessoa em si sabe como manipular seus dados. E uma dica: quando um objeto tem funções dentro de si, ela passa a se chamar método, e é esse o nome que vamos usar daqui para frente.' },
	{ char = "pepper/normal", contents = 'Agora, se você der uma olhada no main.lua, você verá que ele tem uma cor mais cinza. Isso por que ele é uma arquivo de somente leitura: não podemos modificá-lo.' },
	{ char = "pepper/normal", contents = 'Isso normalmente acontece quando estamos trabalhando em equipe e aquele código é de um colega. A questão aqui é que o nosso colega assumiu um método que não existe: rejuvenescer.' },
	{ char = "pepper/normal", contents = 'Se tentarmos rodar isso (clicando em começar), um erro bizarro saltará em nossa tela e não avançaremos na fase. Você poderia implementar um método rejuvenescer, que reduz a idade em 1?' },

	-- { char = "pepper/normal", contents = '' },
}

level.files = {
	{ path = "main.lua", readOnly = true },
	{ path = "pessoa.lua" },
}

function level.update()
	
	new 'main.lua'

	main()

	local p = new 'pessoa.lua'
	local idade = p.idade
	p.rejuvenescer()
	p.rejuvenescer()
	p.rejuvenescer()
	
	if (p.idade + 3 == idade) then
		return 'win'
	else
		return 'lose'
	end

end

return level


