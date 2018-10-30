-- Biblioteca de manipulação de strings
-- Documentação:
--
-- 	Pra começar, temos que declarar uma nova string com:
-- 		local str = strings.String('foo')
-- 	Depois podemos pegar o segundo caractere de uma string com:
-- 		print(str[2]) -> 'o'
-- 	Também podemos modificá-lo (só lembrando que vetores em lua começam em 1):
-- 		str[1] = 'F'
--
-- 	Temos funções de CAIXA ALTA e caixa baixa: upper e lower
--
-- 	E de vez em quando não queremos mais o objeto dessa biblioteca (queremos uma string 'pura', normal)
-- 	Pra isso, 'arrancamos' a string ali de dentro:
-- 	local normal = str.buffer
--

local this = {}

local mt = {
	__index = function(table, key)
		return string.sub(table.buffer, key, key)
	end,

	__newindex = function(table, key, value)
		table.buffer = string.sub(table.buffer, 1, key - 1) .. value .. string.sub(table.buffer, key+1)
	end,

}

function this.String(s)

	local str = {
		buffer = s
	}

	setmetatable(str, mt)

	return str
end

function this.upper(s)
	return string.upper(s)
end

function this.lower(s)
	return string.lower(s)
end

return this
