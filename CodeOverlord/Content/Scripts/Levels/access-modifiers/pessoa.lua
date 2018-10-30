local this = {}

local idade = 42
local nome = 'João'

function this.envelhecer()
	idade = idade + 1
end

function this.saudar()
	return 'Olá, eu me chamo', nome, 'e tenho', idade, 'anos!'
end

return this

