/// @description Insert description here
// You can write your code in this editor


file = file_text_open_read("knox.txt");

strData = "";

iRow = 0
while(!file_text_eof(file)) {
  strData = file_text_readln(file)
  
  iCol = 1
  while(iCol < string_length(strData) + 1) {
	  if (string_char_at(strData, iCol) = "#") {
	    instance_create_layer(iCol * 32, iRow * 32, 0, objWall)
	  }
	  iCol += 1
  }
  iRow += 1
  
}
file_text_close(file)
