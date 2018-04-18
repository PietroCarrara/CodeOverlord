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
	"Content/Scripts/Heroes/Rogue/rogue.lua"
}

-- Width and heigth in a 1280x720 screen
level.dimensions = { x = 1000, y = 1000 }

level.spawns = {
	{ x = 10, y = 2 }
}

return level
