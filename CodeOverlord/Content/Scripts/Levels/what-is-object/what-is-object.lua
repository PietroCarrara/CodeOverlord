local level = {}

level.name = "O que é objeto?"

level.map = "Sprites/Rooms/revisao"

level.dialog = {
	{ char = "pepper/normal", contents = "Opa, boas vindas ao Code Overlord, uma ferramenta que vai tentar facilitar sua vida ao estudar orientação a objeto!" },
	{ char = "pepper/normal", contents = "Pra começar, vamos falar um pouco sobre esses tais de objetos." },
	{ char = "pepper/normal", contents = "Quando programamos, podemos notar a separação de dois conceitos: dados (estruturas, structs) e quem manipula esses dados (funções)." },
	{ char = "pepper/normal", contents = 'Porém, a orientação a objeto começa a misturar esses dois. Agora nós vamos deixar nossos dados inteligentes. Ao invés de nos perguntarmos "o que minhas estruturas guardam?" vamos nos perguntar "o que nossas estruturas fazem?".' },
	{ char = "pepper/normal", contents = "Com a orientação a objetos, nossas estruturas não só possuem dados, mas possuem funções dentro delas (que chamamos de métodos). Então elas guardam dados e podem realizar ações (elas fazem um pouco mais que isso, mas vamos focar no básico, que é a essencial para o resto)." },
	{ char = "pepper/normal", contents = 'Assim como declarávamos nossas estruturas, também temos que declará-las aqui, mas pelo fato de serem tão mais complexas, elas não são mais chamadas de estruturas: são chamadas de classes.' },
	{ char = "pepper/normal", contents = 'É na classe que declaramos o que nossos objetos terão e o que farão. Assim como uma estrutura, é um "tipo customizado".' },
	{ char = "pepper/normal", contents = 'Bom, vamos dar uma olhada em como é uma classe. Abra o arquivo contador.lua (é só clicar no nome).' },
	{ char = "pepper/normal", contents = 'A classe pode ser pensada como uma linha de montagem: Passamos um produto que começa vazio, e vamos adicionando as partes que precisamos.' },
	{ char = "pepper/normal", contents = 'Não deixe o que não entender da sintaxe te intimidar: o importante daqui é entender a ideia do que está acontecendo.' },
	{ char = "pepper/normal", contents = 'A primeira peça que adicionamos é a variável "contador". Ela vai ser manipulada pelos próximos métodos.' },
	{ char = 'pepper/normal', contents = 'Os próximos métodos não recebem nenhum parâmetro e devem modificar o contador para subir em uma unidade ou descer em uma unidade.' },
	{ char = "pepper/normal", contents = 'Infelizmente, essa parte ainda não foi programada! Será que você poderia dar uma mãozinha?' },
	{ char = "pepper/normal", contents = 'Ah, quando achar que o programa estiver pronto, é só clicar em começar que eu vou testá-lo! (caso tenha deixado os prints, você poderá ver os testes acontecendo!)' },
}

-- Files provided by the level
level.files = {
	{ path = "contador.lua"  },
}

function level.ready()

end

function level.initialize()

	count = new 'contador.lua'
end

function level.update()

	local contador = 0

	for i = 1,10 do
		local r = math.random() * 100
		local d = math.floor(r % 3)
		if (d ~= 0) then
			count.aumentaContador()
			contador = contador + 1
		else
			count.diminuiContador()
			contador = contador - 1
		end
	end

	print('Fim dos testes, esperava', contador, ', encontrou', count.contador)

	if (count.contador == contador) then
		return 'win'
	else
		return 'lose'
	end
end

return level

