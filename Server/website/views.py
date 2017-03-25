from django.shortcuts import render
from .charts import *


def index(request):
    return render(request, "website/index.html", {'bar_chart': AvgTimePerLevelBar(), 'line_chart': TimeSpentvsLevel()})
