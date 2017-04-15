from django.shortcuts import render
from .charts import *


def index(request):
    return render(request, "website/index.html")

def charts(request):


    return render(request, "website/charts.html", {
        'bar_chart': AvgTimePerLevelBar(),
        'line_chart': TimeSpentvsLevel(),
        'pie_chart': PieChartItemsFound(),
    })
