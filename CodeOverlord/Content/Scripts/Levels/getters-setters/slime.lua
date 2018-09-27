local this = Slime()

-- Biblioteca de manipulação de strings
local strings = require 'strings'

local nome

function this.setNome(n)
	-- Temos que garantir que a primeira letra seja sempre maiúscula
	nome = n
end

function this.getNome()
	return nome
end

function this.update()

	print('Meu nome é ', nome)

	passTurn()
end

return this


