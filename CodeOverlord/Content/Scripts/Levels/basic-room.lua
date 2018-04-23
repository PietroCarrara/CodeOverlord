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

level.dimensions = { x = 1300, y = 1300 }

level.spawns = {
	{ x = 0, y = -8 },
	{ x = -1, y = -8 },
}

return level
