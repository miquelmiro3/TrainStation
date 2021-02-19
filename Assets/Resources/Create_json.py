
female = ["0","1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","19","21", # 20
"2.1","3.1","4.1","5.1","6.1","7.1","8.1","9.1","10.1","11.1","12.1","13.1","14.1","15.1","16.1","18.1","19.1","20.1","21.1"] 

for x in female:
    title = "Female" + x
    text = "{\n"
    text += "\t\"id\": \"" + title + "\",\n"
    text += "\t\"tasks\": [\n"
    text += "\t\t\"DrinkMachine\"\n"
    text += "\t]\n"
    text += "}"
    f = open("Agendas/" + title + ".json", "w")
    f.write(text)
    f.close()

male = []

for x in male:
    title = "Male" + x
    text = "{\n"
    text += "\t\"id\": \"" + title + "\",\n"
    text += "\t\"tasks\": [\n"
    text += "\t\t\"FindDrinkMachine\"\n"
    text += "\t]\n"
    text += "}"
    f = open("Agendas/" + title + ".json", "w")
    f.write(text)
    f.close()

