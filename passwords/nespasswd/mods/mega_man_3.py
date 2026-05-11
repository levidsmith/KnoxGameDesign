# 2026 Levi D. Smith <developer@levidsmith.com>
from pydantic import BaseModel

class QueryParams(BaseModel):
  energyTanks: int 
  geminiMan: bool | None = False
  hardMan: bool | None = False
  magnetMan: bool | None = False
  needleMan: bool | None = False
  shadowMan: bool | None = False
  snakeMan: bool | None = False
  sparkMan: bool | None = False
  topMan: bool | None = False
  docGeminiMan: bool | None = False
  docNeedleMan: bool | None = False
  docShadowMan: bool | None = False
  docSparkMan: bool | None = False
  breakMan: bool | None = False

def get_password(query: QueryParams):
  password = [] 

  all_bosses = [query.geminiMan, query.hardMan, query.magnetMan, query.needleMan,
               query.shadowMan, query.snakeMan, query.sparkMan, query.topMan ]
  all_doc_bosses = [query.docGeminiMan, query.docNeedleMan, query.docShadowMan, query.docSparkMan]

  energy_tanks_code = ["C5", "E6", "E4", "B4", "A5", "C1", "D2", "C3", "F2", "A6"]

  password_red = []
  password_blue = []

  password_red.append(energy_tanks_code[query.energyTanks])

  if query.geminiMan and query.hardMan:
    password_blue.append("B5")
  else:
    if query.geminiMan: 
      password_red.append("B5")
    if query.hardMan: 
      password_red.append("C4")

  if query.magnetMan and query.needleMan:
    password_blue.append("D3")
  else:
    if query.magnetMan:
      password_red.append("F5")
    if query.needleMan:
      password_red.append("D3")
      
  if query.shadowMan and query.sparkMan:
    password_blue.append("F4")
  else:
    if query.shadowMan:
      password_red.append("D6")
    if query.sparkMan:
      password_red.append("F4")
      
  if query.snakeMan and query.topMan:
    password_blue.append("A3")
  else:
    if query.snakeMan:
      password_red.append("F6")
    if query.topMan:
      password_red.append("A3")
      

  if all(all_bosses):
    if query.docGeminiMan and query.docNeedleMan:
      password_blue.append("B2")
    else:
      if query.docGeminiMan:
        password_red.append("B6")
      if query.docNeedleMan:
        password_red.append("B2")
      
    if query.docShadowMan and query.docSparkMan:
      password_blue.append("A1")
    else:
      if query.docShadowMan:
        password_red.append("A4")
      if query.docSparkMan:
        password_red.append("A1")
      
    if query.breakMan and all(all_doc_bosses):
      password_red.append("E1")

  password_red.sort()
  password_blue.sort()

  data = { "red password": ",".join(password_red),  
           "blue password": ",".join(password_blue)  
         }
  return data
