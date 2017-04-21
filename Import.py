import requests
import sys
import json
from random import randint

TOP_SECRET = "Mcjdan,dryd.ugjtgo.oekrpat!"
user_url = "https://byteme.online/api/user/"
#user_url = "http://127.0.0.1:8000/api/user/"
token_url = "https://byteme.online/api/token/"
#token_url = "http://127.0.0.1:8000/api/token/"
save_url = "https://byteme.online/api/save/"
#save_url = "http://127.0.0.1:8000/api/save/"

if len(sys.argv) != 3:
    print("Missing required parameter: \n"
          "python Import.py <UserImportFile> <SaveImportFile")
    exit()

user_import_file = sys.argv[1]
save_import_file = sys.argv[2]

user_data = []
save_data = []
tokens = []

cur_string = ""

try:
    with open(user_import_file) as user_file:
        data = user_file.read()
        user_data = data.split(";")

    with open(save_import_file) as save_file:
        data = save_file.read()
        save_data = data.split(";")

    for user in user_data:
        user = user.replace('\n', '')
        cur_string = user

        if len(user) > 0:
            header = {"topsecret": TOP_SECRET, "Content-Type": "application/json"}
            json_obj = json.loads(user, encoding='utf8')
            request = requests.post(user_url, json=json_obj, headers=header)

            request = requests.post(token_url, json=json_obj, headers=header)
            tokens.append(request.json()['token'])

    for save in save_data:
        save = save.replace('\n', '')
        cur_string = save

        if len(save) > 0:
            token = tokens[randint(0, len(tokens))]
            header = {"topsecret": TOP_SECRET, "Content-Type": "application/json", "Authorization": "Token " + token}
            json_obj = json.loads(save, encoding='utf8')
            request = requests.post(save_url, json=json_obj, headers=header)

except Exception as ex:
    print("Error dummy")
    print(ex)
    print(cur_string)

