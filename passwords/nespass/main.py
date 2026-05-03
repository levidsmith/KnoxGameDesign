# 2026 Levi D. Smith <developer@levidsmith.com>

from fastapi import FastAPI
from pydantic import BaseModel
import mods.mega_man_2
import mods.mike_tysons_punch_out

app = FastAPI()

@app.get("/")
async def root():
  return { "message": "Hello" }

@app.get("/game/{id}")
async def game(id):
  id = id.upper()
  data = None

  game_data = load_game_data()


  if id in game_data.keys():
    return game_data[id]
  else:
    return None 

@app.get("/game")
async def game_query(name: str):
  name = name.lower()
  data = None

  game_data = load_game_data()


  for key in game_data.keys():
    if name in game_data.get(key).get("name").lower():
      return game_data.get(key)

  return {}

@app.post("/game/NES-XR-USA/password")
async def name_password(query: mods.mega_man_2.QueryParams):
  return mods.mega_man_2.get_password(query)
  
@app.post("/game/NES-PT-USA/password")
async def name_password(query: mods.mike_tysons_punch_out.QueryParams):
  return mods.mike_tysons_punch_out.get_password(query)
  


def load_game_data():
  game_data = {}
  game_data["NES-MT-USA"] = {"id": "NES-MT-USA", "name": "Metroid" } 
  game_data["NES-KI-USA"] = {"id": "NES-KI-USA", "name": "Kid Icarus" } 
  game_data["NES-XR-USA"] = {"id": "NES-XR-USA", "name": "Mega Man 2" } 
  game_data["NES-PT-USA"] = {"id": "NES-PT-USA", "name": "Mike Tyson's Punch Out!!" } 

  return game_data
