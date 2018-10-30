local level = {}

level.name = "Labirinto"
level.map = "Sprites/Rooms/labyrinth"
level.index = 11


level.files = {
	{ path = "bússola.lua", readOnly = true },
	{ path = "bússolaNova.lua" },
	{ path = "slime.lua", readOnly = true },
}

level.dialog = {
	{ char = "pepper/normal", contents = "Poxa vida... Se eu não acabar isso antes de o professor voltar eu vou estar ferrada..." },
	{ char = "pepper/normal", contents = 'Ah, ainda bem que você chegou! Eu estava tentando fazer um slime que resolve labirintos, mas por enquanto não estou conseguindo!' },
	{ char = "pepper/normal", contents = 'O código do slime e da bússola eu peguei com um colega, então não posso mecher neles.' },
	{ char = "pepper/normal", contents = 'Eles funcionam... digo, eles não explodem, mas o slime fica preso em um loop infinito!' },
	{ char = "pepper/normal", contents = 'Eu acho que é por causa da bússola.lua, que recomenda as direções que causam isso. Como eu não posso mudar ela, o slime ao invés de usar a bússola, ele usa a bússolaNova, que é uma classe que eu criei para tentar modificar o comportamento da bússola!' },
	{ char = "pepper/normal", contents = 'O que eu estava pensando em fazer era usar a sobrescrita (ou overriding): se declararmos um método com o mesmo nome que nossa classe pai, nós modificamos o método original! Eu queria, então, modificar algum dos métodos (ou o getDirecao ou o renovarCaminho) para recomendar outra rota!' },
	{ char = "pepper/normal", contents = 'Se você conseguir resolver esse problema pra mim, serei eternamente grata!' },

	-- { char = "pepper/studying", contents = '' },
}

-- Building level
function level.ready()
	slime = spawn('slime', 5, 11)

end

-- Starting level
function level.initialize()
	
	setScript(slime,'slime.lua')

end

-- Checking for win
function level.update()

	local pos = slime.getPosition()

	if (pos.X == 10 and pos.Y == 3) then
		return "win"
	end

end

return level
