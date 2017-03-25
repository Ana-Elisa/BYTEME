from django.shortcuts import render
from jchart import Chart
from jchart.config import rgba, DataSet
from KittyKrawler.models import Leaderboard, GameSave

class AvgTimePerLevelBar(Chart):
    chart_type = 'bar'

    def get_labels(self, *args, **kwargs):
        return ['Level 1', 'Level 2', 'Level 3', '+']

    def get_datasets(self, *args, **kwargs):
        data = []

        level0_data = GameSave.objects.filter(next_level=1)
        level1_data = GameSave.objects.filter(next_level=2)
        level2_data = GameSave.objects.filter(next_level=3)
        level3_data = GameSave.objects.filter(next_level=4)

        level0_time = 0
        for level_data in level0_data:
            level0_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
        data.append(level0_time/len(level0_data))

        level1_time = 0
        for level_data in level1_data:
            level1_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
        data.append(level1_time/len(level1_data))

        level2_time = 0
        for level_data in level2_data:
            level2_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
        data.append(level2_time/len(level2_data))

        level3_time = 0
        for level_data in level3_data:
            level3_time += (level_data.time.days * 25 + level_data.time.seconds/3600)
        data.append(level3_time/len(level3_data))

        colors = [
            rgba(255, 99, 132, 0.2),
            rgba(54, 162, 235, 0.2),
            rgba(255, 206, 86, 0.2),
            rgba(75, 192, 192, 0.2),
        ]

        return [DataSet(
            label='Average Time Per Level (Hours)',
            data=data,
            backgroundColor=colors
        )]


def index(request):
    return render(request, "website/index.html", {'bar_chart': AvgTimePerLevelBar()})
