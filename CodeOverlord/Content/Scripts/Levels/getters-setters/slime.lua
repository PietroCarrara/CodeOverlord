local this = Slime()

local name

function this.setName(n)
	name = n
end

function this.getName()
	return name
end

function this.update()

	print('Meu nome é ', name)

	passTurn()
end

return this
