function main()

	local pessoa = new 'pessoa.lua'

	print('A pessoa se chama', pessoa.nome, ' e tem', pessoa.idade, ' anos.')

	pessoa.nome = 'Maria'

	print('A pessoa agora se chama', pessoa.nome, ' e tem', pessoa.idade, ' anos.')
end
