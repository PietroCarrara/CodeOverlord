local this = new 'pessoa.lua'

this.salario = 1000

function this.info()
	return 'Salário do funcionário:', this.salario, 'reais.'
end

return this
