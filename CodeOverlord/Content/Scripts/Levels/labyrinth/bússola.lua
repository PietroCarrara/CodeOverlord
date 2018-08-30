local this = {}

this.direcao = 0

-- Girando no sentido anti-horário
function this.getDirecao()

	if this.direcao == 0 then 
		return "up"
	elseif this.direcao == 1 then
		return "left"
	elseif this.direcao == 2 then
		return "down"
	elseif this.direcao == 3 then
		return "right"
	end

end

function this.renovarCaminho() 
	if this.direcao >= 3 then
		this.direcao = 0
	else
		this.direcao = this.direcao + 1
	end
end

return this
