local level = {}

level.name = 'Relembrando Structs'
level.map = "Sprites/Rooms/revisao"
level.index = 1

level.dialog = {
	{ char = "pepper/normal", contents = "Boas vindas ao Code Overlord, uma ferramenta que vai tentar facilitar sua vida ao estudar orientação a objeto! (Ou pelo menos tentar, haha)" },
	{ char = "pepper/normal", contents = "Para começar, vamos dar uma relembrada em um conceito da programação estruturada: structs" },
	{ char = "pepper/normal", contents = 'Structs eram "tipos customizados" que podíamos criar. Elas eram como uma caixa que tinha outras variáveis dentro de si.' },
	{ char = "pepper/normal", contents = 'A struct "Pessoa", por exemplo, poderia ter os campos "nome" e "idade". No caso, este é o código que pode ser visto em pessoa.lua (e você pode abrí-lo clicando em seu nome).' },
	{ char = "pepper/normal", contents = 'Na orientação a objeto, o equivalente de uma struct é uma classe. Ela faz bem mais que uma struct, mas vamos nos focar no básico por enquanto.' },
	{ char = "pepper/normal", contents = 'Ah, a propósito, não deixe o que não entender de sintaxe te assustar. O importante daqui é entender os conceitos que estão sendo usados, ok?' },
	{ char = "pepper/normal", contents = 'Uma classe é uma espécie de linha de montagem: ' },
	{ char = "pepper/normal", contents = 'Começamos com uma "caixa" vazia (Chaves vazias: {}) e depois vamos botando pra dentro dela aquilo que queremos: nome e idade. Quando temos tudo isso, devolvemos essa caixa pra quem tenha pedido ela.' },
	{ char = "pepper/normal", contents = 'Repare que em nenhum momento dizemos quais são os tipos das variáveis. Isso por que a linguagem que estamos usando (Lua), não precisa deles: ela sabe que "João" é uma string e que 42 é um inteiro!' },
	{ char = "pepper/normal", contents = 'Ok, declaramos nossa classe, mas e agora? Bom vamos fazer uso dela! No arquivo main.lua temos a nossa função principal, a main. Vamos ver o que ela faz.' },
	{ char = "pepper/normal", contents = 'Vamos dar uma boa olhada no que está acontecendo:' },
	{ char = "pepper/normal", contents = 'Logo na primeira linha, temos a palavra "local". Acontece que, em lua, se declaramos uma variável sem "local" antes, ela será uma variável global (vai saber quem pensou que isso era uma boa ideia...), então é um bom hábito sempre declarar variáveis com "local" antes.' },
	{ char = "pepper/normal", contents = 'Ok, estamos declarando uma variável e inicializando ela com algum valor. Mas que valor é esse?' },
	{ char = "pepper/normal", contents = 'Agora chegamos na palavra "new". Ela é responsável por pedir para que seja construído uma variável de alguma classe.' },
	{ char = "pepper/normal", contents = 'Passamos o nome da nossa classe (pessoa), e ela nos devolve uma "nova pessoa", entendeu?' },
	{ char = "pepper/normal", contents = 'Quando usamos a palavra new, damos pra ela uma classe e ela nos devolve uma "caixa" já construída, que chamamos de objeto.' },
	{ char = "pepper/normal", contents = 'Então se a classe é uma fábrica, o objeto é o produto dela. Uma maneira mais chique de falar isso é dizer que "o objeto é uma instância de uma classe".' },
	{ char = "pepper/normal", contents = 'E assim como uma fábrica da vida real, podemos produzir vários produtos com uma só fábrica. Tente criar outras pessoas e manipulá-las!' },
	{ char = "pepper/normal", contents = 'Quando estiver satisfeito e quiser avançar para a próxima fase, clique em começar para vencer esta fase automaticamente (um favorzinho meu)' },

	-- { char = "pepper/normal", contents = '' },
}

level.files = {
	{ path = "pessoa.lua" },
	{ path = "main.lua" },
}

function level.update()
	
	new 'main.lua'

	main()

	return 'win'

end

return level

