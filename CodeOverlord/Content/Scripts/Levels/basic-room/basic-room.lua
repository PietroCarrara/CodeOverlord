--
-- basic-room.lua
-- Copyright (C) 2018 pietro <pietro@the-arch>
--
-- Distributed under terms of the MIT license.
--

local level = {}

level.name = "Basic Room"

level.map = "Sprites/Rooms/Basic Room/map"

level.heroes = {
	"Content/Scripts/Heroes/Rogue/rogue.lua",
	"Content/Scripts/Heroes/Rogue/rogue.lua",
	"Content/Scripts/Heroes/Rogue/rogue.lua",
}

level.dialog = {
	{ char = "ursula/normal", contents = "Ah, você deve ser o novato..."},
	{ char = "ursula/normal", contents = "Bom, não temos tempo a perder. Está vendo esta sala? Em breve ela estará cheia de ladrões. Prepare os slimes até então."},
	{ char = "constanze/normal", contents = "Ei, Ursula, o que está acontecendo aqui?"},
	{ char = "ursula/normal", contents = "Agh, Professor!"},
	{ char = "constanze/normal", contents = "Não importa, eu preciso de paz aqui no laboratório! E quem é esse aí?" },
	{ char = "ursula/normal", contents = "Ah, é o novo estagiário, senhor."},
	{ char = "constanze/normal", contents = "Ah, ótimo! Mostre pra ele como são as coisas por aqui, mas por favor não façam nenhuma algazarra, eu preciso de concetração."},
	{ char = "constanze/normal", contents = "Estarei na minha sala se precisarem de algo." },
}

level.dimensions = { x = 1300, y = 1300 }

-- User can't create new files
-- Still to be implemented
level.lockFiles = true

-- Files provided by the level
level.files = {
	"sample-slime.lua"
}

level.spawns = {
	{ x = 0, y = -8 },
	{ x = -1, y = -8 },
}

return level
