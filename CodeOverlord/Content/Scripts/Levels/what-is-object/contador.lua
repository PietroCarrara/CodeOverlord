-- Começamos como um vazio
local this = {}

-- Nós temos agora uma variável
-- contador
this.contador = 0

-- Nós temos um método update
function this.aumentaContador()

	-- Falta aumentar o contador em 1

	print('Contador aumentado, valendo agora:', this.contador)
end

-- Outro método
function this.diminuiContador()

	-- Falta diminuir o contador em 1

	-- Experimente printar o contador aqui! (opcional)
end

-- Devolvemos o objeto que acabamos
-- de montar pra quem pediu
return this
