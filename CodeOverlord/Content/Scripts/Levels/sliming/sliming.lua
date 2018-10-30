local level = {}

level.name = 'Slimes'
level.map = "Sprites/Rooms/revisao"
level.index = 5

level.dialog = {
	
	{ char = "pepper/normal", contents = 'Bom, você deve estar se perguntando, mas pra quê serve essa sala atraz de mim?' },
	{ char = "pepper/normal", contents = 'É nela que as coisas começam a ficar mais interessantes. Ela é uma maneira de visualizar melhor o seu código, com criaturas que obedecem suas instruções sendo postas ali.' },
	{ char = "pepper/normal", contents = 'Nela, cada criatura chama o próprio método update() uma de cada vez. Aqui, há apenas um slime, ligado à classe slime.lua.' },
	{ char = "pepper/normal", contents = 'Além disso, dentro do update, você pode chamar o método moveUp() para andar para cima (moveDown, moveLeft e moveRight para as outras direções).' },
	{ char = "pepper/normal", contents = 'Experimente tentar mover o slime! Tire-o de sua posição inicial.' },


	-- { char = "pepper/normal", contents = '' },
}

level.files = {
	{ path = "slime.lua" }
}

function level.ready()
	slime = spawn('slime', 7, 7)
end

function level.initialize()
	setScript(slime, 'slime.lua')
	foi = false
end


function level.update()

	if (not foi) then
		foi = true
		return
	end
	
	local pos = slime.getPosition()

	if (pos.X ~= 7 or pos.Y ~= 7) then
		return 'win'
	else 
		return 'lose'
	end

end

return level
