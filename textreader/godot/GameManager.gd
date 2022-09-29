extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	parse_file()
	
func parse_file():
	var scene_wall = load("res://box.tscn")
	var wall	
	
	var strFile = 'res://knox.txt'
	var f = File.new()
	f.open(strFile, File.READ)
	var iRow = 0
	while not f.eof_reached():
		var line = f.get_line()
		line += " "
		var iCol = 0
		while (iCol < line.length()):
			if (line[iCol] == '#'):
				wall = scene_wall.instance()
				wall.translation = Vector3(iCol, 0, iRow)
				add_child(wall)

			iCol += 1
			
		iRow += 1
	f.close()
	return



# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
