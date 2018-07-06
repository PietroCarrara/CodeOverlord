-- Este código é o que chamamos de Classe.
-- É como se fosse uma linha de montagem:
-- Começamos com uma variável simples e
-- vamos botanto coisas nela

-- Criamos a variável this, que começa
-- sendo um slime simples
local this = Slime()

-- Definimos a função update dentro de this
-- Qualquer um pode chamar esta função. Note
-- que ainda podemos chamar funções definidas
-- fora de this
function this.update()
	move()
end

-- Esta função está fora de this. Portanto,
-- somente aqui dentro podemos chamar ela.
function move()
	moveUp()
end

-- Por fim, devolvemos this para quem requisitou
return this
