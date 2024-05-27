import json

with open("cards.json") as f:
    cards = json.load(f)
    for card in cards:
        title = card["Title"]
        effect = card["CardEffect"]
        print(f"//{effect}\ncase \"{title}\":\n    return true;")