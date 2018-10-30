local this = {}

this.nome = 'João'
this.idade = 42

function this.envelhecer()
	this.idade = this.idade + 1
end

function this.saudar()
	print('Olá, eu me chamo', this.nome, 'e tenho', this.idade, 'anos!')
end

return this

