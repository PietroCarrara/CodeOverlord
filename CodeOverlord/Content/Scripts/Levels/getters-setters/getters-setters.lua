local level = {}

level.name = "Getters e Setters"

level.files = {
	{ path = "strings.lua", readOnly = true },
	{ path = "slime.lua" },
	{ path = "main.lua", readOnly = true },
}

level.map = "Sprites/Rooms/revisao"

level.dialog = {
	{ char = "pepper/normal", contents = "Ok, você consegui definir os nomes que eu te falei!" },
	{ char = "pepper/normal", contents = "Uma coisa legal sobre nomes é que eles sempre começam com letra maiúscula, mas nada impede um programador de definir o nome daqueles slimes começando em letras minúsculas!" },
	{ char = "pepper/normal", contents = "Para impedir isso, agora os nomes dos slimes estão privados, então ninguém vai poder mudá-los! Hahahahaha, este código é infalível!... Exceto.. que... ele é meio inútil, né? De que adianta ter uma variável de nome se ninguém pode usar ela pra nada?" },
	{ char = "pepper/normal", contents = "Pra deixar as pessoas usarem nossas variáveis privadas, usamos 'getters e setters', que são funções públicas que vão manipular nossas variáveis privadas!" },
	{ char = "pepper/normal", contents = "Se essa última frase minha não fez tanto sentido assim, eu posso resumir: usamos getters e setters pra deixar as pessoas mexerem nos nossos objetos seguindo as nossas regras!" },
	{ char = "pepper/normal", contents = "Veja, por exemplo, o código da main aberto no editor: o nosso colega de trabalho que escreveu essa função se esqueceu das boas maneiras do português e deixou os pobres slimes com nomes minúsculos!" },
	{ char = "pepper/normal", contents = "E nós ainda por cima não podemos editar o código que ele escreveu!" },
	{ char = "pepper/normal", contents = "Agh, mas nós podemos editar os setters do slime então!" },
	{ char = "pepper/normal", contents = "Vamos adicionar uma regra pra que caso a primeira letra do nome seja minúscula, ela fique maiúscula! Você consegue fazer isso?" },
	{ char = "pepper/normal", contents = "Agh, caso ainda não tenha jogado a fase de manipulação de strings, eu recomendo fazê-la caso não consiga concluir esse desafio!" },
}

level.counter = 0

-- Building level
function level.ready()
	slime = spawn('slime', 5, 7)
	slime2 = spawn('slime', 5, 8)

	level.counter = 0
end

-- Starting level
function level.initialize()
	
	setScript(slime, 'slime.lua')
	setScript(slime2, 'slime.lua')

	new 'main.lua'
end

-- Checking for win
function level.update()

	level.counter = level.counter + 1

	if level.counter <= 1 then
		main(slime, slime2)
	end

	if level.counter < 3 then 
		return
	end

	level.counter = 0
	if slime.getNome() == 'Michael' and slime2.getNome() == 'Jackson' then
		return 'win'
	else
		return 'lose'
	end

end

return level
