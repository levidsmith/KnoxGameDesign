# 2026 Levi D. Smith <developer@levidsmith.com>
from pydantic import BaseModel

class QueryParams(BaseModel):
  majorCircuit: bool | None = None
  worldCircuit: bool | None = None
  mikeTysonFight: bool | None = None
  anotherWorldCircuit: bool | None = None

def get_password(query: QueryParams):
  password = [] 

  if query.mikeTysonFight:
    password = "007 373 5963"
  elif query.worldCircuit:
    password = "777 807 3454"
  elif query.majorCircuit:
    password = "005 737 5432"
  elif query.anotherWorldCircuit:
    password = "135 792 468 (press A+B+Select on the last number)"


  data = { "password": password }
  return data
