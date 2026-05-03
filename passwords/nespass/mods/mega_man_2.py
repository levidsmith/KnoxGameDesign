# 2026 Levi D. Smith <developer@levidsmith.com>
from pydantic import BaseModel

class QueryParams(BaseModel):
  energyTanks: int 
  metalManDefeated: bool | None = False
  airManDefeated: bool | None = False
  bubbleManDefeated: bool | None = False
  quickManDefeated: bool | None = False
  crashManDefeated: bool | None = False
  flashManDefeated: bool | None = False
  heatManDefeated: bool | None = False
  woodManDefeated: bool | None = False

def get_password(query: QueryParams):
  password = [] 

  energyTanksCode = ["A1", "A2", "A3", "A4", "A5"]
  metalManAliveCode = ["E1", "E2", "E3", "E4", "E5"]
  metalManDeadCode = ["E5", "B1", "B2", "B3", "B4"]
  airManAliveCode = ["D2", "D3", "D4", "D5", "E1" ]
  airManDeadCode = [ "E3", "E4", "E5", "B1", "B2" ]
  bubbleManAliveCode = [ "C3", "C4", "C5", "D1", "D2" ]
  bubbleManDeadCode = [ "D1", "D2", "D3", "D4", "D5" ] 
  quickManAliveCode = [ "C4", "C5", "D1", "D2", "D3" ]
  quickManDeadCode = [ "B4", "B5", "C1", "C2", "C3" ]
  crashManAliveCode = [ "E2", "E3", "E4", "E5", "B1"]
  crashManDeadCode = [ "C5", "D1", "D2", "D3", "D4"]
  flashManAliveCode = [ "E4", "E5", "B1", "B2", "B3" ]
  flashManDeadCode = [ "C1", "C2", "C3", "C4", "C5" ]
  heatManAliveCode = [ "D5", "E1", "E2", "E3", "E4" ]
  heatManDeadCode = [ "B2", "B3", "B4", "B5", "C1" ]
  woodManAliveCode = [ "B5", "C1", "C2", "C3", "C4" ]
  woodManDeadCode = [ "D3", "D4", "D5", "E1", "E2" ]

  password.append(energyTanksCode[query.energyTanks])

  if (query.metalManDefeated):
    password.append(metalManDeadCode[query.energyTanks])
  else:
    password.append(metalManAliveCode[query.energyTanks])

  if (query.airManDefeated):
    password.append(airManDeadCode[query.energyTanks])
  else:
    password.append(airManAliveCode[query.energyTanks])

  if (query.bubbleManDefeated):
    password.append(bubbleManDeadCode[query.energyTanks])
  else:
    password.append(bubbleManAliveCode[query.energyTanks])

  if (query.quickManDefeated):
    password.append(quickManDeadCode[query.energyTanks])
  else:
    password.append(quickManAliveCode[query.energyTanks])

  if (query.crashManDefeated):
    password.append(crashManDeadCode[query.energyTanks])
  else:
    password.append(crashManAliveCode[query.energyTanks])

  if (query.flashManDefeated):
    password.append(flashManDeadCode[query.energyTanks])
  else:
    password.append(flashManAliveCode[query.energyTanks])

  if (query.heatManDefeated):
    password.append(heatManDeadCode[query.energyTanks])
  else:
    password.append(heatManAliveCode[query.energyTanks])

  if (query.woodManDefeated):
    password.append(woodManDeadCode[query.energyTanks])
  else:
    password.append(woodManAliveCode[query.energyTanks])

  password.sort()

  data = { "password": ",".join(password) }
  return data
