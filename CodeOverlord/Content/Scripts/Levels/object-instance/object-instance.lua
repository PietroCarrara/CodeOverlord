local level = {}

level.name = "Objetos Instanciados"
level.map = "Sprites/Rooms/basic"
level.index = 4

level.files = {
	{ path = "slime.lua", readOnly = true },
	{ path = "main.lua" },
}

level.dialog = {
	{ char = "pepper/normal", contents = "Ok, agora que você viu como criar objetos, vamos começar a manipulá-los." },
	{ char = "pepper/normal", contents = "Você tem uma função que recebe dois objetos de slime (dê uma lida no script deles para entendê-los)." },
	{ char = "pepper/normal", contents = "Eu gostaria que o slime1 tivesse o nome de Michael e o slime2 tivesse o nome de Jackson, você consegue fazer isso?" },
}

level.counter = 0

-- Building level
function level.ready()
	slime = spawn('slime', 5, 11)
	slime2 = spawn('slime', 5, 12)

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


	if slime.nome == 'Michael' and slime2.nome == 'Jackson' then
		return 'win'
	else
		return 'lose'
	end

end

return level
