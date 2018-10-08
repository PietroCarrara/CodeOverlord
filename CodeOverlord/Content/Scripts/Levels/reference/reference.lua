local level = {}

level.name = "Objetos e Referências"
level.map = "Sprites/Rooms/basic"
level.index = 3

level.files = {
	{ path = "slime.lua", readOnly = true },
	{ path = "modifica_nome.lua" },
	{ path = "main.lua", readOnly = true },
}

level.dialog = {
	{ char = "pepper/normal", contents = "Bom, se já está estudando orientação a objeto, já deve ter estudado passagem por valor e referência." },
	{ char = "pepper/normal", contents = "Caso não consiga se lembrar, quando passamos algo por valor, primeiro criamos uma cópia daquilo e então passamos esta cópia adiante. Quando passamos uma referência, estamos permitindo que modifiquem o valor original diretamente." },
	{ char = "pepper/normal", contents = "E, uma coisa que sempre temos que nos lembrar, é que todos os objetos são passados por referência!" },
	{ char = "pepper/normal", contents = "Isso nos permite fazer várias coisas interessantes, como funções que os manipulam!" },
	{ char = "pepper/normal", contents = "Por exemplo, aqui temos um código que muda o nome dos slimes, mesmo passando-os normalmente para uma função." },
	{ char = "pepper/normal", contents = "Note que em momento nenhum falamos em ponteiros. Isso por que Lua abstrai isso pra nós. Por baixo dos panos, existem sim ponteiros, mas o Lua é muito melhor que nós em gerenciá-los." },
	{ char = "pepper/normal", contents = "Mas voltando ao que importa: o código. Ele está quase pronto, só falta a parte de mudar o nom" },
}

-- Building level
function level.ready()
	slime = spawn('slime', 5, 11)

end

-- Starting level
function level.initialize()
	
	setScript(slime,'slime.lua')

	-- recompile
	new 'modifica_nome.lua'
	new 'main.lua'

	main(slime)
end

-- Checking for win
function level.update()

	if slime.nome ~= '' then
		return 'win'
	end

end

return level

