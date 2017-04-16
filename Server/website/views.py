from django.shortcuts import render
from .charts import *
from KittyKrawler.models import GameSave


def index(request):
    return render(request, "website/index.html")

def charts(request):
    save_queryset = GameSave.objects.all()

    save_unpack = [(save.user.username, len(save.save_items.all()), save.attack, save.defence, save.speed,
                    save.health, save.total_health, save.next_level, save.time)
                   for save in save_queryset]

    save_sorted = sorted(save_unpack,
                         key=lambda s: ((s[1] + s[2] + s[3] + s[4] + s[5] + s[6]) * s[7]) - (s[8].total_seconds()/1000),
                         reverse=True)

    leaderboard = save_sorted[0:11]

    return render(request, "website/charts.html", {
        'bar_chart': AvgTimePerLevelBar(),
        'line_chart': TimeSpentvsLevel(),
        'pie_chart': PieChartItemsFound(),
        'leaderboard': leaderboard
    })
